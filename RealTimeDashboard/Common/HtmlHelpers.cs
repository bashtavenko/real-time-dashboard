using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealTimeDashboard.Common
{
    public static class HtmlHelpers
    {
        public static bool RouteMatch(this HtmlHelper helper, string action, string controller)
        {
            return RouteMatch(helper, action, controller, null, null, null);
        }

        public static bool RouteMatch(this HtmlHelper helper, string navigateToAction, string navigateToController,
                    string actionToMatch, string controllerToMatch, RouteValueDictionary routeParam)
        {         
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"] as string;
            var currentAction = routeData["action"] as string;

            bool actionMatch = CompareTwoStringsOrStringAndRegex(navigateToAction, currentAction, actionToMatch);
            bool controllerMatch = CompareTwoStringsOrStringAndRegex(navigateToController, currentController, controllerToMatch);

            bool routeParamMatch;
            if (routeParam == null)
            {
                routeParamMatch = true;
            }
            else
            {
                var oneParam = routeParam.First();
                routeParamMatch = String.Equals(routeData[oneParam.Key] as string,
                                                oneParam.Value as string, StringComparison.OrdinalIgnoreCase);
            }

            return actionMatch && routeParamMatch && controllerMatch;
        }

        // firstString == secondString or secondString regex match (ignore firstString)
        private static bool CompareTwoStringsOrStringAndRegex(string firstString, string secondString, string secondStringRegex)
        {
            if (string.IsNullOrEmpty(secondStringRegex))
                return string.Equals(firstString, secondString, StringComparison.InvariantCultureIgnoreCase);
            else
            {
                var regex = new Regex(secondStringRegex, RegexOptions.IgnoreCase);
                return regex.Match(secondString).Success;
            }
        }
    }
}