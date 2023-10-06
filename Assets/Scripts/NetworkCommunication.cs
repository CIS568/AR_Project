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

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncrementScore()
        {
            var pName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var curScore = scoreBoard.GetScore(pName);
            photonView.RPC("Network_SetPlayerScore", RpcTarget.All, pName, curScore);
        }
        public void UpdateForNewPlayer(Photon.Realtime.Player player)
        {
            if (player == null) return;
            var pName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
            var curScore = scoreBoard.GetScore(pName);
            photonView.RPC("Network_SetPlayerScore", player, pName, curScore);
        }

        [PunRPC]
        public void Network_SetPlayerScore(string playerName, int newScore)
        {
            scoreBoard.SetScore(playerName, newScore);
        }
    }

}