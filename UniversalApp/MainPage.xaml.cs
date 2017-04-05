using ReactiveUniversal.Rx;
using ReactiveUniversal.Net;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalApp.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;

namespace UniversalApp {

	public sealed partial class MainPage: Page {

		private static readonly string BASE_URL = "https://jsonplaceholder.typicode.com";

		private readonly DisposeBag disposes = new DisposeBag();
		private readonly Variable<List<Photo>> photos = new Variable<List<Photo>>(null);

		public MainPage() {
			this.InitializeComponent();

			var viewModel = new ViewModel();
			var endpoint = RestService.For<Endipoint>(BASE_URL);

			DataContext = viewModel;

			photos
				.AsObservable()
				.Where(x => x != null)
				.SelectMany(x => x)
				.BindNext(Console.WriteLine)
				.DisposeBy(disposes);

			photos
				.AsObservable()
				.Where(x => x != null)
				.BindNext(viewModel.AddRange)
				.DisposeBy(disposes);		

			endpoint
				.Photos()
				.ToHttpResponse<List<Photo>>()
				.Where(r => r.Code == HttpStatusCode.OK)
				.Select(r => r.Content)
				.Retry(3)
				.Async()
				.BindNext(data => photos.Value = data)
				.DisposeBy(disposes);
		}
	}

	public sealed partial class ViewModel {

		private PhotoDataSource dataSource;
		public PhotoDataSource DataSource {
			get {
				return dataSource;
			}
		}

		public ViewModel() {
			dataSource = new PhotoDataSource();
		}

		public void AddRange(List<Photo> photos) {
			if (photos != null) {
				photos.ForEach(dataSource.Add);
			}
		}
	}

	public sealed partial class PhotoDataSource: ObservableCollection<Photo> { }

	public interface Endipoint {
		[Get("/photos")]
		IObservable<HttpResponseMessage> Photos();
	}
}
