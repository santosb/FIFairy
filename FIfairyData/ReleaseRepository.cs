using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIfairyDomain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

namespace FIfairyData
{
    public class ReleaseRepository : IReleaseRepository
    {
        private const string DbFile = "FIFairy.db";

        #region IReleaseRepository Members

        public IEnumerable<Release> GetReleases()
        {            
            ISessionFactory sessionRepository = CreateSessionFactory();
            using (ISession session = sessionRepository.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    return session.CreateCriteria<Release>().List<Release>();
                }
            }
        }

        public IEnumerable<Release> GetReleases(DateTime dateTo)
        {
            return GetReleases().Where(x => x.ReleaseDate  > dateTo);
        }

        public Release GetReleaseDetails(string releaseNumber)
        {
            ISessionFactory sessionRepository = CreateSessionFactory();
            using (ISession session = sessionRepository.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var releaseDetails =
                        session.CreateCriteria<Release>()
                            .Add(Restrictions.Eq("ReleaseNumber", releaseNumber))
                            .UniqueResult<Release>();

                    return new Release
                               {
                                   TeamName = releaseDetails.TeamName,
                                   ReleaseNumber = releaseDetails.ReleaseNumber,
                                   PrePatEmail = releaseDetails.PrePatEmail,
                                   ReleaseFiInstructions = releaseDetails.ReleaseFiInstructions,
                                   ServiceNowTicketLink = releaseDetails.ServiceNowTicketLink
                               };
                }
            }
        }

        public void SaveReleaseDetails(Release release)
        {
            release.ReleaseDate = DateTime.Today;
            // create our NHibernate session factory            
            ISessionFactory sessionRepository = CreateSessionFactory();
            using (ISession session = sessionRepository.OpenSession())
            {
                // populate the database
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(release);                    
                    transaction.Commit();
                }
            }
        }

        #endregion

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
                return;

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
                .Create(false, true);
        }
    }
}