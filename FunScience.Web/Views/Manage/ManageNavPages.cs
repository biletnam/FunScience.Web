﻿namespace FunScience.Web.Views.Manage
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using System;

    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string ChangePassword => "ChangePassword";

        public static string Details = "Details";

        public static string Schadule = "Schedule";

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string DetailsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Details);

        public static string ScheduleNavClass(ViewContext viewContext) => PageNavClass(viewContext, Schadule);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
