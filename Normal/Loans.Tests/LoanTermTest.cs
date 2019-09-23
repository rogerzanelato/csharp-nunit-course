using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    //[Ignore("Need to complete update work.")] // Ignorar toda classe de teste
    public class LoanTermTest
    {
        [Test]
        [Ignore("Need to complete update work.")]
        public void ThisTestShouldBeIgnored()
        {
            
        }

        [Test]
        public void ShouldReturnTermInMonths()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12), "Months should be 12 * number of years");
        }

        [Test]
        public void ShouldReturnYear()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void ShouldThrowExceptionOnZeroOrMinorYear()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            // Garante a descrição da exceção
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                                                .With
                                                .Property("Message")
                                                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));            // Garante a descrição da exceção

            // igual de cima, mas diferente
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                                                .With
                                                .Message
                                                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));

            // if we don't about the message, but want the correct parameter to be validated
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                                                .With
                                                .Property("ParamName")
                                                .EqualTo("years"));

            // outra maneira de fazer a mesma coisa
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                                                .With
                                                .Matches<ArgumentOutOfRangeException>(
                                                    ex => ex.ParamName == "years"));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ShouldHaveSameReference()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);

            // valida por referência
            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c));

            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            Assert.That(y, Is.SameAs(x));
            Assert.That(z, Is.Not.SameAs(x));
        }

        [Test]
        public void TestAFloatPointValue()
        {
            double a = 1.0 / 3.0;

            // especifica que o resultado pode ter uma margem de erro de 0.004
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void TestStringsContainsSample()
        {
            string name = "Sarah";

            Assert.That(name, Does.StartWith("Sa"));
            Assert.That(name, Does.EndWith("ah"));
            Assert.That(name, Does.Contain("ara"));
            Assert.That(name, Does.Not.Contain("mit"));
            Assert.That(name, Does.StartWith("Sa")
                                .And
                                .EndsWith("rah"));
            Assert.That(name, Does.StartWith("aidyohasd")
                                .Or
                                .EndsWith("rah"));

        }

        [Test]
        public void TestNumberRangeSample()
        {
            int i = 42;

            Assert.That(i, Is.Not.GreaterThan(42));
            Assert.That(i, Is.GreaterThanOrEqualTo(42));
            Assert.That(i, Is.Not.LessThan(42));
            Assert.That(i, Is.LessThanOrEqualTo(42));
            Assert.That(i, Is.InRange(40, 50));
        }

        [Test]
        public void TestDateWithinRange()
        {
            DateTime d1 = new DateTime(2000, 2, 20);
            DateTime d2 = new DateTime(2000, 2, 25);

            //Assert.That(d1, Is.EqualTo(d2)); // fail
            Assert.That(d1, Is.Not.EqualTo(d2));
            Assert.That(d1, Is.Not.EqualTo(d2).Within(4).Days);
            Assert.That(d1, Is.EqualTo(d2).Within(6).Days);
        }
    }
}
