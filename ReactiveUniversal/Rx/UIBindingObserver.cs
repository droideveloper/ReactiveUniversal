using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace ReactiveUniversal.Rx {

	public class UIBindingObserver<UIControl, TSource>: IObserver<TSource> {

		private static readonly CoreDispatcher UiThread = CoreApplication.MainView.Dispatcher;

		private readonly UIControl control;
		private readonly Action<UIControl, TSource> binding;

		public UIBindingObserver(UIControl control, Action<UIControl, TSource> binding) {
			if (control == null) {
				throw new ArgumentNullException("control is null");
			}
			if (binding == null) {
				throw new ArgumentNullException("binding is null");
			}
			this.control = control;
			this.binding = binding;
		}

		public void OnCompleted() {
			// no opt
		}

		public void OnError(Exception error) {
			throw error;
		}

		public void OnNext(TSource value) {
			if (UiThread.HasThreadAccess) {
				binding(control, value);
			} else {
				Task.Run(async () => {
					await UiThread.RunAsync(CoreDispatcherPriority.Normal, () => {
						OnNext(value);
					});
				});
			}
		}
	}
}
