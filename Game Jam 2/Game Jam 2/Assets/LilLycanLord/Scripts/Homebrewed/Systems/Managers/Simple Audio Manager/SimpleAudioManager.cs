using System.Collections;
using System.Collections.Generic;
using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace LilLycanLord_Official
{
    public class SimpleAudioManager : MonoBehaviour
    {
        //! ╔═══════════════════╗
        //! ║ SINGLETON CONTENT ║
        //! ╚═══════════════════╝
        private static SimpleAudioManager instance;
        public static SimpleAudioManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            if (instance == null || instance != this)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            //* - - - - - Non - Singleton Awake Content - - - - -
            // if (GetComponent<BeatAndAudioSync>() != null)
            //     Debug.Log("Beat and Audio Synced");

            if (gameObject.GetComponent<AudioSource>() == null)
                gameObject.AddComponent<AudioSource>();

            foreach (Sound sound in sounds)
            {
                sound.audioSource = gameObject.GetComponent<AudioSource>();
            }
        }

        //! - - - - - - - - - - -

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
        private List<Sound> sounds = new List<Sound>();

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝


        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
        public void Play(string name)
        {
            Sound sound = sounds.Find(sound => sound.name == name);
            Assert.IsNotNull(sound, "Sound with name [" + name + "] not found.");
            // if (GetComponent<BeatAndAudioSync>() != null)
            //     BeatAndAudioSync.Instance.StartBeatSync(sound);
            sound.audioSource.loop = sound.loop;
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.Play();
        }

        public void Play(Sound sound)
        {
            Play(sound.name);
        }

        public void Play(string name, float fadeIn)
        {
            Sound sound = sounds.Find(sound => sound.name == name);
            Assert.IsNotNull(sound, "Sound with name [" + name + "] not found.");
            sound.volume = 0.0f;
            // if (GetComponent<BeatAndAudioSync>() != null)
            //     BeatAndAudioSync.Instance.StartBeatSync(sound);
            sound.audioSource.loop = sound.loop;
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.Play();
            StartCoroutine(FadeIn(sound, fadeIn));
        }

        public void Play(Sound sound, float fadeIn)
        {
            Play(sound.name, fadeIn);
        }

        public void Play(List<string> names)
        {
            foreach (string name in names)
                Play(name);
        }

        public void Play(List<Sound> sounds)
        {
            foreach (Sound sound in sounds)
                Play(sound);
        }

        public void Play(List<string> names, float fadeIn)
        {
            foreach (string name in names)
                Play(name, fadeIn);
        }

        public void Play(List<Sound> sounds, float fadeIn)
        {
            foreach (Sound sound in sounds)
                Play(sound, fadeIn);
        }

        public void Stop(string name)
        {
            Sound sound = sounds.Find(sound => sound.name == name);
            Assert.IsNotNull(sound, "Sound with name [" + name + "] not found.");
            sound.audioSource.Stop();
            // if (GetComponent<BeatAndAudioSync>() != null)
            //     BeatAndAudioSync.Instance.StopBeatSync(sound);
        }

        public void Stop(Sound sound)
        {
            Stop(sound.name);
        }

        public void Stop(string name, float fadeOut)
        {
            Sound sound = sounds.Find(sound => sound.name == name);
            Assert.IsNotNull(sound, "Sound with name [" + name + "] not found.");
            sound.volume = 1.0f;
            StartCoroutine(FadeOut(sound, sound.audioClip.length + fadeOut));
        }

        public void Stop(Sound sound, float fadeOut)
        {
            Stop(sound.name, fadeOut);
        }

        public void Stop(List<string> names)
        {
            foreach (string name in names)
                Stop(name);
        }

        public void Stop(List<Sound> sounds)
        {
            Stop(sounds);
        }

        public void Stop(List<string> names, float fadeOut)
        {
            foreach (string name in names)
                Stop(name, fadeOut);
        }

        public void Stop(List<Sound> sounds, float fadeOut)
        {
            Stop(sounds, fadeOut);
        }

        [ContextMenu("Stop all Sounds")]
        public void StopAll()
        {
            foreach (Sound sound in sounds)
            {
                sound.audioSource.Stop();
            }
        }

        public void StopAll(float fadeOut)
        {
            foreach (Sound sound in sounds)
            {
                StartCoroutine(FadeOut(sound, fadeOut));
            }
        }

        IEnumerator FadeIn(Sound sound, float fadeInAmount)
        {
            float timeElapsed = 0;
            while (sound.volume < 1)
            {
                sound.volume = Mathf.Lerp(0, 1, timeElapsed / fadeInAmount);
                sound.audioSource.volume = sound.volume;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator FadeOut(Sound sound, float fadeOutAmount)
        {
            float timeElapsed = 0;

            if (timeElapsed >= sound.audioClip.length)
            {
                if (sound.volume <= 0)
                {
                    sound.audioSource.Stop();
                    // if (GetComponent<BeatAndAudioSync>() != null)
                    //     BeatAndAudioSync.Instance.StopBeatSync(sound);
                }
                else
                {
                    if (sound.volume > 0)
                    {
                        sound.volume = Mathf.Lerp(1, 0, timeElapsed / fadeOutAmount);
                        sound.audioSource.volume = sound.volume;
                        yield return null;
                    }
                }
            }
            timeElapsed += Time.deltaTime;
        }

        public Sound getSound(string name)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.name == name)
                    return sound;
            }
            return null;
        }
    }
}
