using FluentAssertions;
using Loans.Domain.Applications.Values;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanAmountShould
    {
        [Test]
        public void StoreCurrencyCode()
        {
            var loanAmout = new LoanAmount("USD", 100_000);

            //Assert.That(sut.CurrencyCode, Is.EqualTo("USD"));
            loanAmout.CurrencyCode.Should().Be("USD"); // Sensitive
            loanAmout.CurrencyCode.Should().BeEquivalentTo("usd"); // Insensitive

            loanAmout.CurrencyCode.Should().Contain("S");
            loanAmout.CurrencyCode.Should().StartWith("U");
            loanAmout.CurrencyCode.Should().EndWith("D");

            loanAmout.CurrencyCode.Should().BeOneOf("AUD", "GBP", "USD");

            loanAmout.CurrencyCode.Should().Match("*D");
            //loanAmout.CurrencyCode.Should().Match("*S");
            loanAmout.CurrencyCode.Should().MatchRegex("[A-Z]{3}");
        }
    }
}
