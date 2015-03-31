//  --------------------------------
//  <copyright file="ITestDependency.cs">
//      Copyright (c) 2015 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>03/31/2015</date>
//  ---------------------------------

namespace DotNetTestHelper.Tests.SutBuilder
{
    public interface ITestDependency
    {
        string Identifier { get; set; }
    }
}