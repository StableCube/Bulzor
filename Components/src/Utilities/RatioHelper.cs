using System;
using System.Collections.Generic;

namespace StableCube.Bulzor.Components
{
    public static class RatioHelper
    {
        private static Dictionary<BulRatio, double> RatioMap = new Dictionary<BulRatio, double>
        {
            { BulRatio.R16by9, TruncateDecimal((double)16/9, 4) },
            { BulRatio.R1by1, TruncateDecimal((double)1/1, 4) },
            { BulRatio.R1by2, TruncateDecimal((double)1/2, 4) },
            { BulRatio.R1by3, TruncateDecimal((double)1/3, 4) },
            { BulRatio.R2by1, TruncateDecimal((double)2/1, 4) },
            { BulRatio.R2by3, TruncateDecimal((double)2/3, 4) },
            { BulRatio.R3by1, TruncateDecimal((double)3/1, 4) },
            { BulRatio.R3by2, TruncateDecimal((double)3/2, 4) },
            { BulRatio.R3by4, TruncateDecimal((double)3/4, 4) },
            { BulRatio.R3by5, TruncateDecimal((double)3/5, 4) },
            { BulRatio.R4by3, TruncateDecimal((double)4/3, 4) },
            { BulRatio.R4by5, TruncateDecimal((double)4/5, 4) },
            { BulRatio.R5by3, TruncateDecimal((double)5/3, 4) },
            { BulRatio.R5by4, TruncateDecimal((double)5/4, 4) },
            { BulRatio.R9by16, TruncateDecimal((double)9/16, 4) }
        };

        /// <summary>
        /// Find the closest supported ratio for a given width and height
        /// </summary>
        public static BulRatio FindClosestRatio(int width, int height)
        {
            int gcd = GreatestCommonDivisor(width, height);
            double ratioW = (double)width/gcd;
            double ratioH = (double)height/gcd;
            double ratio = TruncateDecimal(ratioW / ratioH, 4);
            double closestDiff = 99;
            BulRatio closestRatio = BulRatio.R16by9;

            foreach (var availRatio in RatioMap)
            {
                var diff = Math.Abs(availRatio.Value - ratio);
                if(diff < closestDiff)
                {
                    closestDiff = diff;
                    closestRatio = availRatio.Key;
                }
            }

            return closestRatio;
        }

        private static int GreatestCommonDivisor(int a, int b)
        {
            return (b == 0) ? a : GreatestCommonDivisor(b, a%b);
        }

        private static double TruncateDecimal(double value, int precision)
        {
            double step = (double)Math.Pow(10, precision);
            double tmp = Math.Truncate(step * value);

            return tmp / step;
        }
    }
}
