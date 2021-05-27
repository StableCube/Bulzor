using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components.Extended
{
    public class BulImageRibbonItemClickEventArgs : BulMouseEventArgsBase
    {
        public RibbonImage Image { get; set; }

        public BulImageRibbonItemClickEventArgs()
        {
        }

        public BulImageRibbonItemClickEventArgs(MouseEventArgs mouseArgs, RibbonImage image)
        {
            MouseEventArgs = mouseArgs;
            Image = image;
        }
    }
}