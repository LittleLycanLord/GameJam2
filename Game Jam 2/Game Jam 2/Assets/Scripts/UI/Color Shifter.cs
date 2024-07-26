using UnityEngine;
using UnityEngine.UI;

public class ColorShifter : MonoBehaviour
{
    //* ╔════════════╗
    //* ║ Components ║
    //* ╚════════════╝

    //* ╔════════╗
    //* ║ Fields ║
    //* ╚════════╝
    [SerializeField]
    UnityEngine.UI.Slider slider;

    [SerializeField]
    UnityEngine.UI.Image targetImage;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float threshold = 0.5f;

    [SerializeField]
    Color lowerColor = Color.black;

    [SerializeField]
    Color middleColor = Color.grey;

    [SerializeField]
    Color upperColor = Color.white;

    //* ╔════════════╗
    //* ║ Attributes ║
    //* ╚════════════╝


    //* ╔══════════╗
    //* ║ Displays ║
    //* ╚══════════╝
    [Header("Displays")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float value = 1.0f;

    //* ╔═══════════════╗
    //* ║ Monobehaviour ║
    //* ╚═══════════════╝
    void Update()
    {
        if (slider != null)
            value = slider.value;
        if (value <= threshold)
            targetImage.color = Color.Lerp(lowerColor, middleColor, value / threshold);
        else
            targetImage.color = Color.Lerp(middleColor, upperColor, (value / 1.0f) / 2);
    }

    //* ╔═════════════════════╗
    //* ║ Non - Monobehaviour ║
    //* ╚═════════════════════╝
}
