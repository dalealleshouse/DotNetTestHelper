//  --------------------------------
//  <copyright file="SutBuilderShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/03/2014</date>
//  ---------------------------------

namespace DotNetTestHelper.Tests.SutBuilder
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SutBuilderShould
    {
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ThrowIfAnyConstructorArgumentIsNotAnInterface()
        {
            var sut = new SutBuilder<TestTypeWithNonInterfaceArgument>();
            sut.Build();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ThrowIfMultipleConstructor()
        {
            var sut = new SutBuilder<TestTypeWithMultipleConstructors>();
            sut.Build();
        }

        [TestMethod]
        public void BuildClassWithNoConstructorArguments()
        {
            var sut = new SutBuilder<TestType>();

            var result = sut.Build();
            Assert.IsInstanceOfType(result, typeof(TestType));
        }

        [TestMethod]
        public void BuildClassWithConstructorArgumentsUsingMocks()
        {
            var sut = new SutBuilder<TestTypeWithConstructorArguments>();

            var result = sut.Build();
            Assert.IsInstanceOfType(result, typeof(TestTypeWithConstructorArguments));
        }

        [TestMethod]
        public void BuildClassWithSingleOverriddenArgument()
        {
            var expected = new TestType { Identifier = "BuildClassWithSingleOverriddenArgument" };

            var sut = new SutBuilder<TestTypeWithConstructorArguments>();
            var result = sut.AddDependency(expected, "testType").Build();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.TestType);
        }

        [TestMethod]
        public void BuildClassWithAllOverriddenArgument()
        {
            var expected = new TestType { Identifier = "BuildClassWithSingleOverriddenArgument" };
            var expected2 = new TestType2 { Identifier = "BuildClassWithSingleOverriddenArgument2" };

            var sut = new SutBuilder<TestTypeWithConstructorArguments>();
            var result = sut.AddDependency(expected, "testType").AddDependency(expected2, "testType2").Build();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.TestType);
            Assert.AreEqual(expected2, result.TestType2);
        }

        [TestMethod]
        public void AutoDiscoverParameterNameOfDependency()
        {
            var expected = new TestType { Identifier = "Auto discovered parameter name" };
            var result = new SutBuilder<TestTypeWithConstructorArguments>().AddDependency(expected).Build();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Identifier, result.TestType.Identifier);
            Assert.IsNull(result.TestType2.Identifier);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ThrowIfNoParameterOfMatchingTypeFound()
        {
            new SutBuilder<TestType>().AddDependency(new TestType2()).Build();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ThrowIfAddingADependencyWithNoNameAndMultiplePossibilitiesExist()
        {
            new SutBuilder<TestTypeWithSameArguments>().AddDependency(new TestType()).Build();
        }
    }
}