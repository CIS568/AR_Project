namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;

    /// <summary>
    /// Controls projectile behaviour. In our case it currently only changes the material of the projectile based on the player that owns it.
    /// </summary>
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Material[] projectileMaterials;

        private void Awake()
        {
            // Pick a material based on our player number so that we can distinguish between projectiles. We use the player number
            // but wrap around if we have more players than materials. This number was passed to us when the projectile was instantiated.
            // See ProjectileLauncher.cs for more details.
            var photonView = this.transform.GetComponent<PhotonView>();
            var playerId = Mathf.Max((int)photonView.InstantiationData[0], 0);
            if (this.projectileMaterials.Length > 0)
            {
                var material = this.projectileMaterials[playerId % this.projectileMaterials.Length];
                this.transform.GetComponent<Renderer>().material = material;
            }
        }

        void OnCollisionEnter(Collision other)
        {
            var proj = other.gameObject.GetComponent<ShieldBehavior>();
            if (proj && PhotonNetwork.IsMasterClient)
            {
                var view = other.gameObject.GetPhotonView();
                GameLogic.Comm.Destroy(view.ViewID);
                GameLogic.Comm.UpdateScore(view.Owner, GameLogic.Comm.GetScore(view.Owner) + 1);
            }
        }

        void Update()
        {
        }
    }
}
