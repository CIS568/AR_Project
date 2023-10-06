using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFirstARGame
{
    public class Init : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            new GameObject("Init").AddComponent<Init>();
        }

        void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if(PhotonNetwork.IsConnected)
                    PhotonNetwork.Disconnect();
            }
        }
    }
}
