using System;
using System.Collections.Generic;

namespace StableCube.Bulzor.Components
{
    public static class RatioHelper
    {
        /// <summary>
        /// Maps ratios to their fractional values
        /// </summary>
        public static Dictionary<BulRatio, double> FractionMap = new Dictionary<BulRatio, double>
        {
            { BulRatio.R16by9, (double)16/9 },
            { BulRatio.RSquare, (double)1/1 },
            { BulRatio.R1by1, (double)1/1 },
            { BulRatio.R1by2, (double)1/2 },
            { BulRatio.R1by3, (double)1/3 },
            { BulRatio.R2by1, (double)2/1 },
            { BulRatio.R2by3, (double)2/3 },
            { BulRatio.R3by1, (double)3/1 },
            { BulRatio.R3by2, (double)3/2 },
            { BulRatio.R3by4, (double)3/4 },
            { BulRatio.R3by5, (double)3/5 },
            { BulRatio.R4by3, (double)4/3 },
            { BulRatio.R4by5, (double)4/5 },
            { BulRatio.R5by3, (double)5/3 },
            { BulRatio.R5by4, (double)5/4 },
            { BulRatio.R9by16, (double)9/16 }
        };

        /// <summary>
        /// Find the closest supported ratio for a given width and height
        /// </summary>
        public static BulRatio FindClosest(int width, int height)
        {
            int gcd = GreatestCommonDivisor(width, height);
            double ratioW = (double)width / gcd;
            double ratioH = (double)height / gcd;
            double ratio = (double)ratioW / ratioH;
            double closestDiff = 99;
            BulRatio closestRatio = BulRatio.R16by9;

            foreach (var availRatio in FractionMap)
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
    }
}
