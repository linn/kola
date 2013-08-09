﻿using Kola.Model;

namespace Kola
{
    public interface IPageHandler
    {
        Page GetPage(string path);
    }

    public class PageHandler : IPageHandler
    {
        public Page GetPage(string path)
        {
            return new Page();
        }
    }
}
