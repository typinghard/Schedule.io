﻿using Raven.Client.Documents.Session;
using Schedule.io.Interfaces.Repositories;
using System;

namespace Schedule.io.Infra.RavenDB.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;

        public UnitOfWork(IDocumentSession session)
        {
            _session = session;
        }

        public bool Commit()
        {
            try
            {
                _session.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
