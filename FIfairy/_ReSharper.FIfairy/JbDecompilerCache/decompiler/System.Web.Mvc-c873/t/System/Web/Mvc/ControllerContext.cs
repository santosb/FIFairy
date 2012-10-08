// Type: System.Web.Mvc.ControllerContext
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System;
using System.Web;
using System.Web.Routing;

namespace System.Web.Mvc
{
  public class ControllerContext
  {
    internal const string PARENT_ACTION_VIEWCONTEXT = "ParentActionViewContext";
    private HttpContextBase _httpContext;
    private RequestContext _requestContext;
    private RouteData _routeData;

    public virtual ControllerBase Controller { get; set; }

    public virtual HttpContextBase HttpContext
    {
      get
      {
        if (this._httpContext == null)
          this._httpContext = this._requestContext != null ? this._requestContext.HttpContext : (HttpContextBase) new ControllerContext.EmptyHttpContext();
        return this._httpContext;
      }
      set
      {
        this._httpContext = value;
      }
    }

    public virtual bool IsChildAction
    {
      get
      {
        RouteData routeData = this.RouteData;
        if (routeData == null)
          return false;
        else
          return routeData.DataTokens.ContainsKey("ParentActionViewContext");
      }
    }

    public ViewContext ParentActionViewContext
    {
      get
      {
        return this.RouteData.DataTokens["ParentActionViewContext"] as ViewContext;
      }
    }

    public RequestContext RequestContext
    {
      get
      {
        if (this._requestContext == null)
          this._requestContext = new RequestContext(this.HttpContext ?? (HttpContextBase) new ControllerContext.EmptyHttpContext(), this.RouteData ?? new RouteData());
        return this._requestContext;
      }
      set
      {
        this._requestContext = value;
      }
    }

    public virtual RouteData RouteData
    {
      get
      {
        if (this._routeData == null)
          this._routeData = this._requestContext != null ? this._requestContext.RouteData : new RouteData();
        return this._routeData;
      }
      set
      {
        this._routeData = value;
      }
    }

    public ControllerContext()
    {
    }

    protected ControllerContext(ControllerContext controllerContext)
    {
      if (controllerContext == null)
        throw new ArgumentNullException("controllerContext");
      this.Controller = controllerContext.Controller;
      this.RequestContext = controllerContext.RequestContext;
    }

    public ControllerContext(HttpContextBase httpContext, RouteData routeData, ControllerBase controller)
      : this(new RequestContext(httpContext, routeData), controller)
    {
    }

    public ControllerContext(RequestContext requestContext, ControllerBase controller)
    {
      if (requestContext == null)
        throw new ArgumentNullException("requestContext");
      if (controller == null)
        throw new ArgumentNullException("controller");
      this.RequestContext = requestContext;
      this.Controller = controller;
    }

    private sealed class EmptyHttpContext : HttpContextBase
    {
    }
  }
}
