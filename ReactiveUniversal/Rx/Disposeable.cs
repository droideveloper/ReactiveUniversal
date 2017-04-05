using System.Reactive.Subjects;

namespace ReactiveUniversal.Rx {

	public sealed class Disposeable {

		private static readonly object notify = new object();

		public readonly ReplaySubject<object> DestroySink = new ReplaySubject<object>(1);

		~Disposeable() {
			DestroySink.OnNext(notify);
			DestroySink.OnCompleted();
		}
	}
}
