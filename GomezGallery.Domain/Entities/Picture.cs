using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GomezGallery.Domain.Entities
{
	public class Picture
	{
		[HiddenInput(DisplayValue = false)]
		public int PictureID { get; set; }

		[Required(ErrorMessage = "Please enter product name")]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Please enter product description")]
		public string Description { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a price")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Please enter product category")]
		public string Category { get; set; }

		public byte[] ImageData { get; set; }

		public string ImageMimeType { get; set; }
	}
}
