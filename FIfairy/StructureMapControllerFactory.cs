using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace FIfairy
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        private IContainer _container;

        public StructureMapControllerFactory(IContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (controllerType == null)
                       ? base.GetControllerInstance(requestContext, null)
                       : (IController) _container.GetInstance(controllerType);
        }
    }
}