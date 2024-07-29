using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;

namespace LilLycanLord_Official
{
    public class Endings : MonoBehaviour
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
        // [Space(10)]
        // [Header("Fields")]
        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        public void Retry()
        {
            SceneTransitionManager.Instance.targetSceneName = "Tutorial";
            SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
            SceneTransitionManager.Instance.LoadSceneWithTransition();
        }

        public void ReturnToMainMenu()
        {
            SceneTransitionManager.Instance.targetSceneName = "Main Menu";
            SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
            SceneTransitionManager.Instance.LoadSceneWithTransition();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
