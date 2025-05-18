using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GomezGallery.Domain.Abstract;
using GomezGallery.Domain.Entities;
using GomezGallery.WebUI.Models;

namespace GomezGallery.WebUI.Controllers
{
    public class PictureController : Controller
    {
		private IPictureRepository myRepository;

		public PictureController(IPictureRepository pictureRepository)
		{
			this.myRepository = pictureRepository;
		}

		public int PageSize = 3;

		public ViewResult List(string category, int page = 1)
		{
			PictureListViewModel model = new PictureListViewModel
			{
				Pictures = myRepository.Pictures
				.Where(p => category == null || p.Category == category)
				.OrderBy(p => p.PictureID)
				.Skip((page - 1) * PageSize)
				.Take(PageSize),

				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = category == null ?
									myRepository.Pictures.Count() :
									myRepository.Pictures.Where(x => x.Category == category).Count()
				},
				CurrentCategory = category
			};

			return View(model);
		}

		public FileContentResult GetImage(int pictureID)
		{
			Picture pic = myRepository.Pictures.FirstOrDefault(p => p.PictureID == pictureID);

			if (pic != null)
			{
				return File(pic.ImageData, pic.ImageMimeType);
			}
			else
			{
				return null;
			}
		}
	}
}