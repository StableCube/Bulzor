using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public abstract class BulInputSelectBase<TValue> : InputBase<TValue>
{
    /// <summary>
    /// Add an icon with the supplied class. For instance "fa fa-globe fa-2x"
    /// </summary>
    [Parameter]
    public string IconClass { get; set; }

    [Parameter]
    public BulSchemeColor? Color { get; set; }

    [Parameter]
    public BulSize? Size { get; set; }

    [Parameter]
    public bool? Rounded { get; set; }

    [Parameter]
    public bool? Loading { get; set; }

    [Parameter]
    public bool? Expanded { get; set; }

    [Parameter]
    public bool? FullWidth { get; set; }

    protected BulmaClassBuilder ControlClassBuilder { get; set; } = new BulmaClassBuilder("control");
    protected BulmaClassBuilder SelectClassBuilder { get; set; } = new BulmaClassBuilder("select");
    protected BulmaClassBuilder IconClassBuilder { get; set; } = new BulmaClassBuilder("icon");

    protected abstract void BuildBulma();
}
