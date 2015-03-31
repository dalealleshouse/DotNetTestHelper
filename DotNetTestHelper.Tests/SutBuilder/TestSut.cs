//  --------------------------------
//  <copyright file="TestSut.cs">
//      Copyright (c) 2015 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>03/31/2015</date>
//  ---------------------------------

namespace DotNetTestHelper.Tests.SutBuilder
{
    using System;

    public class TestSut
    {
        private readonly ITestDependency _testDependency;

        public TestSut()
        {
        }

        public TestSut(ITestDependency testDependency)
        {
            if (testDependency == null)
            {
                throw new ArgumentNullException("testDependency");
            }

            this._testDependency = testDependency;
        }

        public ITestDependency Dependency
        {
            get
            {
                return this._testDependency;
            }
        }
    }
}