using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalApp.Model {

	public class Photo {

		public int AlbumId { get; set; }
		public int Id { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string ThumbnailUrl { get; set; }

		public override string ToString() {
			return string.Format("{0}\n{1}\n", Title, ThumbnailUrl);
		}
	}
}
