using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GomezGallery.Domain.Abstract;

namespace GomezGallery.WebUI.Controllers
{
	public class NavController : Controller
	{
		private IPictureRepository repository;

		public NavController(IPictureRepository repo)
		{
			repository = repo;
		}
		public PartialViewResult Menu(string category = null)
		{
			ViewBag.SelectedCategory = category;

			IEnumerable<string> categories = repository.Pictures.Select(x => x.Category)
				 .Distinct()
				 .OrderBy(x => x);

			return PartialView(categories);
		}
	}
}