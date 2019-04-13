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

   public Classe classe_precedente = Classe.GOD;
   public  double Hp;
   public  double Atk;
   public  double Def;
   public  double Pm;
   public  double Mana;
   public  double Distance = 1;
   public  double ATK_distance = 0;
   public Classe classe = Classe.GOD;

   public void SetClasse(Classe classe)
   {
      switch (classe)
      {
         case Classe.GUERRIER:
            this.Atk = 100;
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

   }

   public void change()
   {
      this.classe = classe;
      SetClasse(this.classe);
   }

   private void Update()
   {
      if (classe_precedente != classe)
      {
         change();
      }
   }
}


