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
        public static bool want_to_change_classe = false;
        public static bool has_change_classe = false;
        public static int futur_classe;
        public GameObject Stat;
        private Perso_Generique_multi vis;
        public static bool J1=true;
        public static bool J2;
        public static bool J3;
        public static bool J4;

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
                float a = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 1f : -5f;
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(a,0.7f,a), Quaternion.identity, 0);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(2*a, 0.7f, a), Quaternion.identity, 0);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(3*a, 0.7f, a), Quaternion.identity, 0);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(4*a, 0.7f, a), Quaternion.identity, 0);
                GameObject[] oui = GameObject.FindGameObjectsWithTag("Player");                
                for (int i=0; i< oui.Length; i++)
                {
                    oui[i].name = (i+1)+"";
                }                
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
            Player_manager_multi p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_manager_multi>();
            if (!p.is_turn || (!p.Want_to_fight && !p.Want_to_move))
            {
                show_stat();
            }
            if(Stat.activeInHierarchy)
            {
                Player_Stats_multi s = Stat.GetComponent<Player_Stats_multi>();
                s.set_player_stats(vis);
            }

        }

        public void QuelJoueur()
        {
            if (J1)
            {
                J1 = J2;
                J2 = true;
            }

            else
            {
                if (J2)
                {
                    J2 = !J2;
                    J3 = true;
                }
                else
                {
                    if (J3)
                    {
                        J3 = !J3;
                        J4 = true;
                    }
                    else
                    {
                        J4 = false;
                        J1 = true;
                    }
                }
            }

        }

        public static bool NomDuJoeurVersUneBool(string name)
        {
            switch (name)
            {
                case "1":
                    return J1;
                case "2":
                    return J2;
                case "3":
                    return J3;
                case "4":
                    return J4;
                default:
                    return false;

            }
        }

        public void show_stat()
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 25.0f))
                {
                    if (hit.transform != null && hit.transform.gameObject.CompareTag("Player"))
                    {
                        GameObject npc = hit.transform.gameObject;                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                        vis = npc.GetComponent<Perso_Generique_multi>();
                        Player_Stats_multi s = Stat.GetComponent<Player_Stats_multi>();
                        s.set_player_stats(vis);
                        Stat.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("caca");
                        if (hit.transform != null && hit.transform.gameObject.CompareTag("Box"))
                        {
                            Debug.Log("prout");
                            menuselectionclasse();
                        }
                    }
                }
            }
        }

        public void menuselectionclasse()
        {
            Debug.Log("entree");
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.CompareTag("Button"))
                    go.SetActive(want_to_change_classe);//go.activeSelf
                if (go.CompareTag("ChangeClasse"))
                    go.SetActive(!want_to_change_classe);
            }
            Stat.SetActive(false);
            want_to_change_classe = !want_to_change_classe;
        }

        public void setfuturclasse(int i)
        {
            futur_classe = i;
        }

        public void confirmclasse()
        {
            has_change_classe = true;
        }

        public void menu()
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                Debug.Log("change clase" + want_to_change_classe);
                if (go.CompareTag("Button"))
                    go.SetActive(is_in_menu ? !want_to_change_classe : is_in_menu);//go.activeSelf
                if (go.CompareTag("Menupause"))
                    go.SetActive(!is_in_menu);
                if (go.CompareTag("ChangeClasse"))
                    go.SetActive(is_in_menu ? want_to_change_classe : is_in_menu);

            }
            Stat.SetActive(false);
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
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            Player_manager_multi p;
            foreach (GameObject oui in player)
            {
                p = oui.GetComponent<Player_manager_multi>();
                if (p.move!=0)
                    p.Want_to_move = !p.Want_to_move;
                p.Resetalltiles();
                p.Want_to_fight = false;
                Stat.SetActive(false);
            }
        }

        public void Attack()
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            Player_manager_multi p;
            foreach (GameObject oui in player)
            {
                p = oui.GetComponent<Player_manager_multi>();
                if (!p.has_attack)
                    p.Want_to_fight = !p.Want_to_fight;
                p.Want_to_move = false;
                p.Resetalltiles();
                Stat.SetActive(false);
            }
        }

        public void EndTurn()
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.CompareTag("Button"))
                    go.SetActive(PhotonNetwork.IsMasterClient ? turn : !turn);//go.activeSelf
            }
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            Player_manager_multi p;
            foreach (GameObject oui in player)
            {
                p = oui.GetComponent<Player_manager_multi>();
                p.is_turn = !p.is_turn;
                p.Resetalltiles();
                p.Want_to_fight = false;
                p.Want_to_move = false;
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
