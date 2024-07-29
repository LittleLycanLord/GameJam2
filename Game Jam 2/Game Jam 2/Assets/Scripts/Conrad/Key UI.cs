using LilLycanLord_Official;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace LilLycanLord_Official
{
    public class KeyUI : MonoBehaviour
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
        KeyCollection player;

        [SerializeField]
        Sprite keyFoundSprite;

        [SerializeField]
        Image key1;

        [SerializeField]
        Image key2;

        [SerializeField]
        Image key3;

        //* ╔════════════╗
        //* ║ Attributes ║
        //* ╚════════════╝

        //* ╔═══════════════╗
        //* ║ Monobehaviour ║
        //* ╚═══════════════╝
        void Awake() { }

        void Start() { }

        void Update()
        {
            switch (player.keys)
            {
                case 1:
                    key1.sprite = keyFoundSprite;
                    break;
                case 2:
                    key2.sprite = keyFoundSprite;
                    break;
                case 3:
                    key3.sprite = keyFoundSprite;
                    break;
            }
        }

        //* ╔═════════════════════╗
        //* ║ Non - Monobehaviour ║
        //* ╚═════════════════════╝
    }
}
