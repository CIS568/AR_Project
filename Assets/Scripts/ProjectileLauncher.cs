namespace MyFirstARGame
{
    using UnityEngine;
    using Photon.Pun;

    /// <summary>
    /// Launches projectiles from a touch point with the specified <see cref="initialSpeed"/>.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class ProjectileLauncher : PressInputBase
    {
        [SerializeField] private ProjectileBehaviour projectilePrefab;

        [SerializeField] private ShieldBehavior shieldPrefab;

        [SerializeField]
        private float initialSpeed = 25;

        protected override void OnPressBegan(Vector3 position)
        {
            if (this.projectilePrefab == null || !NetworkLauncher.Singleton.HasJoinedRoom)
                return;

            // Ensure user is not doing anything else.
            var uiButtons = FindObjectOfType<UIButtons>();
            if (uiButtons != null && (uiButtons.IsPointOverUI(position) || !uiButtons.IsIdle))
                return;

            // We send our current player number as data so that the projectile can pick its material based on the player that owns it.
            var initialData = new object[] { PhotonNetwork.LocalPlayer.ActorNumber };

            // Cast a ray from the touch point to the world. We use the camera position as the origin and the ray direction as the
            // velocity direction.
            var ray = this.GetComponent<Camera>().ScreenPointToRay(position);

            var obj = GameLogic.IsOffensive() ? projectilePrefab.name : shieldPrefab.name ;
            var projectile = PhotonNetwork.Instantiate(obj, ray.origin, Quaternion.identity, data: initialData);

            // By default, the projectile is kinematic in the prefab. This is because it should not be affected by physics
            // on clients other than the one owning it. Hence we disable kinematic mode and let the physics engine take over here.
            // It might make sense to have all game physics run on the server for a more complex scenario. You could transfer
            // ownership here to the server.

            if (GameLogic.IsOffensive())
            {
                var rg = projectile.GetComponent<Rigidbody>();
                rg.useGravity = true;
                rg.velocity = ray.direction * initialSpeed;
            }
            else
            {
                projectile.transform.position += transform.forward * 0.2f;
            }
        }
    }
}
