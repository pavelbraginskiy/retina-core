﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Retina.Extensions
{
    public static class EnumerableExtension
    {
        public static string Riffle(this IEnumerable<string> values, IEnumerable<string> separators)
        {
            var result = new StringBuilder();

            using (var value = values.GetEnumerator())
            using (var separator = separators.GetEnumerator())
            {
                if (!value.MoveNext())
                    return "";

                result.Append(value.Current);

                while (value.MoveNext())
                {
                    if (!separator.MoveNext())
                        throw new ArgumentException("There are not enough separators!");

                    result.Append(separator.Current)
                          .Append(value.Current);
                }

                if (separator.MoveNext())
                    throw new ArgumentException("There are too many separators!");

            }

            return result.ToString();
        }

        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }
    }
}
