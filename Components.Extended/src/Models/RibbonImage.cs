using System;

namespace StableCube.Bulzor.Components.Extended
{
    public struct RibbonImage
    {
        public int Index { get; private set; }

        public Uri Uri { get; private set; }

        public RibbonImage(int index, Uri uri)
        {
            Index = index;
            Uri = uri;
        }
    }
}
