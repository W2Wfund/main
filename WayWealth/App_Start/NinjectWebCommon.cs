using Common.Web.Auth;
using Common.Web.Mail;
using WayWealth.Infrastructure.Auth;
using WayWealth.Infrastructure.Mail;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;
using W2W.ModelKBT;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WayWealth.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WayWealth.App_Start.NinjectWebCommon), "Stop")]

namespace WayWealth.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<IRepo>().ToMethod(ctx => new Repo("Ninject Rocks!"));

            //kernel.Bind<IDataService>().To<FakeDataService>().InRequestScope();
            kernel.Bind<IDataService>().To<KbtDataService>().InSingletonScope();

            //kernel.Bind<IMailService>().To<FakeMailService>().InSingletonScope();
            kernel.Bind<IMailService>().To<MailService>().InSingletonScope();
            //kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InRequestScope();
        }
    }
}