﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomezGallery.Domain.Entities
{
	public class CartLine
	{
		public Picture Picture { get; set; }
		public int Quantity { get; set; }
	}

	public class Cart
	{
		// Initiliazing the cart
		private List<CartLine> lineCollection = new List<CartLine>();

		// Getter for CartLine
		public IEnumerable<CartLine> Lines
		{
			get { return lineCollection; }
		}

		// Add item
		public void AddItem(Picture myPicture, int myQuantity)
		{
			CartLine line = lineCollection
							.Where(p => p.Picture.PictureID == myPicture.PictureID)
							.FirstOrDefault();

			if (line == null)
			{
				lineCollection.Add(new CartLine
				{
					Picture = myPicture,
					Quantity = myQuantity
				});
			}
			else
			{
				line.Quantity += myQuantity;
			}
		}

		// Remove item
		public void RemoveLine(Picture myPicture)
		{
			lineCollection.RemoveAll(x => x.Picture.PictureID == myPicture.PictureID);
		}

		// Compute total cost
		public decimal ComputeTotalValue()
		{
			return lineCollection.Sum(x => x.Picture.Price * x.Quantity);
		}

		// Emptying the cart
		public void Clear()
		{
			lineCollection.Clear();
		}
	}
}
