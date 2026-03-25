using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulVideoPlayer : BulMediaPlayerBase
{
    protected override void BuildMediaTag(RenderTreeBuilder builder, int index)
    {
        PlayerState.MediaType = BulMediaPlayMediaType.Video;

        builder.OpenRegion(index);
        builder.OpenElement(0, "video");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "src", Src);

        if(Poster != null)
            builder.AddAttribute(3, "poster", Poster);

        builder.AddAttribute(4, "loop", PlayerState.Loop);
        builder.AddAttribute(5, "autoplay", PlayerState.Autoplay);
        builder.AddAttribute(6, "muted", PlayerState.Muted);

        if(Preload.HasValue)
            builder.AddAttribute(7, "preload", Preload.Value.ToString().ToLower());

        builder.AddContent(8, ChildContent);
        builder.CloseElement();
        builder.CloseRegion();
    }
}
