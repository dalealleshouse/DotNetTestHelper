//  --------------------------------
//  <copyright file="DynamicMock.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper
{
    using System;
    using System.Linq;

    using Moq;

    public static class DynamicMock
    {
        public static object Mock(Type type)
        {
            var constructorInfo = typeof(Mock<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes);
            if (constructorInfo != null)
            {
                var mock = constructorInfo.Invoke(new object[] { });
                return mock.GetType().GetProperties().Single(f => f.Name == "Object" && f.PropertyType == type).GetValue(mock, new object[] { });
            }

            return null;
        }
    }
}