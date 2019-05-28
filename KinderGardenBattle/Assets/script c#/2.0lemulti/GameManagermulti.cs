using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManagermulti : MonoBehaviourPunCallbacks
{
    public static bool turn = true;//synchronise the turn with the master client
    private static bool ancien_turn = turn;
    public static bool Move_Button;
    public static bool ATK_Button;
    public static bool is_in_menu = false;
    public static bool want_to_change_classe = false;
    public Perso_Generiquemulti vis;
    public GameObject Stat;
    public static bool has_change_classe = false;
    public static int futur_classe;

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            float a = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 0f : -3f;
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(a, 2f, a), Quaternion.identity, 0);
        }
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.CompareTag("Button"))
                go.SetActive(PhotonNetwork.IsMasterClient ? turn : !turn);//go.activeSelf
        }
        Move_Button = false;
        ATK_Button = false;
        TeamTurn = Team.Blue;
        J1 = true;
        J2 = J3 = J4 = false;        
    }

    [PunRPC]
    public void Set_turn() { photonView.RPC("Lel", RpcTarget.All); }

    [PunRPC]
    void Lel() { turn = !turn; }

    public enum Team
    {
        Blue, Red
    }

    public static bool J1;
    public static bool J2;
    public static bool J3;
    public static bool J4;
    
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
    public void setfuturclasse(int i)
    {
        futur_classe = i;
    }

    public void confirmclasse()
    {
        has_change_classe = true;
    }
    public static bool NomDuJoeurVersUneBool(string name)
    {
        switch (name)
        {
            case "plamulti(Clone)":
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
    public static string TeamtoString(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                return "BLEU";
            case Team.Red:
                return "RED";
            default:
                return "ERROR";
        }
    }

    public static Team TeamTurn;
    public static bool booleen = false;

    public void Button_Moove()
    {

        Move_Button = !Move_Button;
        ATK_Button = false;
        Debug.Log("MOVE " + Move_Button);
        Stat.SetActive(false);
    }
    public void Button_ATK()
    {
        ATK_Button = !ATK_Button;
        Move_Button = false;
        Debug.Log("ATK" + ATK_Button);
        Stat.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menu();
        }
        if (!Move_Button && !ATK_Button)
        {
            show_stat();
        }
        if (turn != ancien_turn)
        {
            FinDeTour();
            ancien_turn = turn;
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
                if (hit.transform != null && (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("NPC")))
                {
                    GameObject npc = hit.transform.gameObject;                    //manager_cible= npc.GetComponent<GameManagerSolo>();
                    vis = npc.GetComponent<Perso_Generiquemulti>();
                    Stat.SetActive(true);
                    Player_Statsmulti s = Stat.GetComponent<Player_Statsmulti>();
                    s.set_player_stats(vis);
                }
                else
                {
                    if (hit.transform != null && hit.transform.gameObject.CompareTag("Box"))
                    {
                        menuselectionclasse();
                        Debug.Log("sortie de fonction");
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

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
       
    public static void SetTeamTurn(Team Color)
    {
        TeamTurn = Color;
    }

    public void FinDeTour()
    {
        TeamTurn = (TeamTurn == Team.Blue) ? Team.Red : Team.Blue;
        Move_Button = false;
        ATK_Button = false;
        Stat.SetActive(false);
        Debug.Log("Tours de l'equipe : " + TeamtoString(TeamTurn));

        GameObject[] liste = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject VARIABLE in liste)
        {
            PlayerMovemulti script = VARIABLE.GetComponent<PlayerMovemulti>();
            script.hasmooved = false;
        }
    }
}