// Type: FIfairyData.ReleaseRepository
// Assembly: FIfairyData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\FI fairy\FIfairy\FIfairy\FIfairy\bin\FIfairyData.dll

using FIfairyDomain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FIfairyData
{
  public class ReleaseRepository : IReleaseRepository
  {
    private const string DbFile = "FIFairy.db";

    public IEnumerable<Release> GetReleases()
    {
      using (ISession session = ReleaseRepository.CreateSessionFactory().OpenSession())
      {
        using (session.BeginTransaction())
          return (IEnumerable<Release>) session.CreateCriteria<Release>().List<Release>();
      }
    }

    public IEnumerable<Release> GetReleases(DateTime dateTo)
    {
      return Enumerable.Where<Release>(this.GetReleases(), (Func<Release, bool>) (x => x.ReleaseDate > dateTo));
    }

    public Release GetReleaseDetails(string releaseNumber)
    {
      using (ISession session = ReleaseRepository.CreateSessionFactory().OpenSession())
      {
        using (session.BeginTransaction())
          return session.Get<Release>((object) releaseNumber);
      }
    }

    public void SaveReleaseDetails(Release release)
    {
      using (ISession session = ReleaseRepository.CreateSessionFactory().OpenSession())
      {
        using (ITransaction transaction = session.BeginTransaction())
        {
          session.SaveOrUpdate((object) release);
          transaction.Commit();
        }
      }
    }

    private static ISessionFactory CreateSessionFactory()
    {
      return Fluently.Configure().Database((IPersistenceConfigurer) SQLiteConfiguration.Standard.UsingFile("FIFairy.db")).Mappings((Action<MappingConfiguration>) (m => m.FluentMappings.AddFromAssemblyOf<ReleaseRepository>())).ExposeConfiguration(new Action<Configuration>(ReleaseRepository.BuildSchema)).BuildSessionFactory();
    }

    private static void BuildSchema(Configuration config)
    {
      if (File.Exists("FIFairy.db"))
        return;
      new SchemaExport(config).Create(false, true);
    }

    public void SavePrePatEmailFile(string filename, Stream inputStream)
    {
      string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + filename;
      MemoryStream memoryStream = new MemoryStream();
      inputStream.CopyTo((Stream) memoryStream);
      File.WriteAllBytes(path, memoryStream.ToArray());
    }

    public FileStream GetPrePatEmailFile(string filename)
    {
      return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "\\" + filename);
    }
  }
}
