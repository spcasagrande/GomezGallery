using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GomezGallery.Domain.Entities;

namespace GomezGallery.WebUI.Models
{
	public class PictureListViewModel
	{
		public IEnumerable<Picture> Pictures { get; set; }

		public PagingInfo PagingInfo { get; set; }

		// add current category
		public string CurrentCategory { get; set; }
	}
}