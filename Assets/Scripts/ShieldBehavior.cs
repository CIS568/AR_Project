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
            if (_destroyed)
            {
                return;
            }

            _timer += Time.deltaTime;

            if (_timer >= 2.0f)
            {
                var view = gameObject.GetPhotonView();
                GameLogic.Comm.Destroy(view.ViewID);
                GameLogic.Comm.UpdateScore(view.Owner, GameLogic.Comm.GetScore(view.Owner) + 1);

                _destroyed = true;
            }
        }
    }
}
