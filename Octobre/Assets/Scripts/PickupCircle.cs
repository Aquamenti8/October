using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupCircle : MonoBehaviour {


    [SerializeField] private GameObject questionMark;

    private GameObject player;
    private GameObject actionCollider;

    public InventoryReminder theReminder;
    public GameObject Menu;
    public string activeMenu;

  
    //Detect objects
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            questionMark.SetActive(true);

            //Collect object
            if (Input.GetMouseButtonDown(0))
            {
                
                string ItemToCollectName = other.gameObject.GetComponent<ITEM>().collectable.name;
               Destroy(other.gameObject);
                

                theReminder.PickupCollectable(ItemToCollectName);
                this.GetComponent<MenuBehaviour>().RollMenu();
                this.GetComponent<MenuBehaviour>().UpdateSprite();
                
                Debug.Log("Collect!");
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
    }

    //GetInputs
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            theReminder.CleanInventory();
            this.GetComponent<MenuBehaviour>().RollMenu();
            this.GetComponent<MenuBehaviour>().UpdateSprite();
            
        }

        if (Input.GetMouseButtonDown(1)) //OPEN MENU
        {
            if (activeMenu == "") { activeMenu = "Main"; Menu.SetActive(true); }
            else { activeMenu = ""; Menu.SetActive(false); }
        }

        if (Input.GetMouseButtonDown(0) && activeMenu == "Main")
        {
            if( this.GetComponent<MenuBehaviour>().current == 0) // si c'est CALL
            {
                //OPEN CALL
                activeMenu = "Call";
            }
            if (this.GetComponent<MenuBehaviour>().current == 1) // si c'est INVENTORY
            {
                //OPEN INVENTORY
                activeMenu = "Inventory";
            }
            if (this.GetComponent<MenuBehaviour>().current == 2) // si c'est SLEEP
            {
                //SLEEP
            }
        }
        if (Input.GetMouseButtonDown(0) && activeMenu == "Inventory")
        {
            theReminder.UseCollectable(this.GetComponent<MenuBehaviour>().current);
            this.GetComponent<MenuBehaviour>().RollMenu();
            this.GetComponent<MenuBehaviour>().UpdateSprite();
            activeMenu = "";
            Menu.SetActive(false);
        }
    }


}
