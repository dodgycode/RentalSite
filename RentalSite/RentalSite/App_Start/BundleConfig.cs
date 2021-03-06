﻿using System.Web;
using System.Web.Optimization;

namespace RentalSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));
            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/css/basic.css",
                     "~/Scripts/dropzone/css/dropzone.css"));

            bundles.Add(new StyleBundle("~/content/toastr").Include("~/Content/toastr.min.css")
                .Include("~/Content/toastr.css"));
            bundles.Add(new ScriptBundle("~/bundles/toastr")
                .Include("~/Scripts/toastr.js"));

            bundles.Add(new StyleBundle("~/content/PropertyEdit")
                .Include("~/Content/PropertyEdit.less"));

            bundles.Add(new StyleBundle("~/content/simple-slideshow-styles")
                .Include("~/content/simple-slideshow-styles.css"));
            bundles.Add(new ScriptBundle("~/scripts/better-simple-slideshow")
                .Include("~/Scripts/better-simple-slideshow.js")
                .Include("~/Scripts/hammer.js"));
        }
    }
}
