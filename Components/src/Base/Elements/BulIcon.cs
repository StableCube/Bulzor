using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public enum BulIconPosition
{
    Left,
    Right
}

public class BulIcon : BulComponentBase
{
    [Parameter]
    public BulColor? Color { get; set; }

    [Parameter]
    public BulSize? Size { get; set; }

    /// <summary>
    /// Sets the is-left or is-right bulma property
    /// </summary>
    [Parameter]
    public BulIconPosition? Position { get; set; }

    protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("icon");

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
        WrapperClassBuilder.Size = Size;
        WrapperClassBuilder.TextColor = Color;

        if(Position.HasValue)
        {
            if(Position.Value == BulIconPosition.Left)
            {
                WrapperClassBuilder.IsLeft = true;
            }
            else
            {
                WrapperClassBuilder.IsRight = true;
            }
        }
        
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", WrapperClassBuilder.ClassString);

        builder.OpenElement(2, "i");
        builder.AddMultipleAttributes(3, AdditionalAttributes);
        builder.CloseElement();

        builder.CloseElement();
    }
}
