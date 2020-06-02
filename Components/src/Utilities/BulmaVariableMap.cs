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

        public static string Size(BulSize value)
        {
            switch (value)
            {
                case BulSize.Small:
                    return "is-small";
                case BulSize.Normal:
                    return "is-normal";
                case BulSize.Medium:
                    return "is-medium";
                case BulSize.Large:
                    return "is-large";
                default:
                    return "is-normal";
            }
        }

        public static string ChildSize(BulSize value)
        {
            switch (value)
            {
                case BulSize.Small:
                    return "are-small";
                case BulSize.Normal:
                    return "are-normal";
                case BulSize.Medium:
                    return "are-medium";
                case BulSize.Large:
                    return "are-large";
                default:
                    return "are-normal";
            }
        }

        public static string TextColor(BulColor value)
        {
            switch (value)
            {
                case BulColor.White:
                    return "has-text-white";
                case BulColor.Black:
                    return "has-text-black";
                case BulColor.Light:
                    return "has-text-light";
                case BulColor.Dark:
                    return "has-text-dark";
                case BulColor.Primary:
                    return "has-text-primary";
                case BulColor.Info:
                    return "has-text-info";
                case BulColor.Link:
                    return "has-text-link";
                case BulColor.Success:
                    return "has-text-success";
                case BulColor.Warning:
                    return "has-text-warning";
                case BulColor.Danger:
                    return "has-text-danger";
                case BulColor.BlackBis:
                    return "has-text-black-bis";
                case BulColor.BlackTer:
                    return "has-text-black-ter";
                case BulColor.GreyDarker:
                    return "has-text-grey-darker";
                case BulColor.GreyDark:
                    return "has-text-grey-dark";
                case BulColor.Grey:
                    return "has-text-grey";
                case BulColor.GreyLight:
                    return "has-text-grey-light";
                case BulColor.GreyLighter:
                    return "has-text-grey-lighter";
                case BulColor.WhiteTer:
                    return "has-text-white-ter";
                case BulColor.WhiteBis:
                    return "has-text-white-bis";
                default:
                    return "has-text-black";
            }
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

        public static string BackgroundColor(BulColor value)
        {
            switch (value)
            {
                case BulColor.White:
                    return "has-background-white";
                case BulColor.Black:
                    return "has-background-black";
                case BulColor.Light:
                    return "has-background-light";
                case BulColor.Dark:
                    return "has-background-dark";
                case BulColor.Primary:
                    return "has-background-primary";
                case BulColor.Info:
                    return "has-background-info";
                case BulColor.Link:
                    return "has-background-link";
                case BulColor.Success:
                    return "has-background-success";
                case BulColor.Warning:
                    return "has-background-warning";
                case BulColor.Danger:
                    return "has-background-danger";
                case BulColor.BlackBis:
                    return "has-background-black-bis";
                case BulColor.BlackTer:
                    return "has-background-black-ter";
                case BulColor.GreyDarker:
                    return "has-background-grey-darker";
                case BulColor.GreyDark:
                    return "has-background-grey-dark";
                case BulColor.Grey:
                    return "has-background-grey";
                case BulColor.GreyLight:
                    return "has-background-grey-light";
                case BulColor.GreyLighter:
                    return "has-background-grey-lighter";
                case BulColor.WhiteTer:
                    return "has-background-white-ter";
                case BulColor.WhiteBis:
                    return "has-background-white-bis";
                default:
                    return "has-background-white";
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
    }
}