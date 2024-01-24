using System.Collections.Generic;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Demo.Client;

public class InputSelectPageFormData
{
    public string GenericSelectValue { get; set; } = "darth-vadar";

    public KeyValuePair<string, string> DicSelectValue { get; set; } = new KeyValuePair<string, string>("Chewbacca", "chewbacca");
    public KeyValuePair<int, string> DicSelectValue2 { get; set; } = new KeyValuePair<int, string>(1960, "1960's");
    public BulSize EnumSelectValue { get; set; } = BulSize.Medium;
    public string DemoDataValue { get; set; } = "Luke";
}
