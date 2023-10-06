using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFirstARGame
{
    public class ScoreBoard : MonoBehaviour
    {
        private Dictionary<string, int> scores = new Dictionary<string, int>();

        public void SetScore(string playerName, int score)
        {
            if (scores.ContainsKey(playerName))
            {
                scores[playerName] = score;
            }
            else
            {
                scores.Add(playerName, score);
            }
        }

        public int GetScore(string playerName)
        {
            if (scores.ContainsKey(playerName))
            {
                return scores[playerName];
            }
            else
            {
                return 0;
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            foreach (var score in scores)
            {
                GUILayout.Label($"{score.Key}: {score.Value}", new GUIStyle
                {
                    normal = new GUIStyleState { textColor = Color.black },
                    fontSize = 22
                });
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
