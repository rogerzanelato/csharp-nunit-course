using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    public class ProductCompararTest
    {
        private List<LoanProduct> _products;
        private ProductComparer _comparer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Simulate long setup init for this list of products
            // We assume that this list will not be modified by any tests
            // as this will potentially break other ters (i.e. break test isolation)
            _products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Run after last test in this test class (fixture) executes
            // e. g. disposing of shared expensive setup performed in OneTimeSetUp

            // products.Dispose(); e.g if products implemented IDisposable
        }

        [SetUp]
        public void Setup()
        {
            _comparer = new ProductComparer(new LoanAmount("USD", 200_000m), _products);
        }

        [TearDown]
        public void TearDown()
        {
            // Runs after each test executes
        }

        [Test]
        public void ShouldReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = _comparer.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void CollectionShouldNotHaveDuplicates()
        {
            List<MonthlyRepaymentComparison> comparisons = _comparer.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ComparisonsContainsExpectedProduct()
        {
            List<MonthlyRepaymentComparison> comparisons = _comparer.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ComparisonsContainsExpectedProduct_WithPartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons = _comparer.CompareMonthlyRepayments(new LoanTerm(30));

            // don't care about the expected monthly repayment, only that the product is there
            Assert.That(comparisons, Has.Exactly(1)
                                        .Property("ProductName").EqualTo("a")
                                        .And
                                        .Property("InterestRate").EqualTo(1)
                                        .And
                                        .Property("MonthlyRepayment").GreaterThan(0));

            // Mesma forma de fazer \/
            Assert.That(comparisons, Has.Exactly(1)
                                        .Matches<MonthlyRepaymentComparison>(
                                            item => item.ProductName == "a" &&
                                                    item.InterestRate == 1 &&
                                                    item.MonthlyRepayment > 0));
        }

        [Test]
        public void ComparisonsContainsExpectedProduct_WithPartialKnownExpectedValues_V2()
        {
            List<MonthlyRepaymentComparison> comparisons = _comparer.CompareMonthlyRepayments(new LoanTerm(30));

            //Assert.That(comparisons, Has.Exactly(1)
            //                            .Property("ProductName").EqualTo("a")
            //                            .And
            //                            .Property("InterestRate").EqualTo(1)
            //                            .And
            //                            .Property("MonthlyRepayment").GreaterThan(0));

            Assert.That(comparisons,
                Has.Exactly(1)
                    .Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));
        }
    }
}
