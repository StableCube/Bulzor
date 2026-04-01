using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components.MediaPlayer;

public class BulAudioPlayer : BulMediaPlayerBase
{
    protected override void BuildMediaTag(RenderTreeBuilder builder, int index)
    {
        PlayerState.MediaType = BulMediaPlayMediaType.Audio;

        builder.OpenRegion(index);
        builder.OpenElement(0, "audio");
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "src", Src);
        builder.AddAttribute(3, "data-player-id", InstanceId);
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
