using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace LilLycanLord_Official
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Sound", menuName = "LilLycanLord/Audio/Sound")]
    public class Sound : ScriptableObject
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
        [Space(10)]
        [Header("Audio Details")]
        public AudioClip audioClip;
        public float BPM = 0;

        [Space(10)]
        [Header("AudioSource Attributes")]
        public bool loop = false;

        [Range(0.1f, 2.0f)]
        public float pitch = 1.0f;

        [Range(0.1f, 3.0f)]
        public float volume = 1.0f;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝
        [NonSerialized]
        public AudioSource audioSource;

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
    }
}
