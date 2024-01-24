using System;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components;

public abstract class BulInputSingleLineBase<TValue> : BulInputBase<TValue>
{
    [Parameter]
    public bool? Rounded { get; set; }

    [Parameter]
    public bool HasIconsLeft { get; set; }

    [Parameter]
    public bool HasIconsRight { get; set; }

    [Parameter]
    public bool? Static { get; set; }

    [Parameter]
    public RenderFragment BulIcons { get; set; }

    [Parameter]
    public bool? Expanded { get; set; }

    [Parameter]
    public bool? FullWidth { get; set; }

    protected BulmaClassBuilder WrapperClassBuilder { get; set; } = new BulmaClassBuilder("control");
    protected BulmaClassBuilder InputClassBuilder { get; set; } = new BulmaClassBuilder("input");

    protected override void BuildBulma()
    {
        WrapperClassBuilder.Size = Size;
        WrapperClassBuilder.IsLoading = Loading;
        WrapperClassBuilder.IsStatic = Static;
        WrapperClassBuilder.HasIconsLeft = HasIconsLeft;
        WrapperClassBuilder.HasIconsRight = HasIconsRight;
        WrapperClassBuilder.IsExpanded = Expanded;

        InputClassBuilder.SchemeColor = Color;
        InputClassBuilder.Size = Size;
        InputClassBuilder.IsRounded = Rounded;
        InputClassBuilder.IsFullWidth = FullWidth;

        MergeBuilderClassAttribute(InputClassBuilder);
    }
}