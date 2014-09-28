//  --------------------------------
//  <copyright file="TestDbAsyncEnumerator.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper.EntityFramework
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this._inner = inner;
        }

        public T Current
        {
            get
            {
                return this._inner.Current;
            }
        }

        object IDbAsyncEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public void Dispose()
        {
            this._inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this._inner.MoveNext());
        }
    }
}