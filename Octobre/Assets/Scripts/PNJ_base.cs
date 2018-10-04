using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PNJ_base : MonoBehaviour {


    /** BASE COMMUNE A TOUS LES PNJ
     * Contient les fonctions de base des pnj, qui seront appelé dans leur propre behaviour
     * 
     * LookAtPlayer
     * ListenToPlayer: si le player fait une emote et que le pnj est dans le range, capte l'emote (a utiliser en Update du behaviour)
     * X Talk: ouvre une boite de dialogue avec un texte (behaviour) dedans, click D fait passer/changer.
     * GoTo: Utile pour les cinematique. Location, vitesse
     * 
     */

    /* 
     * Click droit pres d'un pnj (dans le range de la boite de collision spaciale du pnj)
     * Talk(array)
     *      Si boite fermée:
     *          LookAtPlayer()
     *          Instanciate un canvas de texte au dessus de la tete du pnj (pos tete est une publique qui a un set par default, a reguler en fonction du pnj)
     *          Display le premier truc de l'array, lettre par lettre (vitesse publique, a twik en fonction du pnj)
     *      Si boite ouverte:
     *          Display le truc plus un de l'array (regarde le truc suivant, si c'est un trigger, le lance)
     *          Si array finie
     *              Detruit l'instance.
     *              Reset le i
     * Si PJ sort de la range de parlotte du PNJ
     *      Destroy l'instance
     *      Reset le i
     * 
     * 
     */
    public GameObject Player;
    public float HeadX;
    public float HeadY;
    public Text Textbox;
    public Canvas canvas;
    public float TalkRange;
    public float HearRange;
    public Text TextboxInstance;
    public int Bookmark;
    public float typingSpeed;



    public void Talk(string[] array)
    {
        if (TextboxInstance == null)
        {
            //TODO LookAtPlayer()
            TextboxInstance = Instantiate(Textbox, new Vector2(HeadX, HeadY), Quaternion.identity);
            TextboxInstance.transform.SetParent(canvas.transform, true);
            TextboxInstance.text = array[0].ToString();
            Bookmark = 0;
        }
        else
        {
            Bookmark += 1;
            if(Bookmark > array.Length)
            {
                Destroy(TextboxInstance.gameObject);
            }
            else
            {
                TextboxInstance.text = array[Bookmark].ToString();
                //TODO verifier les trigger
                //TODO lettre par lettre
            }

        }

        /*
        IEnumerator Type()
        {

            foreach (char letter in array[Bookmark].ToCharArray())
            {
                TextboxInstance.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            
    }*/
    }



    // Use this for initialization
    void Start () {
	}


}
