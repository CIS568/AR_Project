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

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnCollisionEnter(Collision other)
        {
            var proj = other.gameObject.GetComponent<ProjectileBehaviour>();
            if (proj)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
