using System;
using System.Collections.Generic;
using LilLycanLord_Official;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LilLycanLord_Official
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class SceneTransition : MonoBehaviour
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
        public UnityEvent beforeTriggers;

        public UnityEvent afterTriggers;

        [SerializeField]
        protected TMP_Text transitionDebugText;

        [SerializeField]
        protected TMP_Text loadingIndicator;

        [SerializeField]
        protected TMP_Text loadingText;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝
        [HideInInspector]
        public string transitionName;

        [HideInInspector]
        public Image background;

        [HideInInspector]
        public Animator animator;

        [HideInInspector]
        public Slider slider;

        [HideInInspector]
        public CanvasGroup canvasGroup;

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
        protected void InitializeSceneTransition()
        {
            transitionName = name;
            background = GetComponent<Image>();
            animator = GetComponent<Animator>();
            slider = GetComponent<Slider>();
            slider.fillRect = background.rectTransform;
            canvasGroup = GetComponent<CanvasGroup>();

            if (transitionDebugText == null && SceneTransitionManager.Instance.debugMode)
            {
                GameObject transitionDebugTextObject = new GameObject("Transition Debug Text");
                transitionDebugText = transitionDebugTextObject.AddComponent<TMP_Text>();
                transitionDebugText.text = "";
            }
        }

        protected virtual void UpdateDebugText()
        {
            transitionDebugText.gameObject.SetActive(SceneTransitionManager.Instance.debugMode);
            transitionDebugText.text =
                "Loading to \"" + SceneTransitionManager.Instance.targetSceneName + "\"...";
        }

        public abstract void PrimeTransition();
        public abstract void SwitchScene();
        public abstract void CleanUpTransition();
    }

    [Serializable]
    public class SceneTriggers
    {
        public Scene scene;
        public UnityEvent onEnter;
        public UnityEvent onExit;
    }

    public class SceneTransitionManager : MonoBehaviour
    {
        //! ╔═══════════════════╗
        //! ║ SINGLETON CONTENT ║
        //! ╚═══════════════════╝
        private static SceneTransitionManager instance;
        public static SceneTransitionManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            //* - - - - - Non - Singleton Awake Content - - - - -
        }

        //! - - - - - - - - - - -


        //* ╔════════════╗
        //* ║ Components ║
        //* ╚════════════╝

        //* ╔══════════╗
        //* ║ Displays ║
        //* ╚══════════╝
        [Header("Displays")]
        public bool isTransitioning = false;
        public List<SceneTransition> transitions = new List<SceneTransition>();

        [SerializeField]
        SceneTransition selectedTransition;

        //* ╔════════╗
        //* ║ Fields ║
        //* ╚════════╝
        [Space(10)]
        [Header("Fields")]
        [SerializeField]
        List<SceneTriggers> sceneTriggers = new List<SceneTriggers>();

        public bool debugMode = false;
        public string targetSceneName = "";
        public string selectedTransitionName = "";

        [Space(10)]
        [Header("Timing")]
        public float delay = 0.0f;
        public float enteringDuration = 0.0f;
        public float exitingDuration = 0.0f;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝
        string previousSceneName;
        float GUIWidth = 220;
        bool debugModeDrawn = false;

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        void Start()
        {
            foreach (Transform transition in transform.Find("Transitions").transform)
            {
                if (transition.GetComponent<SceneTransition>() != null)
                    transitions.Add(transition.GetComponent<SceneTransition>());
            }
        }

        void Update()
        {
            if (debugMode)
            {
                ShowDebugButtons();
                debugModeDrawn = true;
            }
            else
            {
                CleanUpDebugButtons();
                debugModeDrawn = false;
            }
        }

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        void ShowDebugButtons()
        {
            if (debugModeDrawn)
                return;

            DebuggingManager.Instance.SetWidth(GUIWidth);

            UnityEvent loadSceneWithTransition = new UnityEvent();
            loadSceneWithTransition.AddListener(LoadSceneWithTransition);
            DebuggingManager.Instance.AddDebugOption(
                "LoadSceneWithTransition",
                "Load Scene With Transition",
                "Loads the Target Scene with the selected Transition",
                loadSceneWithTransition
            );

            UnityEvent loadSceneWithoutTransition = new UnityEvent();
            loadSceneWithoutTransition.AddListener(LoadSceneWithoutTransition);
            DebuggingManager.Instance.AddDebugOption(
                "LoadSceneWithoutTransition",
                "Load Scene Without Transition",
                "Loads the Target Scene",
                loadSceneWithoutTransition
            );

            UnityEvent addToCurrentScene = new UnityEvent();
            addToCurrentScene.AddListener(LoadSceneWithoutTransition);
            DebuggingManager.Instance.AddDebugOption(
                "AddToCurrentScene",
                "Add To Current Scene",
                "Adds the Target Scene onto the Current Scene",
                addToCurrentScene
            );

            UnityEvent returnToPreviousScene = new UnityEvent();
            returnToPreviousScene.AddListener(ReturnToPreviousScene);
            DebuggingManager.Instance.AddDebugOption(
                "ReturnToPreviousScene",
                "Return To Previous Scene",
                "Return tp the previously Loaded Scene",
                returnToPreviousScene
            );
        }

        void CleanUpDebugButtons()
        {
            if (!debugModeDrawn)
                return;
            DebuggingManager.Instance.RemoveDebugOption("LoadSceneWithTransition");
            DebuggingManager.Instance.RemoveDebugOption("LoadSceneWithoutTransition");
            DebuggingManager.Instance.RemoveDebugOption("AddToCurrentScene");
            DebuggingManager.Instance.RemoveDebugOption("ReturnToPreviousScene");
        }

        public void LoadSceneWithTransition()
        {
            if (targetSceneName == "")
            {
                Debug.LogError(name + " has no Target Scene");
                return;
            }
            if (selectedTransitionName == "")
            {
                Debug.LogWarning(
                    name + " has no Selected Transition; switching with no transition"
                );
                LoadSceneWithoutTransition();
                return;
            }
            if (!isTransitioning)
            {
                previousSceneName = SceneManager.GetActiveScene().name;
                selectedTransition = FindTransition(selectedTransitionName);
                Debug.Log("Transitioning to \"" + targetSceneName + "\"...");
                Invoke("TriggerTransition", delay);
            }
            else
                Debug.LogWarning(name + " is already transitioning to " + targetSceneName);
        }

        void TriggerTransition()
        {
            selectedTransition.PrimeTransition();
            ActivateTriggers(false);
            FlagTransition(true);
        }

        public void LoadSceneWithTransition(string transitionName)
        {
            selectedTransitionName = transitionName;
            LoadSceneWithTransition();
        }

        public void LoadSceneWithoutTransition()
        {
            if (isTransitioning)
                return;
            previousSceneName = SceneManager.GetActiveScene().name;
            ActivateTriggers(false);
            LoadToTargetScene();
            ActivateTriggers(true);
        }

        public void LoadToTargetScene()
        {
            SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
            if (selectedTransitionName != "" && selectedTransition != null)
                EndTransition();
        }

        public void AddToCurrentScene()
        {
            SceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive);
        }

        public void AddToCurrentScene(string transitionName)
        {
            targetSceneName = transitionName;
            AddToCurrentScene();
        }

        public void ReturnToPreviousScene()
        {
            if (isTransitioning)
                return;
            targetSceneName = previousSceneName;
            LoadSceneWithTransition();
        }

        public void EndTransition()
        {
            ActivateTriggers(true);
            FlagTransition(false);

            targetSceneName = "";
        }

        SceneTransition FindTransition(string transtionName)
        {
            if (transitions.Find(transition => transition.transitionName == transtionName) == null)
            {
                Debug.LogError("A Scene Transition called \"" + transtionName + "\" was not found");
                return null;
            }
            return transitions.Find(transition => transition.transitionName == transtionName);
        }

        void ActivateTriggers(bool onEnter)
        {
            List<SceneTriggers> triggers = new List<SceneTriggers>();
            if (onEnter)
            {
                if (selectedTransitionName != "" && selectedTransition != null)
                    if (selectedTransition.afterTriggers != null)
                        selectedTransition.afterTriggers.Invoke();
                if (
                    sceneTriggers.Contains(
                        sceneTriggers.Find(
                            sceneTrigger => sceneTrigger.scene.name == targetSceneName
                        )
                    )
                )
                    triggers = sceneTriggers.FindAll(
                        sceneTrigger => sceneTrigger.scene.name == targetSceneName
                    );
            }
            else
            {
                if (selectedTransitionName != "" && selectedTransition != null)
                    if (selectedTransition.beforeTriggers != null)
                        selectedTransition.beforeTriggers.Invoke();
                if (
                    sceneTriggers.Contains(
                        sceneTriggers.Find(
                            sceneTrigger => sceneTrigger.scene.name == targetSceneName
                        )
                    )
                )
                    triggers = sceneTriggers.FindAll(
                        sceneTrigger => sceneTrigger.scene.name == previousSceneName
                    );
            }

            if (triggers.Count > 0)
                foreach (SceneTriggers sceneTrigger in triggers)
                    if (onEnter)
                        sceneTrigger.onEnter.Invoke();
                    else
                        sceneTrigger.onExit.Invoke();
        }

        void FlagTransition(bool flag)
        {
            isTransitioning = flag;
            if (selectedTransitionName == "")
                return;

            selectedTransition.animator.SetBool("isTransitioning", isTransitioning);
        }
    }
}
