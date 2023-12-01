using Microsoft.Win32;
using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.DAL;
using SkillslabAssignment.Common.Entities;
using SkillslabAssignment.Common.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IDbConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=assignment;Integrated Security=True;TrustServerCertificate=True");
                GenericRepository<Department> departmentRepository = new GenericRepository<Department>(connection);
                Department department = new Department() { Name = "Edoo Department 11hellloooo this is to try something 1", Description = "This is a trial descrition aaaaaaaaaa", Id = 3 };
                DateTime start = DateTime.Now;
                //List<Department> departmentList = new List<Department>
                DateTime end = DateTime.Now;
                Console.WriteLine((end - start).TotalMilliseconds);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //static void trial(object parameters = null)
            //{
            //    if (parameters != null)
            //    {
            //        foreach (var property in parameters.GetType().GetProperties())
            //        {
            //            var columnAttribute = property.GetCustomAttributes(typeof(ColumnAttribute), true)
            //               .FirstOrDefault() as ColumnAttribute;
            //            string columnName = columnAttribute?.Name ?? property.Name;
            //            Console.WriteLine($"@{columnName}", property.GetValue(parameters));
            //        }
            //    }
            //}

            //GenericRepository<Trial> trialRepo = new GenericRepository<Trial>(connection);
            //Trial trial = new Trial() { Age = 1000, Name = "Hello my name is edoo junaid qqqqqqqqqq" };
            //trial.Id = 12;

            //        trialRepo.Update(trial);
            //        Console.Read();
        }
    }
}
