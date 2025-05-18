using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GomezGallery.Domain.Entities;

namespace GomezGallery.Domain.Abstract
{
	public interface IPictureRepository
	{
		IEnumerable<Picture> Pictures { get; }

		void SavePicture(Picture picture);

		Picture DeletePicture(int pictureId);
	}
}
