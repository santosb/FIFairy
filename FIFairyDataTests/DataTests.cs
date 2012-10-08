﻿using System;
using System.IO;
using System.Reflection;
using FIfairyData;
using FIfairyDomain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace FIFairyDataTests
{
    [TestFixture]
    public class DataTests
    {
        private const string DbFile = "FIFairyTests.db";

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard.UsingFile(DbFile)
                )
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<ReleaseRepository>())                          
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists(DbFile))
                File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
                .Create(false, true);
        }


        [Test]
        public void CanCorrectlyMapReleaseDetails()
        {
            ISessionFactory sessionFactory = CreateSessionFactory();

            using (ISession session = sessionFactory.OpenSession())
            {
                new PersistenceSpecification<ReleaseDetailsModel>(session)
                    .CheckProperty(c => c.TeamName, "ENZO")
                    .CheckProperty(c => c.ReleaseNumber, "REL00001")                    
                    .CheckProperty(c => c.ReleaseFiInstructions, "some FI instructions")
                    .CheckProperty(c => c.PrePatEmail, "prepat email")
                    .CheckProperty(c => c.ServiceNowTicketLink, "www.someurl.com")
                    .VerifyTheMappings();
            }
        }
    }
}