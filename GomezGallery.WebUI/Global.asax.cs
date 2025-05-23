using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GomezGallery.Domain.Entities;
using GomezGallery.WebUI.Infrastructure.Binders;

namespace GomezGallery.WebUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// add ModelBinders
			ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
		}
	}
}
