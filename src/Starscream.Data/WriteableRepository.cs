using System;
using System.Collections.Generic;
using System.Linq;
using Starscream.Domain;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using NHibernate;

namespace Starscream.Data
{
    public class WriteableRepository : IWriteableRepository
    {
        readonly ISession _session;

        public WriteableRepository(ISession session)
        {
            _session = session;
        }

        public T Create<T>(T itemToCreate) where T : IEntity
        {
            _session.Save(itemToCreate);
            return itemToCreate;
        }

        public void DeleteAll<T>() where T : class, IEntity
        {
            foreach (var item in _session.QueryOver<T>().List())
            {
                Delete<T>(item.Id);
            }
        }

        public IEnumerable<T> CreateAll<T>(IEnumerable<T> list) where T : IEntity
        {
            var items = list as List<T> ?? list.ToList();
            foreach (var item in items)
            {
                Create(item);
            }

            return items;
        }

        public T Update<T>(T itemToUpdate) where T : IEntity
        {
            var session = _session;
            session.Update(itemToUpdate);
            return itemToUpdate;
        }

        public void Delete<T>(Guid itemId) where T : IEntity
        {
            var itemToDelete = _session.Get<T>(itemId);
            _session.Delete(itemToDelete);
        }


    }
}