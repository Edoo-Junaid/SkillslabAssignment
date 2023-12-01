using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var validator = new Validator<PendingAccount>();

            var account = new PendingAccount { };

            if (validator.TryValidate(account, out var validationResults))
            {
                Console.WriteLine("Validation passed!");
            }
            else
            {
                Console.WriteLine("Validation failed:");
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                }
            }

            Console.ReadKey();
        }
    }
}
