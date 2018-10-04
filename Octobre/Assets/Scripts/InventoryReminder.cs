using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryReminder : MonoBehaviour {

    public static InventoryReminder Inv;
    public List<string> item = new List<string>();
    public List<int> quantity = new List<int>();

    // Use this for initialization
    void Start()
    {
        CleanInventory();
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

    public void CleanInventory()
    {
        item.RemoveRange(0, item.Count);
        quantity.RemoveRange(0, quantity.Count);
        Debug.Log("clear!");

    }
}
