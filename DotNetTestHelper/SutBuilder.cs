//  --------------------------------
//  <copyright file="SutBuilder.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class SutBuilder<T>
    {
        private readonly Dictionary<string, object> _dependencies;

        public SutBuilder()
        {
            this._dependencies = new Dictionary<string, object>();
        }

        public SutBuilder<T> AddDependency<TDep>(TDep dependency, string name)
        {
            this._dependencies.Add(name, dependency);
            return this;
        }

        public SutBuilder<T> AddDependency<TDep>(TDep dependency)
        {
            var con = this.GetConstructor();
            var typeParams = con.GetParameters().Where(p => p.ParameterType.IsInstanceOfType(dependency));

            var parameters = typeParams as ParameterInfo[] ?? typeParams.ToArray();
            if (typeParams == null || !parameters.Any())
            {
                throw new InvalidOperationException("Unable to find a constructor argument of the specified type");
            }

            if (parameters.Count() > 1)
            {
                throw new InvalidOperationException("There are multiple constructor arguments of the specified type. Please specify the name of the arguments.");
            }

            this.AddDependency(dependency, parameters.First().Name);
            return this;
        }

        public T Build()
        {
            var constructor = this.GetConstructor();

            var args =
                constructor.GetParameters()
                    .Select(p => this._dependencies.ContainsKey(p.Name) ? this._dependencies[p.Name] : DynamicMock.Mock(p.ParameterType))
                    .ToArray();

            return (T)constructor.Invoke(args);
        }

        private ConstructorInfo GetConstructor()
        {
            var type = typeof(T);

            var constructors = type.GetConstructors();

            if (constructors.Count() != 1)
            {
                throw new InvalidOperationException("Unable to construct a type with multiple or no constructors.");
            }

            var constructor = constructors.First();
            var conParams = constructor.GetParameters();

            if (conParams.Any(p => !p.ParameterType.IsInterface))
            {
                throw new InvalidOperationException("All parameters must be interfaces to use this class.");
            }

            return constructor;
        }
    }
}