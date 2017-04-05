using System;
using System.Reactive.Linq;
using Windows.UI.Core;

namespace ReactiveUniversal.Rx {

	public sealed class ControlEvent<TSource>: IObservable<TSource> {

		private readonly IObservable<TSource> events;

		public ControlEvent(IObservable<TSource> events) {
			if (events == null) {
				throw new ArgumentNullException("events is null");
			}
			this.events = events.SubscribeOnDispatcher(CoreDispatcherPriority.Normal);
		}

		public IDisposable Subscribe(IObserver<TSource> observer) {
			return events.Subscribe(observer);
		}

		public IObservable<TSource> AsObservable() {
			return events;
		}

		public ControlEvent<TSource> AsControlEvent() {
			return this;
		}
	}
}
