using System;

namespace StableCube.Bulzor.Components;

public static class BCss
{
    /// <summary>
    /// Adds the Bulzor css prefix to all elements in a string
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    public static string Prefix(string className)
    {
        string[] classParts = className.Split(' ');
        string result = String.Empty;
        for (int i = 0; i < classParts.Length; i++)
        {
            classParts[i] = CssConfig.Prefix + classParts[i];
        }

        return String.Join(" ", classParts);
    }
}
