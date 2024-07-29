using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LilLycanLord_Official
{
    public class Timer : MonoBehaviour
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
        public Slider colorShifter;
        public float amountInSeconds = 300.0f;
        float currentTimer = 0.0f;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        void Awake()
        {
            colorShifter = GetComponent<Slider>();
        }

        void Start()
        {
            currentTimer = amountInSeconds;
        }

        void Update()
        {
            colorShifter.value = currentTimer / amountInSeconds;
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                SceneTransitionManager.Instance.targetSceneName = "Bad Ending";
                SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
                SceneTransitionManager.Instance.LoadSceneWithTransition();
            }
        }

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
    }
}
