using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace MyFirstARGame
{
    public class ShieldBehavior : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }


        private float _timer = 0.0f;

        private bool _destroyed = false;

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (_destroyed)
                {
                    return;
                }

                _timer += Time.deltaTime;

                if (_timer >= 5.0f)
                {
                    var view = gameObject.GetPhotonView();
                    GameLogic.Comm.UpdateScore(view.Owner, GameLogic.Comm.GetScore(view.Owner) + 1);
                    GameLogic.Comm.Destroy(view.ViewID);

                    _destroyed = true;
                }
            }
        }

        void OnGUI()
        {
/*            var p = Camera.main.WorldToScreenPoint(transform.position);

            var save = GUI.color;
            GUI.color = Color.blue;
            GUILayout.BeginArea(new Rect(p.x, Screen.height - p.y, 200, 200));
            GUILayout.Label($"Owner = Player {gameObject.GetPhotonView().Owner.ActorNumber}");
            GUILayout.EndArea();
            GUI.color = save;*/
        }
    }
}
