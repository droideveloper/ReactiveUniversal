using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUniversal.Rx {

	public sealed class DisposeBag {

		private readonly CompositeDisposable disposes = new CompositeDisposable();

		public void Add(IDisposable dispose) {
			bool alreadyAdded = dispose == null || disposes.Contains(dispose);
			if (!alreadyAdded) {
				disposes.Add(dispose);
			}
		}

		~DisposeBag() {
			if (!disposes.IsDisposed) {
				disposes.Dispose();
			}
		}
	}
}
