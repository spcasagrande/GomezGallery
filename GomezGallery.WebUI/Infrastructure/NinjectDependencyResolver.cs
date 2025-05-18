using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Moq;
using GomezGallery.Domain.Abstract;
using GomezGallery.Domain.Entities;
using GomezGallery.Domain.Concrete;
using System.Configuration;
using GomezGallery.WebUI.Infrastructure.Abstract;
using GomezGallery.WebUI.Infrastructure.Concrete;

namespace GomezGallery.WebUI.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel mykernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			mykernel = kernelParam;
			AddBindings();
		}

		public object GetService(Type myserviceType)
		{
			return mykernel.TryGet(myserviceType);
		}

		public IEnumerable<object> GetServices(Type myserviceType)
		{
			return mykernel.GetAll(myserviceType);
		}


		private void AddBindings()
		{
			mykernel.Bind<IPictureRepository>().To<EFPictureRepository>();

			EmailSettings emailSettings = new EmailSettings
			{
				WriteAsFile = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
			};

			mykernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

			mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
		}
	}
}