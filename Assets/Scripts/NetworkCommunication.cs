namespace MyFirstARGame
{
    using Photon.Pun;
    using UnityEngine;
    using Photon.Realtime;
    
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

        public int GetScore(Player player)
        {
            return scoreBoard.GetScore($"Player {player.ActorNumber}");
        }

        public void UpdateScore(Player player, int score)
        {
            var pName = $"Player {player.ActorNumber}";
            photonView.RPC("Network_SetPlayerScore", RpcTarget.All, pName, score);
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
                if (targetShield.IsMine)
                {
                    PhotonNetwork.Destroy(targetShield.gameObject);
                }

                // Destroy(targetShield.gameObject);
            }
        }
    }

}