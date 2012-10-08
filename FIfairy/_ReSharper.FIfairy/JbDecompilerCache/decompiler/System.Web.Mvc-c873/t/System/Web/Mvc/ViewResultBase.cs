// Type: System.Web.Mvc.ViewResultBase
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System;
using System.IO;

namespace System.Web.Mvc
{
  public abstract class ViewResultBase : ActionResult
  {
    private TempDataDictionary _tempData;
    private ViewDataDictionary _viewData;
    private ViewEngineCollection _viewEngineCollection;
    private string _viewName;

    public TempDataDictionary TempData
    {
      get
      {
        if (this._tempData == null)
          this._tempData = new TempDataDictionary();
        return this._tempData;
      }
      set
      {
        this._tempData = value;
      }
    }

    public IView View { get; set; }

    public ViewDataDictionary ViewData
    {
      get
      {
        if (this._viewData == null)
          this._viewData = new ViewDataDictionary();
        return this._viewData;
      }
      set
      {
        this._viewData = value;
      }
    }

    public ViewEngineCollection ViewEngineCollection
    {
      get
      {
        return this._viewEngineCollection ?? ViewEngines.Engines;
      }
      set
      {
        this._viewEngineCollection = value;
      }
    }

    public string ViewName
    {
      get
      {
        return this._viewName ?? string.Empty;
      }
      set
      {
        this._viewName = value;
      }
    }

    public override void ExecuteResult(ControllerContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");
      if (string.IsNullOrEmpty(this.ViewName))
        this.ViewName = context.RouteData.GetRequiredString("action");
      ViewEngineResult viewEngineResult = (ViewEngineResult) null;
      if (this.View == null)
      {
        viewEngineResult = this.FindView(context);
        this.View = viewEngineResult.View;
      }
      TextWriter output = context.HttpContext.Response.Output;
      this.View.Render(new ViewContext(context, this.View, this.ViewData, this.TempData, output), output);
      if (viewEngineResult == null)
        return;
      viewEngineResult.ViewEngine.ReleaseView(context, this.View);
    }

    protected abstract ViewEngineResult FindView(ControllerContext context);
  }
}
