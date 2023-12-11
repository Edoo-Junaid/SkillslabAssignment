using SkillslabAssigment.DAL.DAL;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            container.RegisterType(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            container.RegisterType(typeof(IGenericService<,>), typeof(GenericService<,>));
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
            container.RegisterType<IStorrageService, FirebaseStorageService>(
                new InjectionConstructor(bucketString)
                );
            container.RegisterType<IAttachmentService, AttachmentService>();
            container.RegisterType<IAttachmentRepository, AttachmentRepository>();
            container.RegisterType<ITrainingRepository, TrainingRepository>();
            container.RegisterType<IPendingAccountRepository, PendingAccountRepository>();
            container.RegisterType(typeof(IValidatorService<>), typeof(ValidatorService<>));
            Container = container;
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}