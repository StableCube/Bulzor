
namespace StableCube.Bulzor.Components.Extended;

public class BulmaClassBuilderExtended : BulmaClassBuilder
{
    public bool? IsThin { get; set; }
    public bool? IsRightToLeft { get; set; }

    public BulmaClassBuilderExtended(){}

    public BulmaClassBuilderExtended(string baseClass) : base(baseClass)
    {
    }

    protected override void BuildClassString()
    {
        base.BuildClassString();

        AppendIfTrue(IsThin, "is-thin");
        AppendIfTrue(IsRightToLeft, "is-rtl");
    }
}
