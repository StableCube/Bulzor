using System;

namespace StableCube.Bulzor
{
    public static class BulmaClassHelper
    {
        public static string ColorClass(BulmaColor color)
        {
            switch (color)
            {
                case BulmaColor.Danger:
                    return "is-danger";
                case BulmaColor.Dark:
                    return "is-dark";
                case BulmaColor.Info:
                    return "is-info";
                case BulmaColor.Link:
                    return "is-link";
                case BulmaColor.Primary:
                    return "is-primary";
                case BulmaColor.Success:
                    return "is-success";
                case BulmaColor.Text:
                    return "is-text";
                case BulmaColor.Warning:
                    return "is-warning";
                default:
                    return "is-primary";
            }
        }
    }
}