using LZ.DataLayer.Billing.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.LibraryManagement.Repository
{
    public class RepositoryTestBase : RepositoryTestFixture
    {
        protected Dictionary<string, List<object>> CreatedEntities { get; }
        public RepositoryTestBase() : base()
        {
            CreatedEntities = new Dictionary<string, List<object>>();
        }

        protected virtual T Create<T>(Action<T> predicate = null) where T : class, new()
        {
            var obj = new T();

            predicate?.Invoke(obj);
            using (LibraryContextMemory context = new LibraryContextMemory(_configuration))
            {
                context.Add<T>(obj);
                context.SaveChanges();
            }

            if (CreatedEntities.ContainsKey(typeof(T).Name))
            {
                var value = CreatedEntities[typeof(T).Name];
                value.Add(obj);
            }
            else
            {
                CreatedEntities.Add(typeof(T).Name, new List<object> { obj });
            }
            return obj;
        }

        protected T Create<T>(T obj) where T : class, new()
        {
            using (var context = new LibraryContextMemory(_configuration))
            {
                context.Add<T>(obj);
                context.SaveChanges();
            }

            if (CreatedEntities.ContainsKey(typeof(T).Name))
            {
                var value = CreatedEntities[typeof(T).Name];
                value.Add(obj);
            }
            else
            {
                CreatedEntities.Add(typeof(T).Name, new List<object> { obj });
            }
            return obj;
        }

    }
}
