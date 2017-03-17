
namespace Anycmd.Tests.UqmlTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnifiedQueries;
    using UnifiedQueries.Compilers;

    [TestClass]
    public class UnifiedQueriesTest
    {
        [TestMethod]
        public void QueryTest()
        {
            // FirstName StartWith "Peter" And not (LastName.Contains "r" Or YearlyIncome LessThanOrEqualTo 30000)
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "QuerySpecificationSample.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers();
            foreach (var customer in customers.Where(compiler.Compile(querySpecification).Compile()))
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(50000, customer.YearlyIncome);
                Console.WriteLine(
                    @"FirstName: {0}, LastName: {1}, YearlyIncome: {2}",
                    customer.FirstName,
                    customer.LastName,
                    customer.YearlyIncome);
            }
        }

        private static IEnumerable<Customer> GetAllCustomers()
        {
            return new[]
                       {
                           new Customer { FirstName = "Sunny", LastName = "Chen", YearlyIncome = 10000 },
                           new Customer { FirstName = "PeterJam", LastName = "Yo", YearlyIncome = 10000 },
                           new Customer { FirstName = "PeterR", LastName = "Ko", YearlyIncome = 50000 },// 命中行
                           new Customer { FirstName = "FPeter", LastName = "Law", YearlyIncome = 70000 },
                           new Customer { FirstName = "Jim", LastName = "Peter", YearlyIncome = 30000 }
                       };
        }
    }
}
