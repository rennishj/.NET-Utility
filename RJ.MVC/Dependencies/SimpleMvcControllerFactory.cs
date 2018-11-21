using RJ.DependencyInjection;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace RJ.MVC.Dependencies {
    public class SimpleMvcControllerFactory : DefaultControllerFactory {
        private readonly SimpleDependencyResolver _resolver;

        public SimpleMvcControllerFactory(SimpleDependencyResolver resolver) {
            _resolver = resolver;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if (controllerType == null) {
                return null;
            }

            var controller = _resolver.CreateInstance<IController>(controllerType, false)
                ?? base.GetControllerInstance(requestContext, controllerType);
            return controller;
        }
    }
}