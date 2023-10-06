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

        private static GameLogic _ins = null;
        private bool _showGameOver = false;
        private string _gameOverMsg;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            _ins = new GameObject("GameLogic").AddComponent<GameLogic>();
        }

        public static bool IsDefending()
        {
            return PhotonNetwork.LocalPlayer.ActorNumber % 2 == 0;
        }

        public static bool IsOffensive()
        {
            return PhotonNetwork.LocalPlayer.ActorNumber % 2 == 1;
        }

        public void GameOverSeq(string message)
        {
            IEnumerator _routine()
            {
                _gameOverMsg = message;
                _showGameOver = true;
                yield return new WaitForSeconds(4);
                _showGameOver = false;

                Comm.RestartGame();
            }

            StartCoroutine(_routine());
        }

        public static void StartGameOverSeq(int winner)
        {
            var message = PhotonNetwork.LocalPlayer.ActorNumber == winner ? "You Win!!" : "You Lose!!";
            _ins.GameOverSeq(message);
        }

        void OnGUI()
        {
            if (_showGameOver)
            {
                var style = new GUIStyle();
                style.normal.textColor = Color.red;
                style.fontSize = 100;
                style.fontStyle = FontStyle.Bold;

                var pos = new Vector2(Screen.width, Screen.height) * 0.5f;
                var content = new GUIContent(_gameOverMsg);
                var size = style.CalcSize(content);
                pos -= size / 2;

                GUI.Label(new Rect(pos, size), content, style);
            }
        }
    }
}
