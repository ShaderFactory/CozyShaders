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
                new GradientColorKey(Hex("#FFA679"), 0.379f),
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

    private static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out var color);
        return color;
    }
}
