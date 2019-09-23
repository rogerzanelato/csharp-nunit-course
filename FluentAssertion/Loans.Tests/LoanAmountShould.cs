using Loans.Domain.Applications.Values;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanAmountShould
    {
        [Test]
        public void StoreCurrencyCode()
        {
            var sut = new LoanAmount("USD", 100_000);

            Assert.That(sut.CurrencyCode, Is.EqualTo("USD"));
        }
    }
}
