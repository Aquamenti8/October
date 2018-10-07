using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryReminder : MonoBehaviour {

    //public static InventoryReminder Inv;
    public List<string> item;
    public List<int> quantity;

    public List<string> faces;

    public List<string> mainOptions;

    private static bool created = false;
    // Use this for initialization
    void Awake()
    {
        CleanInventory();
        foreach (string data in item) { Debug.Log(data); }
        foreach (int data in quantity) { Debug.Log(data); }
        Debug.Log("startClear!");
        
        mainOptions.Clear();
        
        mainOptions.Add("Call");
        mainOptions.Add("Inventory");
        mainOptions.Add("Sleep");

        faces.Add("Unknown");

        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    
}


    // Update is called once per frame
    void Update()
    {

    }

    public void PickupCollectable(string objName)
    {
        //si l'objet existe deja
        if (item.Contains(objName))
        {
            quantity[item.IndexOf(objName)] += 1;
        }
        else //si c'est le premier
        {
            item.Add(objName);
            quantity.Add(1);
        };
        foreach (string data in item ){Debug.Log(data);}
        foreach (int data in quantity) { Debug.Log(data); }
    }

    public GameObject Cherry;
    public GameObject Carrot;

    public void UseCollectable(int currentindex)
    {
        if (quantity[currentindex] > 0)
        {
            GameObject player = GameObject.Find("Player");
            GameObject collectablePref = null;
            switch (item[currentindex])
            {
                case "Carrot":
                    collectablePref = Carrot;
                    break;
                case "Cherry":
                    collectablePref = Cherry;
                    break;
            }
            //retire current item de l'inventaire
            if (quantity[currentindex] == 1)
            {
                item.RemoveAt(currentindex);
                quantity.RemoveAt(currentindex);
            }
            else if (quantity[currentindex] > 1)
            {
                quantity[currentindex] -= 1;
            }
            foreach (string data in item) { Debug.Log(data); }
            foreach (int data in quantity) { Debug.Log(data); }

            //fait top une instance de l'objet devant le joueur (celon son flip)
            
            
            GameObject pop = Instantiate(collectablePref, new Vector3(player.transform.position.x , player.transform.position.y,0) , Quaternion.identity);
            float normalImpulse = 50f;
            float runImpulse = 250f;
            float ImpulseSign = 0;
            if (player.GetComponent<SpriteRenderer>().flipX) ImpulseSign = -1;
            else ImpulseSign = 1;
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                pop.GetComponent<Rigidbody2D>().AddForce(new Vector2(normalImpulse * ImpulseSign, 20f));
            }
            else { pop.GetComponent<Rigidbody2D>().AddForce(new Vector2(runImpulse * ImpulseSign, 20f)); }
            

            //donne une impulsion au l'objet si le joueur presse droite ou gauche en meme temps
        }
    }

    public void CleanInventory()
    {
        item.RemoveRange(0, item.Count);
        quantity.RemoveRange(0, quantity.Count);
        Debug.Log("clear!");

    }
}
