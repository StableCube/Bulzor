using Microsoft.AspNetCore.Components.Rendering;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public class BulAudioPlayer : BulMediaPlayerBase
    {
        protected override void BuildMediaTag(RenderTreeBuilder builder, int index)
        {
            PlayerState.MediaType = BulMediaPlayMediaType.Audio;

            builder.OpenRegion(index);
            builder.OpenElement(0, "audio");
            builder.AddAttribute(1, "class", MediaRootClassBuilder.ClassString);
            builder.AddAttribute(2, "src", Src);
            builder.AddAttribute(3, "loop", PlayerState.Loop);
            builder.AddAttribute(4, "autoplay", PlayerState.Autoplay);
            builder.AddAttribute(5, "muted", PlayerState.Muted);

            if(Preload.HasValue)
                builder.AddAttribute(6, "preload", Preload.Value.ToString().ToLower());

            builder.AddContent(7, ChildContent);
            builder.CloseElement();
            builder.CloseRegion();
        }
    }
}