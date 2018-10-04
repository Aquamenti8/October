using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupCircle : MonoBehaviour {


    [SerializeField] private GameObject questionMark;

    private GameObject player;
    private GameObject actionCollider;

    public InventoryReminder theReminder;
    public GameObject menu;


  
    //Detect objects
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            questionMark.SetActive(true);

            //Collect object
            if (Input.GetMouseButtonDown(0))
            {
               Destroy(other.gameObject);
                Debug.Log("Collect!");

                theReminder.PickupCollectable(other.gameObject.name);
                this.GetComponent<MenuBehaviour>().RollMenu();
                this.GetComponent<MenuBehaviour>().UpdateSprite();
            }
        }
        else
        {
            questionMark.SetActive(false);
        }
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

        if (Input.GetMouseButtonDown(1)) //Open menu
        {
            if (menu.activeSelf) 
            {
                menu.SetActive(false);
            }
            else {
                menu.SetActive(true);
            }
        }

    }
}
