using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LilLycanLord_Official
{
    public class Tutorial : MonoBehaviour
    {
        //* ╔════════════╗
        //* ║ Components ║
        //* ╚════════════╝

        //* ╔══════════╗
        //* ║ Displays ║
        //* ╚══════════╝
        [Header("Displays")]
        public int currentPage = 1;

        //* ╔════════╗
        //* ║ Fields ║
        //* ╚════════╝
        [Space(10)]
        [Header("Fields")]
        public Image Tutorial1;
        public Image Tutorial2;
        public Image Tutorial3;
        public Image Tutorial4;
        public Image Tutorial5;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        void Awake() { }

        void Start()
        {
            Reset();
        }

        void Update() { }

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        void Reset()
        {
            Tutorial1.gameObject.SetActive(true);
            Tutorial2.gameObject.SetActive(false);
            Tutorial3.gameObject.SetActive(false);
            Tutorial4.gameObject.SetActive(false);
            Tutorial5.gameObject.SetActive(false);
        }

        public void ProgressTutorial()
        {
            if (currentPage >= 6)
                return;
            SimpleAudioManager.Instance.Play("Tutorial");
            currentPage++;
            switch (currentPage)
            {
                case 2:
                    Tutorial2.gameObject.SetActive(true);
                    break;
                case 3:
                    Tutorial3.gameObject.SetActive(true);
                    break;
                case 4:
                    Tutorial4.gameObject.SetActive(true);
                    break;
                case 5:
                    Tutorial5.gameObject.SetActive(true);
                    break;
                case 6:
                    SceneTransitionManager.Instance.targetSceneName = "Game Proper";
                    SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
                    SceneTransitionManager.Instance.LoadSceneWithTransition();
                    break;
            }
        }
    }
}
