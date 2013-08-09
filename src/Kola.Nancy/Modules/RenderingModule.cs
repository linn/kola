﻿using Nancy;
using Nancy.Responses.Negotiation;

namespace Kola.Nancy.Modules
{
    public class RenderingModule : NancyModule
    {
        private readonly IPageHandler pageHandler;

        public RenderingModule(IPageHandler pageHandler)
        {
            this.pageHandler = pageHandler;
            this.Get["(?<templatePath>.*)"] = this.GetPage;
            this.Get["/"] = this.GetPage;
        }

        private Negotiator GetPage(dynamic parameters)
        {
            var page = this.pageHandler.GetPage((string)parameters.templatePath);

            return this.Negotiate
                .WithMediaRangeModel("text/html", page).WithView("Page");
        }
    }
}