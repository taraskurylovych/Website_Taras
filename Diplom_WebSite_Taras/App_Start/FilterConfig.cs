﻿using System.Web;
using System.Web.Mvc;

namespace Diplom_WebSite_Taras
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
