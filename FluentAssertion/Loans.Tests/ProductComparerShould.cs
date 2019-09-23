using FluentAssertions;
using FluentAssertions.Execution;
using Loans.Domain.Applications;
using Loans.Domain.Applications.Values;
using NUnit.Framework;
using System.Collections.Generic;

namespace Loans.Tests
{
    [ProductComparison]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Simulate long setup init time for this list of products
            // We assume that this list will not be modified by any tests
            // as this will potentially break other tests (i.e. break test isolation)
            products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };
        }


        [SetUp]
        public void Setup()
        {
            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }


        [Test]        
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Assert.That(comparisons, Has.Exactly(3).Items);

            comparisons.Should().HaveCount(3);
        }


        [Test]
        public void NotReturnDuplicateComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Assert.That(comparisons, Is.Unique);
            comparisons.Should().OnlyHaveUniqueItems();
        }


        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            // Need to also know the expected monthly repayment
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            //Assert.That(comparisons, Does.Contain(expectedProduct));

            comparisons.Should().Contain(expectedProduct);
        }


        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons,
                        Has.Exactly(1)
                           .Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));
        }

        [Test]
        public void ReturnNonNullNonEmptyComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            comparisons.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ReturnComparisonsSortedByProductName()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            comparisons.Should().BeInAscendingOrder(x => x.ProductName);
        }

        [Test]
        public void AssertionScopes()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // Eefetua todas as validações, mesmo que uma falhe
            using (new AssertionScope())
            {
                comparisons.Should().NotBeNullOrEmpty();
                comparisons.Should().HaveCount(3);
                comparisons.Should().OnlyHaveUniqueItems();
                comparisons.Should().Contain(new MonthlyRepaymentComparison("a", 1, 643.28m));
                comparisons.Should().BeInAscendingOrder(x => x.ProductName);
            }
        }

        [Test]
        public void AssertionScopes_FluentChained()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // Eefetua todas as validações, mesmo que uma falhe
            using (new AssertionScope())
            {
                comparisons.Should().NotBeNullOrEmpty()
                    .And
                    .HaveCount(3)
                    .And
                    .OnlyHaveUniqueItems()
                    .And
                    .Contain(new MonthlyRepaymentComparison("a", 1, 643.28m))
                    .And
                    .BeInAscendingOrder(x => x.ProductName);
            }
        }
    }
}
