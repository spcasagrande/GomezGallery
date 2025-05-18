using GomezGallery.Domain.Abstract;
using GomezGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GomezGallery.WebUI.Models;

namespace GomezGallery.WebUI.Controllers
{
	public class CartController : Controller
	{
		private IPictureRepository repository;
		private IOrderProcessor orderProcessor;

		public CartController(IPictureRepository repo, IOrderProcessor proc)
		{
			repository = repo;
			orderProcessor = proc;
		}

		// Add to cart
		public RedirectToRouteResult AddToCart(Cart cart, int pictureID, string returnUrl)
		{
			Picture picture = repository.Pictures.FirstOrDefault(p => p.PictureID == pictureID);

			if (picture != null)
			{
				cart.AddItem(picture, 1);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		// Remove from cart
		public RedirectToRouteResult RemoveFromCart(Cart cart, int pictureID, string returnUrl)
		{
			Picture picture = repository.Pictures.FirstOrDefault(p => p.PictureID == pictureID);

			if (picture != null)
			{
				cart.RemoveLine(picture);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public ViewResult Index(Cart cart, string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				ReturnUrl = returnUrl,
				Cart = cart
			});
		}

		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}

		public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}

		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			// empty cart check
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			// check for errors in ModelState
			if (ModelState.IsValid)
			{
				orderProcessor.ProcessOrder(cart, shippingDetails);
				cart.Clear();
				return View("Completed", shippingDetails);
			}
			else
			{
				return View(shippingDetails);
			}
		}
	}
}