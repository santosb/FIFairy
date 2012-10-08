// Type: System.Web.Mvc.ViewDataDictionary
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc.Resources;

namespace System.Web.Mvc
{
  public class ViewDataDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private readonly Dictionary<string, object> _innerDictionary;
    private object _model;
    private ModelMetadata _modelMetadata;
    private readonly ModelStateDictionary _modelState;
    private TemplateInfo _templateMetadata;

    public int Count
    {
      get
      {
        return this._innerDictionary.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this._innerDictionary.IsReadOnly;
      }
    }

    public ICollection<string> Keys
    {
      get
      {
        return (ICollection<string>) this._innerDictionary.Keys;
      }
    }

    public object Model
    {
      get
      {
        return this._model;
      }
      set
      {
        this._modelMetadata = (ModelMetadata) null;
        this.SetModel(value);
      }
    }

    public virtual ModelMetadata ModelMetadata
    {
      get
      {
        if (this._modelMetadata == null && this._model != null)
          this._modelMetadata = ModelMetadataProviders.Current.GetMetadataForType((Func<object>) (() => this._model), this._model.GetType());
        return this._modelMetadata;
      }
      set
      {
        this._modelMetadata = value;
      }
    }

    public ModelStateDictionary ModelState
    {
      get
      {
        return this._modelState;
      }
    }

    public object this[string key]
    {
      get
      {
        object obj;
        this._innerDictionary.TryGetValue(key, out obj);
        return obj;
      }
      set
      {
        this._innerDictionary[key] = value;
      }
    }

    public TemplateInfo TemplateInfo
    {
      get
      {
        if (this._templateMetadata == null)
          this._templateMetadata = new TemplateInfo();
        return this._templateMetadata;
      }
      set
      {
        this._templateMetadata = value;
      }
    }

    public ICollection<object> Values
    {
      get
      {
        return (ICollection<object>) this._innerDictionary.Values;
      }
    }

    public ViewDataDictionary()
      : this((object) null)
    {
    }

    public ViewDataDictionary(object model)
    {
      this.Model = model;
    }

