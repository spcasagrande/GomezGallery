using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GomezGallery.Domain.Abstract;
using GomezGallery.Domain.Entities;

namespace GomezGallery.WebUI.Controllers
{
	[Authorize]
    public class AdminController : Controller
    {
		private IPictureRepository repository;

		public AdminController(IPictureRepository repo)
		{
			repository = repo;
		}

		public ViewResult Index()
		{
			return View(repository.Pictures);
		}

		public ViewResult Edit(int pictureID)
		{
			Picture picture = repository.Pictures.FirstOrDefault(p => p.PictureID == pictureID);

			return View(picture);
		}

		[HttpPost]
		public ActionResult Edit(Picture picture, HttpPostedFileBase image = null)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					picture.ImageMimeType = image.ContentType;
					picture.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(picture.ImageData, 0, image.ContentLength);
				}

				repository.SavePicture(picture);
				TempData["message"] = string.Format("{0} has been saved", picture.Name);
				return RedirectToAction("Index");
			}
			else
			{
				return View(picture);
			}
		}

		public ViewResult Create()
		{
			return View("Edit", new Picture());
		}

		public ActionResult Delete(int pictureID)
		{
			Picture deletedPicture = repository.DeletePicture(pictureID);

			if (deletedPicture != null)
			{
				TempData["message"] = string.Format("{0} has been deleted", deletedPicture.Name);
			}

			return RedirectToAction("Index");
		}
	}
}