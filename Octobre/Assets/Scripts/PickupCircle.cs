using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupCircle : MonoBehaviour {


    private GameObject player;
    private GameObject actionCollider;

    public InventoryReminder theReminder;
    public GameObject Menu;
    public string activeMenu;

    public bool calling;
    public bool callingtwo;
    public string callingName;

    public Text debugText;

    private MenuBehaviour menuB;

    public string nearNPC = "";
    public string nearWarp = "";
    public bool nearObject = false;
    public bool giving = false;
  
    //Detect objects
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
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
                nearObject = true;
            }
        }
        else
        {
            nearObject = false;
        }
    }

    void Start()
    {
        activeMenu = "";
        theReminder = GameObject.Find("TheReminder").GetComponent<InventoryReminder>();
        menuB = this.GetComponent<MenuBehaviour>();
        player = GameObject.Find("Player");
        player.GetComponent<Animator>().SetTrigger("Sleep");
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
            if (theReminder.faces.Contains(nearNPC))
            {
                debugText.text = "Talk to " + nearNPC;
            }
            else debugText.text = "Talk to ????";
        }
        if( nearWarp != "")
        {
            debugText.text = "Go to " + nearWarp;
        }
        if (Input.GetMouseButtonDown(1) && !player.GetComponent<Player_movment>().charging && !player.GetComponent<Player_movment>().cutscene && !player.GetComponent<Player_movment>().thinking) //OPEN MENU
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

                if (nearNPC != "")
                {
                    if (menuB.Inv.item.Count != menuB.current)
                    {
                        StartCoroutine("Give");
                        theReminder.GiveCollectable(menuB.current);
                        player.GetComponent<Player_movment>().dontMove = true;
                        menuB.RollMenu();
                        menuB.UpdateSprite();
                        activeMenu = "";
                        Menu.SetActive(false);
                    }
                }
                else
                {
                    theReminder.UseCollectable(menuB.current);
                    menuB.RollMenu();
                    menuB.UpdateSprite();
                    activeMenu = "";
                    Menu.SetActive(false);
                }
            }

        }

        

        if (activeMenu == "Call") {
            debugText.text = menuB.Inv.faces[menuB.current];
            if (Input.GetMouseButtonDown(0))
            {
                activeMenu = "";
                Menu.SetActive(false);
                callingName = menuB.Inv.faces[menuB.current];
                StartCoroutine("CallEffect");
                

            }
        }

        if (callingtwo) { calling = false; callingName = ""; callingtwo = false; }
        if (calling) { callingtwo = true; }
        

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
                    player.GetComponent<Animator>().SetTrigger("Sleep");
                    activeMenu = "";
                    player.GetComponent<Player_movment>().sleep = true;
                    Menu.SetActive(false);
                }
            }
        }


        if (activeMenu == "" && nearNPC == "" && nearWarp =="" && !nearObject)
        {
            debugText.text = "";
        }

    }

    IEnumerator Give()
    {
        player.GetComponent<Animator>().SetBool("Giving", true);
        yield return new WaitForSeconds(0.5f);
        giving = true;
        Debug.Log("giving");
    }

    public GameObject CallEffectGM;
    IEnumerator CallEffect()
    {
        // RECUP LE MASK ID, le fou sur le sprite renderer de l'objet call effect
        string name = callingName;
        Sprite mask = null;
        switch (name)
        {
            case "Unknown":
                mask = menuB.Mask0;
                break;
            case "Kolimo":
                mask = menuB.Mask3;
                break;
            case "Keeper":
                mask = menuB.Mask1;
                break;
            case "Dalima":
                mask = menuB.Mask2;
                break;
            case "Cleo":
                mask = menuB.Mask5;
                break;
            case "Morgana":
                mask = menuB.Mask4;
                break;
        }
        SpriteRenderer CaEffSp = CallEffectGM.GetComponent<SpriteRenderer>();
        CaEffSp.sprite = mask;
        CallEffectGM.SetActive(true);
        //while a>0 le faire grandir et disparaitre
        CallEffectGM.transform.localScale = new Vector2(1, 1);
        float a = 1;
        float scalePas = 0.2f;
        float aSpeed = 2;
        while (a > 0)
        {
            CallEffectGM.transform.localScale = new Vector2(CallEffectGM.transform.localScale.x+scalePas, CallEffectGM.transform.localScale.y+scalePas);
            CaEffSp.color = new Color(1, 1, 1, a);
            a -= Time.deltaTime* aSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        

        calling = true;
        callingName = name;
        Debug.Log("call");
        CallEffectGM.SetActive(false);
        yield return new WaitForSeconds(0.01f);

    }

}
