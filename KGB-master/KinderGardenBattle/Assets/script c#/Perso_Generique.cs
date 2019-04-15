using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perso_Generique : MonoBehaviour
{
   public enum Classe
   {
      GUERRIER,
      NINJA,
      FRONDEUR,
      TANK,
      HEALER,
      PIRATE,
      MAGE,
      GOD

   }

   public static Classe classe_precedente = Classe.GOD;
   public  static double Hp;
   public static  double Atk;
   public  static double Def;
   public  static double Pm;
   public  static double Mana;
   public  static double Distance = 1;
   public  static double ATK_distance = 0;
   public  static  Classe classe = Classe.GOD;

   public static void  SetClasse(Classe Klasse)
   {
      switch (classe)
      {
         case Classe.GUERRIER:
            Atk = 100;
            Def = 0.7;
            Hp = 50;
            Pm = 5;
            Mana = 0;
            break;
         case Classe.GOD:
            Atk = 1000;
            Def = 0.01;
            Hp = 1000;
            Pm = 100;
            Mana = 1000;
            break;
         case Classe.TANK:
            Atk = 15;
            Def = 0.5;
            Hp = 100;
            Pm = 2;
            Mana = 0;
            break;
         case Classe.FRONDEUR:
            Distance = 3;
            Atk = 10;
            ATK_distance = 30;
            Def = 0.80;
            Hp = 75;
            Pm = 5;
            Mana = 0;
            break;
         case Classe.HEALER:
            Atk = 10;
            Def = 0.80;
            Hp = 75;
            Pm = 7;
            Mana = 0;
            break;
         case Classe.PIRATE:
            Distance = 2;
            ATK_distance = 20;
            Atk = 20;
            Def = 0.90;
            Hp = 75;
            Pm = 5;
            Mana = 0;
            break;
         case Classe.NINJA:
            Atk = 50;
            Hp = 60;
            Def = 1;
            Pm = 5;
            Mana = 0;
            break;
         case Classe.MAGE:
            Atk = 10;
            ATK_distance = 30; // + alteration de la cible ( genre psn ou brulé 
            Def = 1;
            Hp = 60;
            Pm = 5;
            Mana = 100;
            break;
      }
      return;

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

   public static void change(Classe Klasse)
   {
      classe = Klasse;
      SetClasse(classe);
   }

   public void ATK(NPC npc)
   {
      
   }
   private void Update()
   {

      if (classe_precedente != classe)
      {
         change( classe);
      }
   }
}


