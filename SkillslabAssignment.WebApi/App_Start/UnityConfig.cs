using SkillslabAssigment.DAL.DAL;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Interface;
using SkillslabAssignment.Service;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Injection;
using System.Configuration;
namespace SkillslabAssignment.WebApi
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; internal set; }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            string connectionString = ConfigurationManager.ConnectionStrings["MSSQLServerConnection"].ConnectionString;
            container.RegisterType<IDbConnection, SqlConnection>(
                new InjectionConstructor(connectionString)
            );
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            container.RegisterType(typeof(IGenericService<>), typeof(GenericService<>));
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
            container.RegisterType<IStorrageService, FirebaseStorageService>();
            Container = container;
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}