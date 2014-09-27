//  --------------------------------
//  <copyright file="TestTypeWithConstructorArguments.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper.Tests.SutBuilder
{
    using System;

    public class TestTypeWithConstructorArguments
    {
        private readonly ITestType _testType;

        private readonly ITestType2 _testType2;

        public TestTypeWithConstructorArguments(ITestType testType, ITestType2 testType2)
        {
            if (testType == null)
            {
                throw new ArgumentNullException("testType");
            }

            if (testType2 == null)
            {
                throw new ArgumentNullException("testType2");
            }

            this._testType = testType;
            this._testType2 = testType2;
        }

        public ITestType TestType
        {
            get
            {
                return this._testType;
            }
        }

        public ITestType2 TestType2
        {
            get
            {
                return this._testType2;
            }
        }
    }
}