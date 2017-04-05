using System;
using System.Reactive.Linq;
using Windows.UI.Core;

namespace ReactiveUniversal.Rx {

	public sealed class ControlProperty<TSource>: IObserver<TSource>, IObservable<TSource> {

		public ControlEvent<TSource> Changed {
			get {
				return new ControlEvent<TSource>(values.Skip(1));
			}
		}

		private readonly IObservable<TSource> values;
		private readonly IObserver<TSource> valueSink;

		public ControlProperty(IObservable<TSource> values, IObserver<TSource> valueSink) {
			if (values == null) {
				throw new ArgumentNullException("values are null");
			}
			if (valueSink == null) {
				throw new ArgumentNullException("valueSink is null");
			}
			this.values = values.SubscribeOnDispatcher(CoreDispatcherPriority.Normal);
			this.valueSink = valueSink;
		}

		public void OnCompleted() {
			valueSink.OnCompleted();
		}

		public void OnError(Exception error) {
			throw error;
		}

		public void OnNext(TSource value) {
			valueSink.OnNext(value);
		}

		public IDisposable Subscribe(IObserver<TSource> observer) {
			return values.Subscribe(observer);
		}

		public IObservable<TSource> AsObservable() {
			return values;
		}

		public ControlProperty<TSource> AsControlProperty() {
			return this;
		}
	}
}
