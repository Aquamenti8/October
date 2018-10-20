using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupCircle : MonoBehaviour {


    [SerializeField] private GameObject questionMark;

    private GameObject player;
    private GameObject actionCollider;

    public InventoryReminder theReminder;
    public GameObject Menu;
    public string activeMenu;

    public bool calling;
    public string callingName;

    public Text debugText;

    private MenuBehaviour menuB;

    public string nearNPC = "";
  
    //Detect objects
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            questionMark.SetActive(true);
            debugText.text = "Pick " + other.gameObject.GetComponent<ITEM>().collectable.name;
            //Collect object
            if (Input.GetMouseButtonDown(0))
            {
                
                string ItemToCollectName = other.gameObject.GetComponent<ITEM>().collectable.name;
               Destroy(other.gameObject);
                

                theReminder.PickupCollectable(ItemToCollectName);
                this.GetComponent<MenuBehaviour>().RollMenu();
                this.GetComponent<MenuBehaviour>().UpdateSprite();
                
                Debug.Log("Collect!");

                player.GetComponent<Animator>().SetTrigger("Pickup");
            }
        }
        else
        {
            questionMark.SetActive(false);
        }
    }

    void Start()
    {
        activeMenu = "";
        theReminder = GameObject.Find("TheReminder").GetComponent<InventoryReminder>();
        menuB = this.GetComponent<MenuBehaviour>();
        player = GameObject.Find("Player");
    }

    //GetInputs
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            theReminder.CleanInventory();
            menuB.RollMenu();
            menuB.UpdateSprite();
            
        }
        if (nearNPC != "")
        {
            debugText.text = "Talk to " + nearNPC;
        }

        if (Input.GetMouseButtonDown(1)) //OPEN MENU
        {
            if (activeMenu == "") {
                Menu.SetActive(true);
                activeMenu = "Main";
                menuB.ChangeCurrent(0);
                menuB.UpdateSprite();
            }
            else { activeMenu = ""; Menu.SetActive(false); }
        }

        if (activeMenu == "Inventory")
        {
            if (menuB.Inv.item.Count == menuB.current) {debugText.text = "Empty";}
            else{
                debugText.text = menuB.Inv.item[menuB.current];
                if (nearNPC != "")
                {
                    debugText.text = "Give "+ menuB.Inv.item[menuB.current];
                }
            }

            if(Input.GetMouseButtonDown(0)){

                theReminder.UseCollectable(menuB.current);
                menuB.RollMenu();
                menuB.UpdateSprite();
                activeMenu = "";
                Menu.SetActive(false);
            }

        }

        if (calling) { calling = false; callingName = ""; }

        if (activeMenu == "Call") {
            debugText.text = menuB.Inv.faces[menuB.current];
            if (Input.GetMouseButtonDown(0))
            {
                activeMenu = "";
                Menu.SetActive(false);
                calling = true;
                callingName = menuB.Inv.faces[menuB.current];


            }
        }

        if (activeMenu == "Main")
        {
            debugText.text = menuB.Inv.mainOptions[menuB.current];
            if (Input.GetMouseButtonDown(0))
            {
                if (menuB.current == 0) // si c'est CALL
                {
                    //OPEN CALL
                    activeMenu = "Call";
                    menuB.ChangeCurrent(0);
                    menuB.UpdateSprite();

                }
                if (menuB.current == 1) // si c'est INVENTORY
                {
                    //OPEN INVENTORY
                    activeMenu = "Inventory";
                    menuB.ChangeCurrent(0);
                    menuB.UpdateSprite();
                }
                if (menuB.current == 2) // si c'est SLEEP
                {
                    //SLEEP
                }
            }
        }


        if (activeMenu == "" && !questionMark.activeSelf && nearNPC == "")
        {
            debugText.text = "";
        }




    }


}
