using System;

namespace StableCube.Bulzor
{
    public class SassCompileException : Exception
    {
        public SassCompileException(string message, Exception inner): base(message, inner)
        {
        }
    }
}