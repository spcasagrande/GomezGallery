﻿using GomezGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomezGallery.Domain.Abstract
{
	public interface IOrderProcessor
	{
		void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
	}
}
