using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GomezGallery.Domain.Entities;
using System.Data.Entity;

namespace GomezGallery.Domain.Concrete
{
	public class EFDbContext : DbContext
	{
		public DbSet<Picture> Pictures { get; set; }
	}
}
