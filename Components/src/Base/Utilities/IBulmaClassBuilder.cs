
namespace StableCube.Bulzor.Components;

public interface IBulmaClassBuilder
{
    string ClassString { get; }

    string ToString();

    void SetRaw(string value);
}
