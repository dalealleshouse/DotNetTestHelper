//  --------------------------------
//  <copyright file="TestTypeWithNonInterfaceArgument.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper.Tests.SutBuilder
{
    using System.Diagnostics.CodeAnalysis;

    public class TestTypeWithNonInterfaceArgument
    {
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Test Code")]
        private readonly TestType _testType;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Test Code")]
        private readonly ITestType _testType2;

        public TestTypeWithNonInterfaceArgument(TestType testType, ITestType testType2)
        {
            this._testType = testType;
            this._testType2 = testType2;
        }
    }
}