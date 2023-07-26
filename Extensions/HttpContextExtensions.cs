﻿namespace WMKancelariapp.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetController(this HttpContext context)
        {
            var controller = GetPath(context).ElementAt(1);

            return controller;
        }
        public static string GetAction(this HttpContext context)
        {
            var action = GetPath(context).ElementAt(0);

            return action;
        }

        private static string[] GetPath(HttpContext context)
        {
            var host = context.Request.Host.ToUriComponent();
            var referer = context.Request.Headers["Referer"].ToString();
            var trimmedUrl = referer.Substring(referer.IndexOf(host, StringComparison.InvariantCultureIgnoreCase) + host.Length + 1);
            var path = trimmedUrl.Split('/');

            return path;
        }
    }
}
