using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoxe.Extensions.Mongo.UnitTest
{
    internal static class Comparer
    {
        internal static bool Compare<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first is null || second is null)
                return false;
            return first.Count() == second.Count() && first.All(second.Contains);
        }

        internal static bool Compare(IList<DateTime> first, IList<DateTime> second)
        {
            if (first is null || second is null)
                return false;
            for (var i = 0; i < first.Count; i++)
            {
                var f = first[i].ToUniversalTime();
                var s = second[i].ToUniversalTime();
                if (f.Date != s.Date)
                    return false;
                if (f.Hour != s.Hour)
                    return false;
                if (f.Minute != s.Minute)
                    return false;
                if (f.Second != s.Second)
                    return false;
            }

            return true;
        }

        internal static bool Compare(DateTime first, DateTime second)
        {
            first = first.ToUniversalTime();
            second = second.ToUniversalTime();
            if (first.Date != second.Date)
                return false;
            if (first.Hour != second.Hour)
                return false;
            if (first.Minute != second.Minute)
                return false;
            if (first.Second != second.Second)
                return false;
            return true;
        }
    }
}
