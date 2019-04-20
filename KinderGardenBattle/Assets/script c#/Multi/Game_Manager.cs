using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Multi
{
    public class Game_Manager : MonoBehaviourPunCallbacks
    {
        public  static bool turn =true;//synchronise the turn with the master client
        private static bool ancien_turn = turn;
        public static bool is_in_menu = false;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        #region Photon Callbacks

        void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from");
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                float a = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 0f : -5f;
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(a,1f,a), Quaternion.identity, 0);
            }
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.CompareTag("Button"))
                    go.SetActive(PhotonNetwork.IsMasterClient ? turn : !turn);//go.activeSelf
            }
        }


        [PunRPC]
        public void Set_turn() {photonView.RPC("Lel", RpcTarget.All); }

        [PunRPC]
        void Lel() { turn = !turn; }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                menu();
            }

            if (turn != ancien_turn)
            {
                EndTurn();
                ancien_turn = turn;
            }
        }

        public void menu()
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.CompareTag("Button"))
                    go.SetActive(is_in_menu && ((turn && PhotonNetwork.IsMasterClient) || (!turn && !PhotonNetwork.IsMasterClient)));//go.activeSelf
                if (go.CompareTag("Menupause"))
                    go.SetActive(!is_in_menu);
            }
            is_in_menu = !is_in_menu;
        }
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void Move()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player_manager_multi[] oui = player.GetComponents<Player_manager_multi>();
            foreach (Player_manager_multi p in oui)
            {
                p.Want_to_move = !p.Want_to_move;
                p.Resetalltiles();
                p.Want_to_fight = false;
            }
        }

        public void Attack()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player_manager_multi[] oui = player.GetComponents<Player_manager_multi>();
            foreach (Player_manager_multi p in oui)
            {
                if (!p.has_attack)
                    p.Want_to_fight = !p.Want_to_fight;
                p.Want_to_move = false;
                p.Resetalltiles();
            }
        }

        public void EndTurn()
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.CompareTag("Button"))
                    go.SetActive(PhotonNetwork.IsMasterClient ? turn : !turn);//go.activeSelf
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player_manager_multi[] oui = player.GetComponents<Player_manager_multi>();
            foreach (Player_manager_multi p in oui)
            {
                p.is_turn = !p.is_turn;
                p.Resetalltiles();
            }
            Debug.Log("fin de tours"+turn);
        }
        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            
        }


        #endregion

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel(2);
        }

        #endregion

        #region Photon Callbacks


        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }


        #endregion
    }
}
