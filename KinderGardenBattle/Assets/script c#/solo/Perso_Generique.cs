using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perso_Generique : MonoBehaviour
{
    public enum Classe 
    {
        GUERRIER =0,
        NINJA,
        FRONDEUR,
        TANK,
        HEALER,
        PIRATE,
        MAGE,
        GOD,
        KID
    }

    
    

    public string ClasseToString()
    {
        switch (classe)
        {
            case Classe.GUERRIER:
                return "GUERRIER";
            case Classe.GOD:
                return "GOD";
            case Classe.MAGE:
                return "MAGE";
            case Classe.TANK:
                return "TANK";
            case Classe.NINJA:
                return "NINJA";
            case Classe.HEALER :
                return "HEALER";
            case Classe.PIRATE:
                return "PIRATE";
            case Classe.FRONDEUR:
                return "Frondeur";
            case Classe.KID :
                return "Kid";
            default:
                return "wtf";
        }
    }

    
    public  Classe classe_precedente = Classe.GOD;
    public   double Hp;
    public double Max_hp;
    public   double Atk;
    public   double Def;
    public   double Pm;
    public   double Mana;
    public   double Distance = 1;
    public  bool vivant = true;
    public bool selection = false;
    public   double ATK_distance = 0;
    public    Classe classe = Classe.GOD;
    public PlayerMove PlayerMove;
    [SerializeField] private GameObject guerrier;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject ninja;
    [SerializeField] private GameObject pirate;
    [SerializeField] private GameObject tank;
    [SerializeField] private GameObject healer;
    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject lancepierre;
    [SerializeField] private GameObject batondemage;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject nerf;
    [SerializeField] private GameObject bouclier;
    [SerializeField] private GameObject sabre;
    [SerializeField] private GameObject katana;
    [SerializeField] private GameObject grosbouclier;
    [SerializeField] private GameObject malette;
    [SerializeField] private GameObject Cheveux;
    public  void  SetClasse(Classe Klasse)
    {
        guerrier.SetActive(false);
        archer.SetActive(false);
        ninja.SetActive(false);
        pirate.SetActive(false);
        tank.SetActive(false);
        healer.SetActive(false);
        mage.SetActive(false);
        lancepierre.SetActive(false);
        batondemage.SetActive(false);
        sword.SetActive(false);
        nerf.SetActive(false);
        bouclier.SetActive(false);
        sabre.SetActive(false);
        katana.SetActive(false);
        grosbouclier.SetActive(false);
        Cheveux.SetActive(false);
        malette.SetActive(false);
        switch (Klasse)
        {
            case Classe.GUERRIER:
            guerrier.SetActive(true);
                sword.SetActive(true);
                bouclier.SetActive(true);
                Atk = 30;
            Def = 0.7;
            Max_hp = 200;
            Pm = 5;
            Mana = 0;
            break;
            case Classe.GOD:
            Atk = 1000;
            Def = 0.01;
            Max_hp = 1000;
            Pm = 100;
            Mana = 1000;
            break;
            case Classe.TANK:
                tank.SetActive(true);
                grosbouclier.SetActive(true);
                Atk = 15;
            Def = 0.5;
            Max_hp = 300;
            Pm = 2;
            Mana = 0;
            break;
            case Classe.FRONDEUR:
            archer.SetActive(true);
                lancepierre.SetActive(true);
                Distance = 3;
            Atk = 30;
            ATK_distance = 30;
            Def = 0.80;
            Max_hp = 180;
            Pm = 5;
            Mana = 0;
            break;
            case Classe.HEALER:
                healer.SetActive(true);
                malette.SetActive(true);
                Atk = 10;
            Def = 0.80;
            Max_hp = 160;
            Pm = 7;
            Mana = 0;
            break;
            case Classe.PIRATE:
                pirate.SetActive(true);
                sabre.SetActive(true);
                nerf.SetActive(true);
                Distance = 2;
            ATK_distance = 20;
            Atk = 20;
            Def = 0.90;
            Max_hp = 160;
            Pm = 5;
            Mana = 0;
            break;
            case Classe.NINJA:
                ninja.SetActive(true);
                katana.SetActive(true);
                Atk = 50;
            Max_hp = 100;
            Def = 1;
            Pm = 5;
            Mana = 0;
            break;
            case Classe.MAGE:
                mage.SetActive(true);
                batondemage.SetActive(true);
                Atk = 20;
            ATK_distance = 20;
            Def = 1;
            Distance = 4;
            Max_hp = 80;
            Pm = 5;
            Mana = 100;
            break;
            case Classe.KID :
                Cheveux.SetActive(true);
                Atk = 0;
                Max_hp = 500;
                Def = 5;
                Pm = 5;
                Mana = 0;
                break;
        }
        Hp = Max_hp < Hp ? Max_hp : Hp;
        if (gameObject.tag == "Player")
        {
            PlayerMove = gameObject.GetComponent<PlayerMove>();
            PlayerMove.move = (int)Pm;
            PlayerMove.max_move = (int)Pm;
        }
    }

    public  double NewPV (int atk )
    {
        if (atk <0)
        {
            int penis = Convert.ToInt32(this.Hp - atk);
            Hp = penis>Max_hp?Max_hp:penis;
        }
        else
        {


            int penis = Convert.ToInt32(this.Hp - this.Def * atk);


            Hp = penis;

            Animator anim = gameObject.GetComponent<TacticsMove>().anim;
            anim.SetTrigger("Degat");
        }

        return Hp;
    }
    public void int_to_classe(int i)
    {
        classe = (Classe)i;
    }

    public  void  FindTarget()
    {
        RaycastHit hit;
        
        Debug.DrawRay(transform.position,transform.up*10, Color.red);
        if (Physics.Raycast(transform.position, transform.up*10, out hit, 10))
        {
            Debug.Log("penis" + hit);
        }

    }

    public  void change(Classe Klasse)
    {
        classe = Klasse;
        classe_precedente = Klasse;
        SetClasse(classe);
    }

    public double GetCara(Perso_Generique persoGenerique,string demande) // demande en majuscule "HP" par exemple
    {
        switch (demande)
        {
            case "HP":
                return persoGenerique.Hp;
           
            case "ATK":
                return persoGenerique.Atk;
           
            case "DEF":
                return persoGenerique.Def;
           
            case "PM":
                return persoGenerique.Pm;
           
            case "MANA":
                return persoGenerique.Mana;
           
            case "DISTANCE":
                return persoGenerique.Distance;
           
            case "ATKDISTANCE":
                return persoGenerique.ATK_distance;
           
            default:
                return -1;           
        }
    }

    public void Verification()
    {
        vivant = !(Hp < 0);
        
    }

    private void Start()
    {
        SetClasse(classe);
        Hp = Max_hp;        
    }

    private void Update()
    {
        Verification();
        if (!vivant)
        {
            Animator anim = gameObject.GetComponent<TacticsMove>().anim;
            anim.SetTrigger("Mort");
            anim.SetBool("Muerte", true);
            if (anim.GetBool("Destroy"))
            {
                Destroy(gameObject);
            }
            //gameObject.SetActive(false);
        }
      if (classe_precedente != classe)
      {
         change( classe);
      }
    }
}


