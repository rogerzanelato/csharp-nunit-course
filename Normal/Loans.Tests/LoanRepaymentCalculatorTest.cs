using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanRepaymentCalculatorTest
    {
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepayment(
            decimal principal,
            decimal interestRate,
            int termInYears,
            decimal expectedMonthlyPayment)
        {
            var calculator = new LoanRepaymentCalculator();

            var monthlyPayment = calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment_Simplified(
            decimal principal,
            decimal interestRate,
            int termInYears)
        {
            var calculator = new LoanRepaymentCalculator();

            return calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(
            decimal principal,
            decimal interestRate,
            int termInYears,
            decimal expectedMonthlyPayment)
        {
            var calculator = new LoanRepaymentCalculator();

            var monthlyPayment = calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { "Data.csv" } )]
        public void CalculateCorrectMonthlyRepayment_Csv(
            decimal principal,
            decimal interestRate,
            int termInYears,
            decimal expectedMonthlyPayment)
        {
            var calculator = new LoanRepaymentCalculator();

            var monthlyPayment = calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(
            decimal principal,
            decimal interestRate,
            int termInYears)
        {
            var calculator = new LoanRepaymentCalculator();

            return calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Combinatorial(
            [Values(100_000, 200_000, 500_000)] decimal principal,
            [Values(6.5, 10, 20)] decimal interestRate,
            [Values(10, 20, 30)] int termInYears)
        {
            // Irá gerar 27 testes
            // o teste no formato acima, irá testar a combinação de todos os parâmetros.
            // ideal para quando queremos efetuar um grande número de teste, para verificar se uma exceção será gerada

            var calculator = new LoanRepaymentCalculator();

            var monthlyPayment = calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        [Sequential]
        public void CalculateCorrectMonthlyRepayment_Sequential(
            [Values(200_000, 200_000, 500_000)] decimal principal,
            [Values(6.5, 10, 10)] decimal interestRate,
            [Values(30, 30, 30)] int termInYears,
            [Values(1264.14, 1755.14, 4387.86)] decimal excpectedMonthlyPayment)
        {
            // Dessa forma os parâmetros de teste serão atribuídos em sequência

            var calculator = new LoanRepaymentCalculator();

            var monthlyPayment = calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(excpectedMonthlyPayment));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Range(
            [Range(50_000, 1_000_000, 50_000)] decimal principal,
            [Range(0.5, 20.00, 0.5)] decimal interestRate,
            [Values(10, 20, 30)] int termInYears)
        {
            // Irá criar um intervalo

            var calculator = new LoanRepaymentCalculator();

            var monthlyPayment = calculator.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }
    }
}
