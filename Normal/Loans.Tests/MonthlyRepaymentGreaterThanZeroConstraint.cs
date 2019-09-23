using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loans.Domain.Applications;
using NUnit.Framework.Constraints;

namespace Loans.Tests
{
    public class MonthlyRepaymentGreaterThanZeroConstraint : Constraint
    {
        public string ExpectedProductName { get; private set; }
        public decimal ExpectedInterestRate { get; private set; }

        public MonthlyRepaymentGreaterThanZeroConstraint(string expectedProductName, decimal expectedInterestRate)
        {
            ExpectedProductName = expectedProductName;
            ExpectedInterestRate = expectedInterestRate;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            MonthlyRepaymentComparison comparison = actual as MonthlyRepaymentComparison;

            if (comparison is null)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            }

            if (comparison.InterestRate == ExpectedInterestRate &&
                comparison.ProductName == ExpectedProductName &&
                comparison.MonthlyRepayment > 0)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Success);
            }

            return new ConstraintResult(this, actual, ConstraintStatus.Failure);
        }
    }
}
