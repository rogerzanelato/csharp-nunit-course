using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using FluentAssertions;
using Loans.Domain;
using FluentAssertions.Types;
using System.Reflection;
using Loans.Domain.Applications.Values;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Loans.Tests
{
    public class TechnicalTests
    {
        [Test]
        public void InterfaceImplementation()
        {
            typeof(IdentityVerifierServiceGateway).Should().Implement<IIdentityVerifier>();
        }

        [Test]
        public void DerivedFrom()
        {
            typeof(LoanAmount).Should().BeDerivedFrom<ValueObject>();
        }

        [Test]
        public void AllValueObjectsInCorrectNamespace()
        {
            Assembly productionCodeAssembly = typeof(LoanAmount).Assembly;

            IEnumerable<Type> valueObjectTypes = AllTypes.From(productionCodeAssembly)
                                              .ThatDeriveFrom<ValueObject>();

            valueObjectTypes.Should().OnlyContain(x => x.Namespace == "Loans.Domain.Applications.Values");
        }

        [Test]
        public void NamespaceContainsOnlyValueObjects()
        {
            Assembly productionCodeAssembly = typeof(LoanAmount).Assembly;

            IEnumerable<Type> nonValueObjectTypesInNameSpace = AllTypes.From(productionCodeAssembly)
                                          .ThatAreInNamespace("Loans.Domain.Applications.Values")
                                          .ThatDoNotDeriveFrom<ValueObject>()
                                          .Where(x => x.GetCustomAttribute<CompilerGeneratedAttribute>() == null);

            nonValueObjectTypesInNameSpace.Should().BeEmpty();
        }
    }
}

