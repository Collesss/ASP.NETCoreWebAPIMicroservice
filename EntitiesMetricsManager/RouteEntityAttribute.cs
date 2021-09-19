using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EntitiesMetricsManager
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed class RouteEntityAttribute : Attribute
    {
        public string Pattern { get; }
        public string LeftStopRegExp { get; }
        public string RightStopRegExp { get; }

        public RouteEntityAttribute(string pattern, string leftStopRegExp = @"\{", string rightStopRegExp = @"\}")
        {
            Pattern = pattern;
            LeftStopRegExp = leftStopRegExp;
            RightStopRegExp = rightStopRegExp;
        }

        public string GetRoute(string className)
        {
            Regex regExp = new Regex(@"\\{(?<RegExp>[\W\w]*)\\}");

            Regex regExpInPattern = new Regex(regExp.Match(Pattern).Groups["RegExp"].Value);

            return String.Format(regExp.Replace(Pattern, "{0}"), regExpInPattern.Match(className).Value);
        }
    }
}
