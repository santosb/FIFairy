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

        public IEnumerable<ReleaseModel> GetReleases()
        {
            var releaseModels = new List<ReleaseModel>();
            ISessionFactory sessionRepository = CreateSessionFactory();
            using (ISession session = sessionRepository.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    IList<Release> releases = session.CreateCriteria<Release>().List<Release>();

                    releaseModels.AddRange(releases.Select(
                        release => new ReleaseModel(release.TeamName, release.ReleaseNumber, release.Date)));
                }
            }
            return releaseModels;
        }

        public IEnumerable<ReleaseModel> GetReleases(DateTime dateTo)
        {
            return GetReleases().Where(x => x.Date > dateTo);
        }

        public ReleaseDetailsModel GetReleaseDetails(string releaseNumber)
        {
            ISessionFactory sessionRepository = CreateSessionFactory();
            using (ISession session = sessionRepository.OpenSession())
            {
                // retreive all stores and display them
                using (session.BeginTransaction())
                {
                    var releaseDetails =
                        session.CreateCriteria<ReleaseDetails>()
                            .Add(Restrictions.Eq("ReleaseNumber", releaseNumber))
                            .UniqueResult<ReleaseDetails>();

                    return new ReleaseDetailsModel
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

        public void SaveReleaseDetails(ReleaseDetailsModel releaseDetailsModel)
        {
            var release = new Release
                              {
                                  Date = DateTime.Today,
                                  ReleaseNumber = releaseDetailsModel.ReleaseNumber,
                                  TeamName = releaseDetailsModel.TeamName
                              };
            var releaseDetails = new ReleaseDetails
                                     {
                                         ReleaseNumber = releaseDetailsModel.ReleaseNumber,
                                         TeamName = releaseDetailsModel.TeamName,
                                         PrePatEmail = releaseDetailsModel.PrePatEmail,
                                         ServiceNowTicketLink = releaseDetailsModel.ServiceNowTicketLink,
                                         ReleaseFiInstructions = releaseDetailsModel.ReleaseFiInstructions
                                     };

            // create our NHibernate session factory            
            ISessionFactory sessionRepository = CreateSessionFactory();
            using (ISession session = sessionRepository.OpenSession())
            {
                // populate the database
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(release);
                    session.SaveOrUpdate(releaseDetails);
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