using UnityEngine;

public static class GradientFactory
{
    public static Gradient CreateFogGradient()
    {
        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Hex("#FFFFFF"), 0.000f),
                new GradientColorKey(Hex("#F7D6D6"), 0.2f),
                new GradientColorKey(Hex("#FFAE1F"), 0.379f),
                new GradientColorKey(Hex("#FFA58A"), 0.506f),
                new GradientColorKey(Hex("#2E1173"), 0.788f),
                new GradientColorKey(Hex("#051178"), 1.000f),
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f),
            }
        );

        return gradient;
    }

    public static Gradient CreateSunColorGradient()
    {
        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Hex("#FFFFFF"), 0f),
                new GradientColorKey(Hex("#FFFFFF"), 0.15f),
                new GradientColorKey(Hex("#FFE9CB"), 0.2f),
                new GradientColorKey(Hex("#FFAE00"), 0.3f)
            },
            new GradientAlphaKey[]  // This is just to make sure it's opaque.
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f),
            }
        );

        return gradient;
    }

    // Helpers
    private static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out var color);
        return color;
    }
}
