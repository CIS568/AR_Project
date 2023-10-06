using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace MyFirstARGame
{
    public class GameLogic : MonoBehaviour
    {
        private static NetworkCommunication _comm;
        private static NetworkCommunication Comm
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

        public static void IncrementScore()
        {
            Comm.IncrementScore();
        }

        public static void Destroy(int id)
        {
            Comm.Destroy(id);
        }
    }
}
