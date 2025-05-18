﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GomezGallery.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// list first page from all categories
			routes.MapRoute(null, "", new
			{
				Controller = "Picture",
				action = "List",
				category = (string)null,
				page = 1
			});

			// url = /Page2
			routes.MapRoute(null, "Page{page}", new
			{
				Controller = "Picture",
				action = "List",
				category = (string)null
			},
			new { page = @"\d+" }); // Regex; d=digits, + = one or more

			// first page of items from specific category, url = /Soccer
			routes.MapRoute(null, "{Category}", new
			{
				Controller = "Picture",
				action = "List",
				page = 1
			});

			// url = /Soccer/Page2
			routes.MapRoute(null, "{Category}/Page/{page}", new
			{
				Controller = "Picture",
				action = "List",
			},
			new { page = @"\d+" });

			routes.MapRoute(null, "{controller}/{action}");
		}
    }
}
