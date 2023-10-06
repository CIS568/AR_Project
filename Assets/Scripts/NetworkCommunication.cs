namespace MyFirstARGame
{
    using Photon.Pun;
    using UnityEngine;
    
    /// <summary>
    /// You can use this class to make RPC calls between the clients. It is already spawned on each client with networking capabilities.
    /// </summary>
    public class NetworkCommunication : MonoBehaviourPun
    {
        [SerializeField] private ScoreBoard scoreBoard;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"IsMaster: {PhotonNetwork.IsMasterClient}");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncrementScore()
        {
            var pName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var curScore = scoreBoard.GetScore(pName) + 1;
            photonView.RPC("Network_SetPlayerScore", RpcTarget.All, pName, curScore);
        }
        public void UpdateForNewPlayer(Photon.Realtime.Player player)
        {
            var pName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var curScore = scoreBoard.GetScore(pName);
            photonView.RPC("Network_SetPlayerScore", player, pName, curScore);
        }

        public void Destroy(int viewId)
        {
            photonView.RPC("Network_Destroy", RpcTarget.All, viewId);
        }

        [PunRPC]
        public void Network_SetPlayerScore(string playerName, int newScore)
        {
            scoreBoard.SetScore(playerName, newScore);
        }

        [PunRPC]
        public void Network_Destroy(int viewId)
        {
            PhotonView targetShield = PhotonView.Find(viewId);
            if (targetShield != null)
            {
                Destroy(targetShield.gameObject);
            }
        }
    }

}