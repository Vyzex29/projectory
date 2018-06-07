using System;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using projectory.DbContext;
using projectory.Facades.Identity;
using projectory.Interfaces.Facades;
using projectory.Interfaces.Repositories;
using projectory.Interfaces.Services.Contracts;
using projectory.Models.repository;
using projectory.Repositories;
using projectory.Services.Contracts;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(projectory.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(projectory.App_Start.NinjectWebCommon), "Stop")]
namespace projectory.App_Start
{
   

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IUserService>()
                .To<UserService>()
                .InRequestScope();
            kernel
                .Bind<IIdeaService>()
                .To<IdeaService>()
                .InRequestScope();
            kernel
                .Bind<IAvatarService>()
                .To<AvatarService>()
                .InRequestScope();
            kernel
                .Bind<ICommentService>()
                .To<CommentService>()
                .InRequestScope();
            kernel
                .Bind<IRolesService>()
                .To<RolesService>()
                .InRequestScope();
            kernel
                .Bind<IRepoDbContext>()
                .To<RepoDbContext>()
                .InRequestScope();
            kernel
                .Bind<IGenericRepository<ApplicationUser>>()
                .To<GenericRepository<ApplicationUser>>()
                .InRequestScope();
            kernel
                .Bind<IGenericRepository<Idea>>()
                .To<GenericRepository<Idea>>()
                .InRequestScope();
            kernel
                .Bind<IGenericRepository<Comment>>()
                .To<GenericRepository<Comment>>()
                .InRequestScope();
            kernel
                .Bind<IGenericRepository<UserAvatar>>()
                .To<GenericRepository<UserAvatar>>()
                .InRequestScope();
            kernel
                .Bind<IGenericRepository<Rating>>()
                .To<GenericRepository<Rating>>()
                .InRequestScope();
            kernel
                .Bind<IGenericRepository<IdentityRole>>()
                .To<GenericRepository<IdentityRole>>()
                .InRequestScope();
            kernel
                .Bind<IUserIdentityFacade>()
                .To<UserIdentityFacade>()
                .InRequestScope();
            kernel
                .Bind<IApplicationSignInManagerFacade>()
                .To<AplicationSignInManagerFacade>()
                .InRequestScope();
            kernel
                .Bind<IApplicationUserManagerFacade>()
                .To<ApplicationUserManagerFacade>()
                .InRequestScope();
        }        
    }
}
