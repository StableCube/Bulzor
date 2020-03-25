using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace StableCube.Bulzor
{
    public static class EmbeddedFileHelper
    {
        public static string EmbedToDirPath(string path)
        {
            string newPath = path.Replace('.', Path.DirectorySeparatorChar);
            int extIdx = newPath.LastIndexOf(Path.DirectorySeparatorChar);
            string fullPath = newPath.Substring(0, extIdx) + "." + newPath.Substring(extIdx + 1);

            string parentDirPath = Path.GetDirectoryName(fullPath);
            parentDirPath = parentDirPath.Replace('_', '-');

            fullPath = Path.Combine(parentDirPath, Path.GetFileName(fullPath));

            return fullPath;
        }

        public static void CopyTo(string outputDir)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var provider = new EmbeddedFileProvider(assembly, "StableCube.Bulzor");

            foreach (var item in provider.GetDirectoryContents(String.Empty))
            {
                string relativeFilePath = EmbedToDirPath(item.Name);
                string fullPath = Path.Combine(outputDir, relativeFilePath);
                string parentDirPath = Path.GetDirectoryName(fullPath);

                if(!Directory.Exists(parentDirPath))
                    Directory.CreateDirectory(parentDirPath);

                var resourceStream = assembly.GetManifestResourceStream($"StableCube.Bulzor.{item.Name}");
                using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                {
                    File.WriteAllText(fullPath, reader.ReadToEnd());
                }
            }
        }
    }
}