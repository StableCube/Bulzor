using Microsoft.AspNetCore.Components.Web;

namespace StableCube.Bulzor.Components;

public class BulPageClickEventArgs : BulMouseEventArgsBase
{
    public int Page { get; set; }

    public BulPageClickEventArgs()
    {
    }

    public BulPageClickEventArgs(MouseEventArgs mouseArgs, int page)
    {
        Page = page;
        MouseEventArgs = mouseArgs;
    }
}
