
namespace StableCube.Bulzor.Components;

public static class BulmaVariableMap
{
    public static string SchemeColor(BulSchemeColor color)
    {
        return color switch
        {
            BulSchemeColor.Danger => "is-danger",
            BulSchemeColor.Dark => "is-dark",
            BulSchemeColor.Light => "is-light",
            BulSchemeColor.Info => "is-info",
            BulSchemeColor.Link => "is-link",
            BulSchemeColor.Primary => "is-primary",
            BulSchemeColor.Success => "is-success",
            BulSchemeColor.Text => "is-text",
            BulSchemeColor.Warning => "is-warning",
            _ => "is-primary",
        };
    }

    private static string SizeMap(BulSize value)
    {
        return value switch
        {
            BulSize.Small => "small",
            BulSize.Normal => "normal",
            BulSize.Medium => "medium",
            BulSize.Large => "large",
            _ => "normal",
        };
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
        return value switch
        {
            BulColor.White => "white",
            BulColor.Black => "black",
            BulColor.Light => "light",
            BulColor.Dark => "dark",
            BulColor.Primary => "primary",
            BulColor.Info => "info",
            BulColor.Link => "link",
            BulColor.Success => "success",
            BulColor.Warning => "warning",
            BulColor.Danger => "danger",
            BulColor.BlackBis => "black-bis",
            BulColor.BlackTer => "black-ter",
            BulColor.GreyDarker => "grey-darker",
            BulColor.GreyDark => "grey-dark",
            BulColor.Grey => "grey",
            BulColor.GreyLight => "grey-light",
            BulColor.GreyLighter => "grey-lighter",
            BulColor.WhiteTer => "white-ter",
            BulColor.WhiteBis => "white-bis",
            _ => "black",
        };
    }

    public static string Color(BulColor value)
    {
        return "is-" + ColorMap(value);
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
        return value switch
        {
            BulTextSize.S1 => "is-size-1",
            BulTextSize.S2 => "is-size-2",
            BulTextSize.S3 => "is-size-3",
            BulTextSize.S4 => "is-size-4",
            BulTextSize.S5 => "is-size-5",
            BulTextSize.S6 => "is-size-6",
            BulTextSize.S7 => "is-size-7",
            _ => "is-size-1",
        };
    }

    public static string TextWeight(BulTextWeight value)
    {
        return value switch
        {
            BulTextWeight.Light => "has-text-weight-light",
            BulTextWeight.Normal => "has-text-weight-normal",
            BulTextWeight.Medium => "has-text-weight-medium",
            BulTextWeight.SemiBold => "has-text-weight-semibold",
            BulTextWeight.Bold => "has-text-weight-bold",
            _ => "has-text-weight-normal",
        };
    }

    public static string ColumnSize(BulColumnSize value)
    {
        return value switch
        {
            BulColumnSize.S1 => "is-1",
            BulColumnSize.S2 => "is-2",
            BulColumnSize.S3 => "is-3",
            BulColumnSize.S4 => "is-4",
            BulColumnSize.S5 => "is-5",
            BulColumnSize.S6 => "is-6",
            BulColumnSize.S7 => "is-7",
            BulColumnSize.S8 => "is-8",
            BulColumnSize.S9 => "is-9",
            BulColumnSize.S10 => "is-10",
            BulColumnSize.S11 => "is-11",
            BulColumnSize.S12 => "is-12",
            _ => "is-1",
        };
    }

    public static string Dimension(BulDimension value)
    {
        return value switch
        {
            BulDimension.D16x16 => "is-16x16",
            BulDimension.D24x24 => "is-24x24",
            BulDimension.D32x32 => "is-32x32",
            BulDimension.D48x48 => "is-48x48",
            BulDimension.D64x64 => "is-64x64",
            BulDimension.D96x96 => "is-96x96",
            BulDimension.D128x128 => "is-128x128",
            _ => "is-16x16",
        };
    }

    public static string Ratio(BulRatio value)
    {
        return value switch
        {
            BulRatio.RSquare => "is-square",
            BulRatio.R1by1 => "is-1by1",
            BulRatio.R5by4 => "is-5by4",
            BulRatio.R4by3 => "is-4by3",
            BulRatio.R3by2 => "is-3by2",
            BulRatio.R5by3 => "is-5by3",
            BulRatio.R16by9 => "is-16by9",
            BulRatio.R2by1 => "is-2by1",
            BulRatio.R3by1 => "is-3by1",
            BulRatio.R4by5 => "is-4by5",
            BulRatio.R3by4 => "is-3by4",
            BulRatio.R2by3 => "is-2by3",
            BulRatio.R3by5 => "is-3by5",
            BulRatio.R9by16 => "is-9by16",
            BulRatio.R1by2 => "is-1by2",
            BulRatio.R1by3 => "is-1by3",
            _ => "is-square",
        };
    }
}
