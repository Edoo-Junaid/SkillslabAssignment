using SkillslabAssigment.DAL.DAL;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Logger;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Notification;
using SkillslabAssignment.Service;
using SkillslabAssignment.Service.ValidationRule;

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Injection;
namespace SkillslabAssignment.WebApi
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; internal set; }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            string connectionString = ConfigurationManager.ConnectionStrings["MSSQLServerConnection"].ConnectionString;
            string bucketString = ConfigurationManager.ConnectionStrings["Bucket"].ConnectionString;
            container.RegisterType<IDbConnection, SqlConnection>(
                new InjectionConstructor(connectionString)
            );
            //container.RegisterType<IStorrageService, FirebaseStorageService>(
            //    new InjectionConstructor(bucketString)
            //);
            container.RegisterType<IStorrageService, FirebaseStorageServiceV2>();

            container.RegisterType<DbConnection, SqlConnection>(
                new InjectionConstructor(connectionString)
            );
            container.RegisterType(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            container.RegisterType(typeof(IGenericService<,>), typeof(GenericService<,>));
            container.RegisterType<ILogger, Logger>(
                new InjectionConstructor(@"C:\Users\P12AD74\skillslab\final\SkillslabAssignment\SkillslabAssignment.WebApi\App_Data\log.txt")
            );
            container.RegisterType<IPermissionRepository, PermissionRepository>();
            container.RegisterType<ICacheService, MemoryCacheService>();
            container.RegisterType<IPermissionService, PermissionService>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IPendingAccountService, PendingAccountService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITrainingService, TrainingService>();
            container.RegisterType<IPrerequisiteService, PrerequisiteService>();
            container.RegisterType<IPrerequisiteRepository, PrerequisiteRepository>();
            container.RegisterType<IEnrollmentRepository, EnrollmentRepository>();
            container.RegisterType<IEnrollementService, EnrollmentService>();
            container.RegisterType<IAttachmentService, AttachmentService>();
            container.RegisterType<IAttachmentRepository, AttachmentRepository>();
            container.RegisterType<ITrainingRepository, TrainingRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IPendingAccountRepository, PendingAccountRepository>();
            container.RegisterType<IInAppNotificationRepository, InAppNotificationRepository>();
            container.RegisterType<IInAppNotificationService, InAppNotificationService>();
            container.RegisterInstance<IValidatorRule<PendingAccount>>("Unique Email Validation", new UniqueEmailValidatorRule(container.Resolve<IAccountService>(), container.Resolve<IPendingAccountService>()));
            container.RegisterInstance<IValidatorRule<PendingAccount>>("Unique NIC Validation", new UniqueNicValidatorRule(container.Resolve<IPendingAccountService>(), container.Resolve<IUserService>()));
            container.RegisterType(typeof(IValidatorService<PendingAccount>), typeof(ValidatorService<PendingAccount>),
            new InjectionConstructor(new ResolvedArrayParameter<IValidatorRule<PendingAccount>>(
                new ResolvedParameter<IValidatorRule<PendingAccount>>("Unique Email Validation"),
                new ResolvedParameter<IValidatorRule<PendingAccount>>("Unique NIC Validation")
            )));
            container.RegisterType(typeof(IValidatorService<>), typeof(ValidatorService<>));

            container.RegisterInstance<INotificationHandler>("Email Notification", new EmailNotificationHandler(container.Resolve<IAccountRepository>()));
            container.RegisterInstance<INotificationHandler>("In-App Notification", new InAppNotificationHandler(container.Resolve<IInAppNotificationRepository>()));
            container.RegisterType(typeof(INotificationManager), typeof(NotificationManager),
            new InjectionConstructor(new ResolvedArrayParameter<INotificationHandler>(
                new ResolvedParameter<INotificationHandler>("Email Notification"),
                new ResolvedParameter<INotificationHandler>("In-App Notification")
            )));

            Container = container;
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}