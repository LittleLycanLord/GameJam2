using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;

namespace LilLycanLord_Official
{
    public class MainMenu : MonoBehaviour
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

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        public void Play()
        {
            SceneTransitionManager.Instance.targetSceneName = "Tutorial";
            SimpleAudioManager.Instance.Play("Game Start");
            SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
            SceneTransitionManager.Instance.LoadSceneWithTransition();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
