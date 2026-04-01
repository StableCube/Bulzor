using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulVideoPlayer : BulMediaPlayerBase
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void BuildMediaTag(RenderTreeBuilder builder, int index)
    {
        PlayerState.MediaType = BulMediaPlayMediaType.Video;

        builder.OpenRegion(index);
        builder.OpenElement(0, "video");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "src", Src);
        builder.AddAttribute(3, "data-player-id", InstanceId);

        if(Poster != null)
            builder.AddAttribute(4, "poster", Poster);

        builder.AddAttribute(5, "loop", PlayerState.Loop);
        builder.AddAttribute(6, "autoplay", PlayerState.Autoplay);
        builder.AddAttribute(7, "muted", PlayerState.Muted);

        if(Preload.HasValue)
            builder.AddAttribute(8, "preload", Preload.Value.ToString().ToLower());

        builder.AddContent(9, ChildContent);
        builder.CloseElement();
        builder.CloseRegion();
    }
}
