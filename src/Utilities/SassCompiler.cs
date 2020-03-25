using System;
using System.IO;
using System.Text;
using SharpScss;

namespace StableCube.Bulzor
{
    public static class SassCompiler
    {
        public static string Compile(SassOptions ops)
        {
            EmbeddedFileHelper.CopyTo(ops.TempDir);

            string bulzorRootPath = Path.Combine(ops.TempDir, "src/root.scss");
            if(!File.Exists(bulzorRootPath))
                throw new FileNotFoundException($"File not found: {bulzorRootPath}");

            string bulmaRootPath = Path.Combine(ops.TempDir, $"vendor/bulma/bulma.sass");
            if(!File.Exists(bulmaRootPath))
                throw new FileNotFoundException($"File not found: {bulmaRootPath}");

            string bulmaFunctionsPath = Path.Combine(ops.TempDir, $"vendor/bulma/sass/utilities/functions.sass");
            if(!File.Exists(bulmaFunctionsPath))
                throw new FileNotFoundException($"File not found: {bulmaFunctionsPath}");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"@import \"{bulmaFunctionsPath}\";");
            sb.AppendLine($"@import \"{ops.ThemeTemplatePath}\";");
            sb.AppendLine($"@import \"{bulmaRootPath}\";");
            sb.AppendLine($"@import \"{bulzorRootPath}\";");

            foreach (var import in ops.Imports)
            {
                if(!File.Exists(import))
                    throw new FileNotFoundException($"File not found: {import}");

                sb.AppendLine($"@import \"{import}\";");
            }

            string css = String.Empty;
            try
            {
                var result = Scss.ConvertToCss(sb.ToString());
                css = result.Css;
            }
            catch (System.Exception e)
            {
                throw new SassCompileException("Sass failed to compile. There is probably an error in a sass document.", e);
            }

            return css;
        }
    }
}