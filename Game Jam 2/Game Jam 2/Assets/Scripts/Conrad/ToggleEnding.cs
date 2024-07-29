using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;

namespace LilLycanLord_Official
{
    public class ToggleEnding : MonoBehaviour
    {
        //* ╔════════════╗
        //* ║ Components ║
        //* ╚════════════╝

        //* ╔══════════╗
        //* ║ Displays ║
        //* ╚══════════╝
        // [Header("Displays")]

        //* ╔════════╗
        //* ║ Fields ║
        //* ╚════════╝
        [Space(10)]
        [Header("Fields")]
        [SerializeField]
        bool badEnd = false;

        [SerializeField]
        float delay = 3;

        [SerializeField]
        Canvas canvas;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        void Awake() { }

        void Start()
        {
            canvas.gameObject.SetActive(false);
            if (badEnd)
                Invoke("toggle", delay);
        }

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        public void toggle()
        {
            canvas.gameObject.SetActive(true);
            if (badEnd)
                SimpleAudioManager.Instance.Play("Bad Ending");
            else
                SimpleAudioManager.Instance.Play("Good Ending");
        }
    }
}
