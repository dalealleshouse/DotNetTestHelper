//  --------------------------------
//  <copyright file="TestDbSet.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/27/2014</date>
//  ---------------------------------
namespace DotNetTestHelper.EntityFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "TestDbSet matches DbSet")]
    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
        where TEntity : class
    {
        private readonly ObservableCollection<TEntity> _data;

        private readonly IQueryable _query;

        public TestDbSet()
        {
            this._data = new ObservableCollection<TEntity>();
            this._query = this._data.AsQueryable();
        }

        public override ObservableCollection<TEntity> Local
        {
            get
            {
                return this._data;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Justification = "Test class")]
        Type IQueryable.ElementType
        {
            get
            {
                return this._query.ElementType;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Justification = "Test class")]
        Expression IQueryable.Expression
        {
            get
            {
                return this._query.Expression;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Justification = "Test class")]
        IQueryProvider IQueryable.Provider
        {
            get
            {
                return new TestDbAsyncQueryProvider<TEntity>(this._query.Provider);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Justification = "Test class")]
        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<TEntity>(this._data.GetEnumerator());
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Justification = "Test class")]
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return this._data.GetEnumerator();
        }

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Justification = "Test class")]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._data.GetEnumerator();
        }

        public override TEntity Add(TEntity entity)
        {
            this._data.Add(entity);
            return entity;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            var added = new Collection<TEntity>();

            foreach (var entity in entities)
            {
                added.Add(this.Add(entity));
            }

            return added;
        }

        public override TEntity Remove(TEntity entity)
        {
            this._data.Remove(entity);
            return entity;
        }

        public override TEntity Attach(TEntity entity)
        {
            this._data.Add(entity);
            return entity;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }
}