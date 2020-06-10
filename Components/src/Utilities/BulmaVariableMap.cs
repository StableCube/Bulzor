using System;

namespace StableCube.Bulzor.Components
{
    public static class BulmaVariableMap
    {
        public static string PrimaryColor(BulPrimaryColor color)
        {
            switch (color)
            {
                case BulPrimaryColor.Danger:
                    return "is-danger";
                case BulPrimaryColor.Dark:
                    return "is-dark";
                case BulPrimaryColor.Light:
                    return "is-light";
                case BulPrimaryColor.Info:
                    return "is-info";
                case BulPrimaryColor.Link:
                    return "is-link";
                case BulPrimaryColor.Primary:
                    return "is-primary";
                case BulPrimaryColor.Success:
                    return "is-success";
                case BulPrimaryColor.Text:
                    return "is-text";
                case BulPrimaryColor.Warning:
                    return "is-warning";
                default:
                    return "is-primary";
            }
        }

        private static string SizeMap(BulSize value)
        {
            switch (value)
            {
                case BulSize.Small:
                    return "small";
                case BulSize.Normal:
                    return "normal";
                case BulSize.Medium:
                    return "medium";
                case BulSize.Large:
                    return "large";
                default:
                    return "normal";
            }
        }

        public static string Size(BulSize value)
        {
            return "is-" + SizeMap(value);
        }

        public static string ChildSize(BulSize value)
        {
            return "are-" + SizeMap(value);
        }

        private static string ColorMap(BulColor value)
        {
            switch (value)
            {
                case BulColor.White:
                    return "white";
                case BulColor.Black:
                    return "black";
                case BulColor.Light:
                    return "light";
                case BulColor.Dark:
                    return "dark";
                case BulColor.Primary:
                    return "primary";
                case BulColor.Info:
                    return "info";
                case BulColor.Link:
                    return "link";
                case BulColor.Success:
                    return "success";
                case BulColor.Warning:
                    return "warning";
                case BulColor.Danger:
                    return "danger";
                case BulColor.BlackBis:
                    return "black-bis";
                case BulColor.BlackTer:
                    return "black-ter";
                case BulColor.GreyDarker:
                    return "grey-darker";
                case BulColor.GreyDark:
                    return "grey-dark";
                case BulColor.Grey:
                    return "grey";
                case BulColor.GreyLight:
                    return "grey-light";
                case BulColor.GreyLighter:
                    return "grey-lighter";
                case BulColor.WhiteTer:
                    return "white-ter";
                case BulColor.WhiteBis:
                    return "white-bis";
                default:
                    return "black";
            }
        }

        public static string TextColor(BulColor value)
        {
            return "has-text-" + ColorMap(value);
        }

        public static string BackgroundColor(BulColor value)
        {
            return "has-background-" + ColorMap(value);
        }

        public static string TextSize(BulTextSize value)
        {
            switch (value)
            {
                case BulTextSize.S1:
                    return "is-size-1";
                case BulTextSize.S2:
                    return "is-size-2";
                case BulTextSize.S3:
                    return "is-size-3";
                case BulTextSize.S4:
                    return "is-size-4";
                case BulTextSize.S5:
                    return "is-size-5";
                case BulTextSize.S6:
                    return "is-size-6";
                case BulTextSize.S7:
                    return "is-size-7";
                default:
                    return "is-size-1";
            }
        }

        public static string TextWeight(BulTextWeight value)
        {
            switch (value)
            {
                case BulTextWeight.Light:
                    return "has-text-weight-light";
                case BulTextWeight.Normal:
                    return "has-text-weight-normal";
                case BulTextWeight.Medium:
                    return "has-text-weight-medium";
                case BulTextWeight.SemiBold:
                    return "has-text-weight-semibold";
                case BulTextWeight.Bold:
                    return "has-text-weight-bold";
                default:
                    return "has-text-weight-normal";
            }
        }

        public static string ColumnSize(BulColumnSize value)
        {
            switch (value)
            {
                case BulColumnSize.S1:
                    return "is-1";
                case BulColumnSize.S2:
                    return "is-2";
                case BulColumnSize.S3:
                    return "is-3";
                case BulColumnSize.S4:
                    return "is-4";
                case BulColumnSize.S5:
                    return "is-5";
                case BulColumnSize.S6:
                    return "is-6";
                case BulColumnSize.S7:
                    return "is-7";
                case BulColumnSize.S8:
                    return "is-8";
                case BulColumnSize.S9:
                    return "is-9";
                case BulColumnSize.S10:
                    return "is-10";
                case BulColumnSize.S11:
                    return "is-11";
                case BulColumnSize.S12:
                    return "is-12";
                default:
                    return "is-1";
            }
        }

        public static string Dimension(BulDimension value)
        {
            switch (value)
            {
                case BulDimension.D16x16:
                    return "is-16x16";
                case BulDimension.D24x24:
                    return "is-24x24";
                case BulDimension.D32x32:
                    return "is-32x32";
                case BulDimension.D48x48:
                    return "is-48x48";
                case BulDimension.D64x64:
                    return "is-64x64";
                case BulDimension.D96x96:
                    return "is-96x96";
                case BulDimension.D128x128:
                    return "is-128x128";
                default:
                    return "is-16x16";
            }
        }

        public static string Ratio(BulRatio value)
        {
            switch (value)
            {
                case BulRatio.RSquare:
                    return "is-square";
                case BulRatio.R1by1:
                    return "is-1by1";
                case BulRatio.R5by4:
                    return "is-5by4";
                case BulRatio.R4by3:
                    return "is-4by3";
                case BulRatio.R3by2:
                    return "is-3by2";
                case BulRatio.R5by3:
                    return "is-5by3";
                case BulRatio.R16by9:
                    return "is-16by9";
                case BulRatio.R2by1:
                    return "is-2by1";
                case BulRatio.R3by1:
                    return "is-3by1";
                case BulRatio.R4by5:
                    return "is-4by5";
                case BulRatio.R3by4:
                    return "is-3by4";
                case BulRatio.R2by3:
                    return "is-2by3";
                case BulRatio.R3by5:
                    return "is-3by5";
                case BulRatio.R9by16:
                    return "is-9by16";
                case BulRatio.R1by2:
                    return "is-1by2";
                case BulRatio.R1by3:
                    return "is-1by3";
                default:
                    return "is-square";
            }
        }
    }
}