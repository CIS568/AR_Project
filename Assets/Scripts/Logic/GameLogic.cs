using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace MyFirstARGame
{
    public class GameLogic : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            new GameObject("GameLogic").AddComponent<GameLogic>();
        }

        public static bool IsDefending()
        {
            return PhotonNetwork.LocalPlayer.ActorNumber % 2 == 0;
        }

        public static bool IsOffensive()
        {
            return PhotonNetwork.LocalPlayer.ActorNumber % 2 == 1;
        }
    }
}
