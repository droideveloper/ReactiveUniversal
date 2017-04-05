using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ReactiveUniversal.Net {

	public static class Extensions {

		private static readonly int BUFFER = 8192;
		private static readonly HttpClient httpClient = new HttpClient();

		private static readonly JsonSerializerSettings defaultSettings = new JsonSerializerSettings() {
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			NullValueHandling = NullValueHandling.Ignore,
			DateFormatString = "dd-MM-yyyy'T'HH:mm:ss"
		};

		public static IObservable<HttpResponse<TSource>> ToHttpResponse<TSource>(this IObservable<HttpResponseMessage> source, JsonSerializerSettings settings = null) {
			return source.SelectMany(r => {
				if (r.StatusCode == HttpStatusCode.OK) {
					return Observable.FromAsync(async () => {
						settings = settings ?? defaultSettings;
						StringBuilder str = new StringBuilder();
						Stream inp = await r.Content.ReadAsStreamAsync();
						using (BinaryReader reader = new BinaryReader(inp)) {
							byte[] buffer = new byte[BUFFER];
							int read;
							while ((read = reader.Read(buffer, 0, BUFFER)) != 0) {
								str.Append(Encoding.UTF8.GetString(buffer, 0, read));
							}
						}
						TSource content = JsonConvert.DeserializeObject<TSource>(str.ToString(), settings);
						return new HttpResponse<TSource>() {
							Code = r.StatusCode,
							Content = content
						};
					});
				} else {
					return Observable.Return(new HttpResponse<TSource>() {
						Code = r.StatusCode,
						ReasonPhrase = r.ReasonPhrase
					});
				}
			});
		}

		public static IObservable<Stream> ToBinary(this Uri source) {
			return Observable.FromAsync(async () => {
				return await httpClient.GetStreamAsync(source);
			});
		}
	}
}
