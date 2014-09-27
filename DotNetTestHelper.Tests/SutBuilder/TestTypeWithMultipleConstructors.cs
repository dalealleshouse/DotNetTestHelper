//  --------------------------------
//  <copyright file="TestTypeWithMultipleConstructors.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper.Tests.SutBuilder
{
    using System.Diagnostics.CodeAnalysis;

    public class TestTypeWithMultipleConstructors
    {
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Test Code")]
        private readonly ITestType _testType;

        public TestTypeWithMultipleConstructors()
        {
        }

        public TestTypeWithMultipleConstructors(ITestType testType)
        {
            this._testType = testType;
        }
    }
}