    public ViewDataDictionary(ViewDataDictionary dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException("dictionary");
      foreach (KeyValuePair<string, object> keyValuePair in dictionary)
        this._innerDictionary.Add(keyValuePair.Key, keyValuePair.Value);
      foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary.ModelState)
        this.ModelState.Add(keyValuePair.Key, keyValuePair.Value);
      this.Model = dictionary.Model;
      this.TemplateInfo = dictionary.TemplateInfo;
      this._modelMetadata = dictionary._modelMetadata;
    }

    public void Add(KeyValuePair<string, object> item)
    {
      this._innerDictionary.Add(item);
    }

    public void Add(string key, object value)
    {
      this._innerDictionary.Add(key, value);
    }

    public void Clear()
    {
      this._innerDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      return this._innerDictionary.Contains(item);
    }

    public bool ContainsKey(string key)
    {
      return this._innerDictionary.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      this._innerDictionary.CopyTo(array, arrayIndex);
    }

    public object Eval(string expression)
    {
      ViewDataInfo viewDataInfo = this.GetViewDataInfo(expression);
      if (viewDataInfo == null)
        return (object) null;
      else
        return viewDataInfo.Value;
    }

    public string Eval(string expression, string format)
    {
      object obj = this.Eval(expression);
      if (obj == null)
        return string.Empty;
      if (string.IsNullOrEmpty(format))
        return Convert.ToString(obj, (IFormatProvider) CultureInfo.CurrentCulture);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, new object[1]
      {
        obj
      });
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this._innerDictionary.GetEnumerator();
    }

    public ViewDataInfo GetViewDataInfo(string expression)
    {
      if (string.IsNullOrEmpty(expression))
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "expression");
      else
        return ViewDataDictionary.ViewDataEvaluator.Eval(this, expression);
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      return this._innerDictionary.Remove(item);
    }

    public bool Remove(string key)
    {
      return this._innerDictionary.Remove(key);
    }

    protected virtual void SetModel(object value)
    {
      this._model = value;
    }

    public bool TryGetValue(string key, out object value)
    {
      return this._innerDictionary.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this._innerDictionary.GetEnumerator();
    }

    internal static class ViewDataEvaluator
    {
      public static ViewDataInfo Eval(ViewDataDictionary vdd, string expression)
      {
        return ViewDataDictionary.ViewDataEvaluator.EvalComplexExpression((object) vdd, expression);
      }

      private static ViewDataInfo EvalComplexExpression(object indexableObject, string expression)
      {
        foreach (ViewDataDictionary.ViewDataEvaluator.ExpressionPair expressionPair in ViewDataDictionary.ViewDataEvaluator.GetRightToLeftExpressions(expression))
        {
          string propertyName = expressionPair.Left;
          string expression1 = expressionPair.Right;
          ViewDataInfo propertyValue = ViewDataDictionary.ViewDataEvaluator.GetPropertyValue(indexableObject, propertyName);
          if (propertyValue != null)
          {
            if (string.IsNullOrEmpty(expression1))
              return propertyValue;
            if (propertyValue.Value != null)
            {
              ViewDataInfo viewDataInfo = ViewDataDictionary.ViewDataEvaluator.EvalComplexExpression(propertyValue.Value, expression1);
              if (viewDataInfo != null)
                return viewDataInfo;
            }
          }
        }
        return (ViewDataInfo) null;
      }

      private static IEnumerable<ViewDataDictionary.ViewDataEvaluator.ExpressionPair> GetRightToLeftExpressions(string expression)
      {
        yield return new ViewDataDictionary.ViewDataEvaluator.ExpressionPair(expression, string.Empty);
        int lastDot = expression.LastIndexOf('.');
        string subExpression = expression;
        string postExpression = string.Empty;
        for (; lastDot > -1; lastDot = subExpression.LastIndexOf('.'))
        {
          subExpression = expression.Substring(0, lastDot);
          postExpression = expression.Substring(lastDot + 1);
          yield return new ViewDataDictionary.ViewDataEvaluator.ExpressionPair(subExpression, postExpression);
        }
      }

      private static ViewDataInfo GetIndexedPropertyValue(object indexableObject, string key)
      {
        IDictionary<string, object> dictionary = indexableObject as IDictionary<string, object>;
        object obj = (object) null;
        bool flag = false;
        if (dictionary != null)
        {
          flag = dictionary.TryGetValue(key, out obj);
        }
        else
        {
          TryGetValueDelegate getValueDelegate = TypeHelpers.CreateTryGetValueDelegate(indexableObject.GetType());
          if (getValueDelegate != null)
            flag = getValueDelegate(indexableObject, key, out obj);
        }
        if (!flag)
          return (ViewDataInfo) null;
        return new ViewDataInfo()
        {
          Container = indexableObject,
          Value = obj
        };
      }

      private static ViewDataInfo GetPropertyValue(object container, string propertyName)
      {
        ViewDataInfo indexedPropertyValue = ViewDataDictionary.ViewDataEvaluator.GetIndexedPropertyValue(container, propertyName);
        if (indexedPropertyValue != null)
          return indexedPropertyValue;
        ViewDataDictionary viewDataDictionary = container as ViewDataDictionary;
        if (viewDataDictionary != null)
          container = viewDataDictionary.Model;
        if (container == null)
          return (ViewDataInfo) null;
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(container).Find(propertyName, true);
        if (descriptor == null)
          return (ViewDataInfo) null;
        return new ViewDataInfo((Func<object>) (() => descriptor.GetValue(container)))
        {
          Container = container,
          PropertyDescriptor = descriptor
        };
      }

      private struct ExpressionPair
      {
        public readonly string Left;
        public readonly string Right;

        public ExpressionPair(string left, string right)
        {
          this.Left = left;
          this.Right = right;
        }
      }
    }
  }
}
