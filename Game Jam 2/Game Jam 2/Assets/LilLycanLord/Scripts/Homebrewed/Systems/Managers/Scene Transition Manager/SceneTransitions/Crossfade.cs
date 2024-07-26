using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LilLycanLord_Official
{
    public class Crossfade : SceneTransition
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
        Color fadeInColor = Color.black;

        [SerializeField]
        Color fadeOutColor = Color.black;

        [Space(10)]
        [Header("Timing")]
        [SerializeField]
        float fadeInDuration = 1.0f;

        [SerializeField]
        float transitionDuration = 0.0f;

        [SerializeField]
        float fadeOutDuration = 1.0f;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        void Awake()
        {
            InitializeSceneTransition();
        }

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        public override void PrimeTransition()
        {
            UpdateDebugText();
            if (SceneTransitionManager.Instance.exitingDuration > 0)
                fadeInDuration = SceneTransitionManager.Instance.exitingDuration;

            if (fadeInDuration == 0.0f)
                fadeInDuration = 1.0f;
            animator.SetFloat("fadeInSpeed", 1.0f / fadeInDuration);

            if (background != null)
                background.color = fadeInColor;
        }

        public override void SwitchScene()
        {
            SceneTransitionManager.Instance.LoadToTargetScene();
            Invoke("CleanUpTransition", transitionDuration);
        }

        public override void CleanUpTransition()
        {
            if (SceneTransitionManager.Instance.enteringDuration > 0)
                fadeOutDuration = SceneTransitionManager.Instance.enteringDuration;

            if (fadeOutDuration == 0.0f)
                fadeOutDuration = 1.0f;
            animator.SetFloat("fadeOutSpeed", 1.0f / fadeOutDuration);

            if (background != null)
                background.color = fadeOutColor;
            SceneTransitionManager.Instance.EndTransition();
        }
    }
}
