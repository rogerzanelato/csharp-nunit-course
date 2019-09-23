using System;
using NUnit.Framework;

namespace Loans.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ProductComparisonAttribute : CategoryAttribute
    {
        
    }
}
