using System.Net;

namespace ReactiveUniversal.Net {

	public sealed class HttpResponse<TSource> {

		public TSource Content;
		public HttpStatusCode Code;
		public string ReasonPhrase;
	}
}
