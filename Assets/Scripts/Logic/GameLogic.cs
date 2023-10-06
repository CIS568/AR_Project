using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace MyFirstARGame
{
    public class GameLogic : MonoBehaviour
    {
        private static NetworkCommunication _comm;
        public static NetworkCommunication Comm
        {
            get
            {
                if (_comm == null)
                {
                    _comm = FindObjectOfType<NetworkCommunication>();
                }
                return _comm;
            }
        }

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
