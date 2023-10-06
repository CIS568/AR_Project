using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace MyFirstARGame
{
    public class MineBehavior : MonoBehaviour
    {
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
                    GameLogic.Comm.Destroy(view.ViewID);

                    _destroyed = true;
                }
            }
        }
    }
}
