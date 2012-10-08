// Type: System.Web.HttpPostedFileBase
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Web.dll

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
  [TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
  public abstract class HttpPostedFileBase
  {
    public virtual int ContentLength
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public virtual string ContentType
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public virtual string FileName
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public virtual Stream InputStream
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public virtual void SaveAs(string filename)
    {
      throw new NotImplementedException();
    }
  }
}
