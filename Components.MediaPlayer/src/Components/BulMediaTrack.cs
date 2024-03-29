﻿using System;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulMediaTrack : BulComponentBase
{
    [Parameter]
    public Uri SrcUri { get; set; }

    [Parameter]
    public string Kind { get; set; }

    [Parameter]
    public string Label { get; set; }

    [Parameter]
    public string SrcLang { get; set; }

    [Parameter]
    public bool IsDefault { get; set; }

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "track");
        builder.AddAttribute(1, "src", SrcUri);
        builder.AddAttribute(2, "kind", Kind);
        builder.AddAttribute(3, "label", Label);
        builder.AddAttribute(4, "srclang", SrcLang);
        builder.AddAttribute(5, "default", IsDefault);
        builder.CloseElement();
    }
}
