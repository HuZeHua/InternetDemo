
namespace Anycmd.Tests.UqmlTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using UnifiedQueries;
    using UnifiedQueries.Compilers;

    [TestClass]
    public class RelationalOperatorsTest
    {
        [TestMethod]
        public void StringEqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "StringEqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(50000, customer.YearlyIncome);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void Int32EqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "Int32EqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31, customer.Int32Age);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void UInt32EqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "UInt32EqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31u, customer.UInt32Age);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void Int64EqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "Int64EqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31, customer.Int64Field);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void UInt64EqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "UInt64EqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31u, customer.UInt64Field);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void Int16EqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "Int16EqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31, customer.Int16Field);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void UInt16EqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "UInt16EqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31, customer.UInt16Field);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void BooleanEqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "BooleanEqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual(true, customer.IsVendor);
            }
            Assert.AreEqual(2, customers.Count());
        }

        [TestMethod]
        public void FloatEqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "FloatEqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31.0f, customer.FloatField);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void DoubleEqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "DoubleEqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31.0d, customer.FloatField);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void CharEqualTo()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "CharEqualTo.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual('x', customer.CharField);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void Int32GreaterThan()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "Int32GreaterThan.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31, customer.Int32Age);
            }
            Assert.AreEqual(1, customers.Count());
        }

        [TestMethod]
        public void UInt32GreaterThan()
        {
            var querySpecification = QuerySpecification.LoadFromFile(Consts.Path + "UInt32GreaterThan.xml");
            var compiler = new LambdaExpressionCompiler<Customer>();
            var customers = GetAllCustomers().Where(compiler.Compile(querySpecification).Compile()).ToList();
            foreach (var customer in customers)
            {
                Assert.AreEqual("PeterR", customer.FirstName);
                Assert.AreEqual(31u, customer.UInt32Age);
            }
            Assert.AreEqual(1, customers.Count());
        }

        private static IEnumerable<Customer> GetAllCustomers()
        {
            return new[]{ 
                new Customer 
                { 
                    FirstName = "Sunny", LastName = "Chen", 
                    YearlyIncome = 10000, Int32Age=20, UInt32Age = 20, 
                    Int64Field = 20, UInt64Field = 20, Int16Field = 20,
                    UInt16Field = 20, IsVendor = false
                },
                new Customer
                {
                    FirstName = "PeterJam", LastName = "Yo", 
                    YearlyIncome = 10000, Int32Age=30, UInt32Age = 30, 
                    Int64Field = 30, UInt64Field = 30, Int16Field = 30,
                    UInt16Field = 30, IsVendor = false
                },
                new Customer
                {
                    FirstName = "PeterR", LastName = "Ko", 
                    YearlyIncome = 50000, Int32Age=31, UInt32Age = 31, 
                    Int64Field = 31, UInt64Field = 31, Int16Field = 31,
                    UInt16Field = 31, IsVendor = false, FloatField = 31.0f, 
                    DoubleField = 31.0d, CharField = 'x'
                },// 命中行
                new Customer
                {
                    FirstName = "FPeter", LastName = "Law", 
                    YearlyIncome = 70000, Int32Age=20, UInt32Age = 20, 
                    Int64Field = 20, UInt64Field = 20, Int16Field = 20,
                    UInt16Field = 20, IsVendor = true
                },
                new Customer
                {
                    FirstName = "Jim", LastName = "Peter", 
                    YearlyIncome = 30000, Int32Age=22, UInt32Age = 22, 
                    Int64Field = 22, UInt64Field = 22, Int16Field = 22,
                    UInt16Field = 22, IsVendor = true
                }
            };
        }
    }
}
