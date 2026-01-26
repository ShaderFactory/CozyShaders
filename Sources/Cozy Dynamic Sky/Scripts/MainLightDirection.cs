using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MainLightDirection : MonoBehaviour
{
    /// <summary> Used in the logic that detected when you rotated the light so that it calls the Tick() method. </summary>
    private Vector3 lastForward;

    [Header("Settings")]
    [Range(0, 1)] public float fogAmount;
    [Range(0, 1)] public float fogLuminosity;
    [Range(0, 1)] public float fogWashOut;
    public float sunIntensity = 1f;

    [Header("Private Values")]
    [SerializeField] private float DayNightRatio;
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient skyGradient;
    [SerializeField] private Gradient equatorGradient;
    [SerializeField] private Gradient groundGradient;
    [SerializeField] private Gradient sunColorGradient;

    [Header("References")]
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Light lightComponent;
    [SerializeField] private SimpleSunRotate sunRotate;
    [SerializeField] private Material poleLightMaterial;

    [Header("Curves")]
    public AnimationCurve sunsetDisappearanceCurve;

    [Header("Lights")]
    public List<Light> poleLights;

    private void OnEnable()
    {
        ValidateConditions();
        Tick();
    }

    private void OnValidate()
    {
        Tick();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (transform.forward != lastForward)
            {
                lastForward = transform.forward;
                Debug.LogWarning("Just rotated");
                Tick();
            }
        }

        Tick();
    }

    /// <summary>
    /// Performs important checks to make sure the script can only be added to a directional light.
    /// </summary>
    private void ValidateConditions()
    {
        Debug.LogWarning("Validating Conditions!");
    }

    private void Tick()
    {
        // Debug.Log("Tick()");

        // Set up Ambient Light in Render Settings.
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = skyGradient.Evaluate(DayNightRatio); 
        RenderSettings.ambientEquatorColor = equatorGradient.Evaluate(DayNightRatio);
        RenderSettings.ambientGroundColor = groundGradient.Evaluate(DayNightRatio);
        lightComponent.color = sunColorGradient.Evaluate(DayNightRatio);

        // Gradients created from Gradient Factory
        fogGradient = GradientFactory.CreateFogGradient();
        sunColorGradient = GradientFactory.CreateSunColorGradient();

        if (skyMaterial != null)
        {
            skyMaterial.SetVector("_MainLightDirection", transform.forward);
        }

        // Shader Graph logic:
        float dot = Vector3.Dot(transform.forward, Vector3.up);
        DayNightRatio = Mathf.Clamp01(Mathf.InverseLerp(-1f, 1f, dot));

        // Evaluate intensity curve
        lightComponent.intensity = sunsetDisappearanceCurve.Evaluate(DayNightRatio) * sunIntensity;

        SetFogColor();
        LightLogic();

        DynamicGI.UpdateEnvironment();
    }

    private void SetFogColor()
    {
        Color rawHorizonColor = fogGradient.Evaluate(DayNightRatio);
        Color horizonColorDarkened = rawHorizonColor * fogLuminosity;
        Color grey = new Color(fogWashOut, fogWashOut, fogWashOut, 1f);

        RenderSettings.fogColor = horizonColorDarkened + grey;
        RenderSettings.fogDensity = fogAmount * 0.2f;
    }   

    private void LightLogic()
    {
        if (DayNightRatio > 0.6f)
        { 
            foreach(Light light in poleLights)
            {
                light.enabled = true;
                if (poleLightMaterial != null)
                { 
                    poleLightMaterial.EnableKeyword("_EMISSION");
                }
            }
        }
        else
        {
            foreach (Light light in poleLights)
            {
                light.enabled = false;
                if (poleLightMaterial != null)
                {
                    poleLightMaterial.DisableKeyword("_EMISSION");
                }
            }
        }
    }

}