using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour {

    //recupere les infos du reminder
    //classe en "current", "-1", "+1", -2 et +2 les index du reminder

    //possede main mise sur les sprite des slot et des slots item
    //en fonction de "item" dans l'index, change le sprite, 
    //en fonction de "quantity" dans l'index change le texte


    public GameObject reminder;
    public InventoryReminder Inv;

    public GameObject Slot1;
    public GameObject Slot1Item;
    public Text Slot1Quantity;

    public GameObject Slot2;
    public GameObject Slot2Item;
    public Text Slot2Quantity;

    public GameObject Slot3;
    public GameObject Slot3Item;
    public Text Slot3Quantity;

    public GameObject Slot4;
    public GameObject Slot4Item;
    public Text Slot4Quantity;

    public GameObject Slot5;
    public GameObject Slot5Item;
    public Text Slot5Quantity;


    public int current;
    private int p1;
    private int p2;
    private int m1;
    private int m2;

    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    public GameObject pos5;
    public GameObject posrem;

    float pos1X;
    float pos1Y;

    float pos2X;
    float pos2Y;

    float pos3X;
    float pos3Y;

    float pos4X;
    float pos4Y;

    float pos5X;
    float pos5Y;

    public Sprite SelectedSp;
    public Sprite AuxSp;

    public Sprite SpCarrot;
    public Sprite SpCherry;

    Sprite sp1;
    Sprite sp2;
    Sprite sp3;
    Sprite sp4;
    Sprite sp5;

    public Sprite Mask0;
    public Sprite Mask1;
    public Sprite Mask2;

    public Sprite Call;
    public Sprite Inventory;
    public Sprite Sleep;

    //private string activeMenu;

    // Use this for initialization
    void Start () {
        reminder = GameObject.Find("TheReminder");
        Inv = reminder.GetComponent<InventoryReminder>();
        current = 0;
        p1 = 0;
        p2 = 0;
        m1 = 0;
        m2 = 0;

        pos1 = Slot1;
        pos2 = Slot2;
        pos3 = Slot3;
        pos4 = Slot4;
        pos5 = Slot5;

        pos1X = 0f;
        pos1Y = 0.23f;

        pos2X = 0.15f;
        pos2Y = 0.15f;

        pos3X = 0.1f;
        pos3Y = -0.01f;

        pos4X = -0.1f;
        pos4Y = -0.01f;

        pos5X = -0.15f;
        pos5Y = 0.15f;

        sp1 = Slot1.GetComponent<SpriteRenderer>().sprite;
        sp2 = Slot2.GetComponent<SpriteRenderer>().sprite;
        sp3 = Slot3.GetComponent<SpriteRenderer>().sprite;
        sp4 = Slot4.GetComponent<SpriteRenderer>().sprite;
        sp5 = Slot5.GetComponent<SpriteRenderer>().sprite;
        RollMenu();
        UpdateSprite();

    }
	
    public int PlusOne(int index)
    {
        int activeList = 0;
        if (this.GetComponent<PickupCircle>().activeMenu == "Inventory") activeList = Inv.item.Count;
        else if (this.GetComponent<PickupCircle>().activeMenu == "Call") activeList = Inv.faces.Count;
        else if (this.GetComponent<PickupCircle>().activeMenu == "Main") activeList = Inv.mainOptions.Count;
        else activeList = Inv.mainOptions.Count;

        int newIndex = index+1;
        
        if (newIndex >= activeList)
        {
            newIndex = 0;
        }

        return newIndex;
    }
    public int MinusOne(int index)
    {
        List<string> activeList = null;
        if (this.GetComponent<PickupCircle>().activeMenu == "Inventory") activeList = Inv.item;
        else if (this.GetComponent<PickupCircle>().activeMenu == "Call") activeList = Inv.faces;
        else if (this.GetComponent<PickupCircle>().activeMenu == "Main") activeList = Inv.mainOptions;
        else activeList = Inv.mainOptions;

        int newIndex = index - 1;
        if (newIndex <0)
        {
            if (activeList.Count == 0)
            {
                newIndex = 0;
            }
            else
            {
                newIndex = activeList.Count - 1;
            }
        }
        return newIndex;
    }

    public void RollMenu()
    {
        //CHANGE INDEX
        float Scroll = Input.GetAxis("Mouse ScrollWheel");
        int scrollTimes = Mathf.Abs(Mathf.RoundToInt(Scroll * 10));

        for (int i = 0; i < scrollTimes; i++)
        {

            if (Mathf.Sign(Scroll) == 1)
            {
                ChangeCurrent(PlusOne(current));

                posrem = pos1;
                pos1 = pos2;
                pos2 = pos3;
                pos3 = pos4;
                pos4 = pos5;
                pos5 = posrem;
                posrem = null;

            }
            else
            {
                ChangeCurrent( MinusOne(current));

                posrem = pos5;
                pos5 = pos4;
                pos4 = pos3;
                pos3 = pos2;
                pos2 = pos1;
                pos1 = posrem;
                posrem = null;
                

            }
           

           
            UpdateSprite();

        }
        ChangeCurrent(current);


    }

    public void ChangeCurrent(int newCurrent)
    {
        current = newCurrent;
        p1 = PlusOne(current);
        p2 = PlusOne(PlusOne(current));
        m1 = MinusOne(current);
        m2 = MinusOne(MinusOne(current));
    }

    public void UpdateSprite()
    {
        if (this.GetComponent<PickupCircle>().activeMenu == "Inventory")
        {
            //CHANGE SPRITE
            //si c'est un scroll positif, les slots changent de position en positif, pos1 devient pos2 ect
            if (pos1 == Slot1) { sp1 = SelectedSp; } else { sp1 = AuxSp; }
            if (pos1 == Slot2) sp2 = SelectedSp; else sp2 = AuxSp;
            if (pos1 == Slot3) sp3 = SelectedSp; else sp3 = AuxSp;
            if (pos1 == Slot4) sp4 = SelectedSp; else sp4 = AuxSp;
            if (pos1 == Slot5) sp5 = SelectedSp; else sp5 = AuxSp;

            Slot1.GetComponent<SpriteRenderer>().sprite = sp1;
            Slot2.GetComponent<SpriteRenderer>().sprite = sp2;
            Slot3.GetComponent<SpriteRenderer>().sprite = sp3;
            Slot4.GetComponent<SpriteRenderer>().sprite = sp4;
            Slot5.GetComponent<SpriteRenderer>().sprite = sp5;

            //Switch, si le pos1 est egal a slot1/2/3/4/5;
            //slot1/2/3Item prend lobject switch correspondant a


            if (Inv.item.Count == 0)
            {
                Slot1Item.GetComponent<SpriteRenderer>().sprite = null;
                Slot2Item.GetComponent<SpriteRenderer>().sprite = null;
                Slot3Item.GetComponent<SpriteRenderer>().sprite = null;
                Slot4Item.GetComponent<SpriteRenderer>().sprite = null;
                Slot5Item.GetComponent<SpriteRenderer>().sprite = null;
                Slot1Quantity.text = "0";
                Slot2Quantity.text = "0";
                Slot3Quantity.text = "0";
                Slot4Quantity.text = "0";
                Slot5Quantity.text = "0";
            }
            else
            {
                GameObject SlotTemp = null;
                Text SlotQTemp = null;
                if (pos1 == Slot1) { SlotTemp = Slot1Item; SlotQTemp = Slot1Quantity; }
                else if (pos1 == Slot2) { SlotTemp = Slot2Item; SlotQTemp = Slot2Quantity; }
                else if (pos1 == Slot3) { SlotTemp = Slot3Item; SlotQTemp = Slot3Quantity; }
                else if (pos1 == Slot4) { SlotTemp = Slot4Item; SlotQTemp = Slot4Quantity; }
                else if (pos1 == Slot5) { SlotTemp = Slot5Item; SlotQTemp = Slot5Quantity; }
                SlotItemSprite(SlotTemp, current);
                if (Inv.item.Count == current) SlotQTemp.text = "0";
                else SlotQTemp.text = (Inv.quantity[current].ToString());

                if (pos2 == Slot1) { SlotTemp = Slot1Item; SlotQTemp = Slot1Quantity; }
                else if (pos2 == Slot2) { SlotTemp = Slot2Item; SlotQTemp = Slot2Quantity; }
                else if (pos2 == Slot3) { SlotTemp = Slot3Item; SlotQTemp = Slot3Quantity; }
                else if (pos2 == Slot4) { SlotTemp = Slot4Item; SlotQTemp = Slot4Quantity; }
                else if (pos2 == Slot5) { SlotTemp = Slot5Item; SlotQTemp = Slot5Quantity; }
                SlotItemSprite(SlotTemp, p1);
                if (Inv.item.Count == p1) SlotQTemp.text = "0";
                else SlotQTemp.text = (Inv.quantity[p1].ToString());

                if (pos3 == Slot1) { SlotTemp = Slot1Item; SlotQTemp = Slot1Quantity; }
                else if (pos3 == Slot2) { SlotTemp = Slot2Item; SlotQTemp = Slot2Quantity; }
                else if (pos3 == Slot3) { SlotTemp = Slot3Item; SlotQTemp = Slot3Quantity; }
                else if (pos3 == Slot4) { SlotTemp = Slot4Item; SlotQTemp = Slot4Quantity; }
                else if (pos3 == Slot5) { SlotTemp = Slot5Item; SlotQTemp = Slot5Quantity; }
                SlotItemSprite(SlotTemp, p2);
                if (Inv.item.Count == p2) SlotQTemp.text = "0";
                else SlotQTemp.text = (Inv.quantity[p2].ToString());

                if (pos4 == Slot1) { SlotTemp = Slot1Item; SlotQTemp = Slot1Quantity; }
                else if (pos4 == Slot2) { SlotTemp = Slot2Item; SlotQTemp = Slot2Quantity; }
                else if (pos4 == Slot3) { SlotTemp = Slot3Item; SlotQTemp = Slot3Quantity; }
                else if (pos4 == Slot4) { SlotTemp = Slot4Item; SlotQTemp = Slot4Quantity; }
                else if (pos4 == Slot5) { SlotTemp = Slot5Item; SlotQTemp = Slot5Quantity; }
                SlotItemSprite(SlotTemp, m2);
                if (Inv.item.Count == m2) SlotQTemp.text = "0";
                else SlotQTemp.text = (Inv.quantity[m2].ToString());

                if (pos5 == Slot1) { SlotTemp = Slot1Item; SlotQTemp = Slot1Quantity; }
                else if (pos5 == Slot2) { SlotTemp = Slot2Item; SlotQTemp = Slot2Quantity; }
                else if (pos5 == Slot3) { SlotTemp = Slot3Item; SlotQTemp = Slot3Quantity; }
                else if (pos5 == Slot4) { SlotTemp = Slot4Item; SlotQTemp = Slot4Quantity; }
                else if (pos5 == Slot5) { SlotTemp = Slot5Item; SlotQTemp = Slot5Quantity; }
                SlotItemSprite(SlotTemp, m1);
                if (Inv.item.Count == m1) SlotQTemp.text = "0";
                else SlotQTemp.text = (Inv.quantity[m1].ToString());
            }
        }
        else if (this.GetComponent<PickupCircle>().activeMenu == "Main")
        {
            if (Slot1 == pos1) SlotMainSprite(Slot1, current);
            else if (Slot1 == pos2) SlotMainSprite(Slot1, p1);
            else if (Slot1 == pos3) SlotMainSprite(Slot1, p2);
            else if (Slot1 == pos4) SlotMainSprite(Slot1, m2);
            else if (Slot1 == pos5) SlotMainSprite(Slot1, m1);

            if (Slot2 == pos1) SlotMainSprite(Slot2, current);
            else if (Slot2 == pos2) SlotMainSprite(Slot2, p1);
            else if (Slot2 == pos3) SlotMainSprite(Slot2, p2);
            else if (Slot2 == pos4) SlotMainSprite(Slot2, m2);
            else if (Slot2 == pos5) SlotMainSprite(Slot2, m1);

            if (Slot3 == pos1) SlotMainSprite(Slot3, current);
            else if (Slot3 == pos2) SlotMainSprite(Slot3, p1);
            else if (Slot3 == pos3) SlotMainSprite(Slot3, p2);
            else if (Slot3 == pos4) SlotMainSprite(Slot3, m2);
            else if (Slot3 == pos5) SlotMainSprite(Slot3, m1);

            if (Slot4 == pos1) SlotMainSprite(Slot4, current);
            else if (Slot4 == pos2) SlotMainSprite(Slot4, p1);
            else if (Slot4 == pos3) SlotMainSprite(Slot4, p2);
            else if (Slot4 == pos4) SlotMainSprite(Slot4, m2);
            else if (Slot4 == pos5) SlotMainSprite(Slot4, m1);

            if (Slot5 == pos1) SlotMainSprite(Slot5, current);
            else if (Slot5 == pos2) SlotMainSprite(Slot5, p1);
            else if (Slot5 == pos3) SlotMainSprite(Slot5, p2);
            else if (Slot5 == pos4) SlotMainSprite(Slot5, m2);
            else if (Slot5 == pos5) SlotMainSprite(Slot5, m1);
        }
        else if (this.GetComponent<PickupCircle>().activeMenu == "Call")
        {
            if(Slot1 == pos1) SlotFaceSprite(Slot1, current);
            else if (Slot1 == pos2) SlotFaceSprite(Slot1, p1);
            else if (Slot1 == pos3) SlotFaceSprite(Slot1, p2);
            else if (Slot1 == pos4) SlotFaceSprite(Slot1, m2);
            else if (Slot1 == pos5) SlotFaceSprite(Slot1, m1);

            if (Slot2 == pos1) SlotFaceSprite(Slot2, current);
            else if (Slot2 == pos2) SlotFaceSprite(Slot2, p1);
            else if (Slot2 == pos3) SlotFaceSprite(Slot2, p2);
            else if (Slot2 == pos4) SlotFaceSprite(Slot2, m2);
            else if (Slot2 == pos5) SlotFaceSprite(Slot2, m1);

            if (Slot3 == pos1) SlotFaceSprite(Slot3, current);
            else if (Slot3 == pos2) SlotFaceSprite(Slot3, p1);
            else if (Slot3 == pos3) SlotFaceSprite(Slot3, p2);
            else if (Slot3 == pos4) SlotFaceSprite(Slot3, m2);
            else if (Slot3 == pos5) SlotFaceSprite(Slot3, m1);

            if (Slot4 == pos1) SlotFaceSprite(Slot4, current);
            else if (Slot4 == pos2) SlotFaceSprite(Slot4, p1);
            else if (Slot4 == pos3) SlotFaceSprite(Slot4, p2);
            else if (Slot4 == pos4) SlotFaceSprite(Slot4, m2);
            else if (Slot4 == pos5) SlotFaceSprite(Slot4, m1);

            if (Slot5 == pos1) SlotFaceSprite(Slot5, current);
            else if (Slot5 == pos2) SlotFaceSprite(Slot5, p1);
            else if (Slot5 == pos3) SlotFaceSprite(Slot5, p2);
            else if (Slot5 == pos4) SlotFaceSprite(Slot5, m2);
            else if (Slot5 == pos5) SlotFaceSprite(Slot5, m1);
        }
    }

    void SlotItemSprite(GameObject SlotTemp, int pos)
    {
        if (Inv.item.Count == pos)
        {
            SlotTemp.GetComponent<SpriteRenderer>().sprite = null;
        }
        else switch (Inv.item[pos])
        {
            case "Carrot":
                SlotTemp.GetComponent<SpriteRenderer>().sprite = SpCarrot;
                break;
            case "Cherry":
                SlotTemp.GetComponent<SpriteRenderer>().sprite = SpCherry;
                break;
        }
    }

    void SlotFaceSprite(GameObject SlotTemp, int pos)
    {
        switch (Inv.faces[pos])
            {
                case "Unknown":
                    SlotTemp.GetComponent<SpriteRenderer>().sprite = Mask0;
                    break;
                case "Kolimo":
                    SlotTemp.GetComponent<SpriteRenderer>().sprite = Mask1;
                    break;
                case "Bernie":
                SlotTemp.GetComponent<SpriteRenderer>().sprite = Mask2;
                    break;
        }
    }
    void SlotMainSprite(GameObject SlotTemp, int pos)
    {
        switch (Inv.mainOptions[pos])
        {
            case "Call":
                SlotTemp.GetComponent<SpriteRenderer>().sprite = Call;
                break;
            case "Inventory":
                SlotTemp.GetComponent<SpriteRenderer>().sprite = Inventory;
                break;
            case "Sleep":
                SlotTemp.GetComponent<SpriteRenderer>().sprite = Sleep;
                break;
        }
    }

    // Update is called once per frame
    void Update () {

        //Inv = reminder.GetComponent<InventoryReminder>();

        if (this.GetComponent<PickupCircle>().activeMenu != "")
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                RollMenu();
            }
            pos1.transform.localPosition = Vector2.MoveTowards(pos1.transform.localPosition, new Vector2(pos1X, pos1Y), Time.deltaTime);
            pos2.transform.localPosition = Vector2.MoveTowards(pos2.transform.localPosition, new Vector2(pos2X, pos2Y), Time.deltaTime);
            pos3.transform.localPosition = Vector2.MoveTowards(pos3.transform.localPosition, new Vector2(pos3X, pos3Y), Time.deltaTime);
            pos4.transform.localPosition = Vector2.MoveTowards(pos4.transform.localPosition, new Vector2(pos4X, pos4Y), Time.deltaTime);
            pos5.transform.localPosition = Vector2.MoveTowards(pos5.transform.localPosition, new Vector2(pos5X, pos5Y), Time.deltaTime);


            //augmente/baisse l'opa 
            float opaSpeed = 6f;
            if ((Slot1.GetComponent<SpriteRenderer>().color.a > 0) && (Slot1 == pos3 || Slot1 == pos4))
            {
                float Alphaa = Slot1.GetComponent<SpriteRenderer>().color.a;
                Alphaa -= Time.deltaTime * opaSpeed;
                Slot1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot1Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot1Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }
            else if ((Slot1.GetComponent<SpriteRenderer>().color.a < 1) && (Slot1 != pos3 && Slot1 != pos4))
            {
                float Alphaa = Slot1.GetComponent<SpriteRenderer>().color.a;
                Alphaa += Time.deltaTime * opaSpeed;
                Slot1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot1Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot1Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }

            if ((Slot2.GetComponent<SpriteRenderer>().color.a > 0) && (Slot2 == pos3 || Slot2 == pos4))
            {
                float Alphaa = Slot2.GetComponent<SpriteRenderer>().color.a;
                Alphaa -= Time.deltaTime * opaSpeed;
                Slot2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot2Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot2Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }
            else if ((Slot2.GetComponent<SpriteRenderer>().color.a < 1) && (Slot2 != pos3 && Slot2 != pos4))
            {
                float Alphaa = Slot2.GetComponent<SpriteRenderer>().color.a;
                Alphaa += Time.deltaTime * opaSpeed;
                Slot2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot2Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot2Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }

            if ((Slot3.GetComponent<SpriteRenderer>().color.a > 0) && (Slot3 == pos3 || Slot3 == pos4))
            {
                float Alphaa = Slot3.GetComponent<SpriteRenderer>().color.a;
                Alphaa -= Time.deltaTime * opaSpeed;
                Slot3.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot3Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot3Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }
            else if ((Slot3.GetComponent<SpriteRenderer>().color.a < 1) && (Slot3 != pos3 && Slot3 != pos4))
            {
                float Alphaa = Slot3.GetComponent<SpriteRenderer>().color.a;
                Alphaa += Time.deltaTime * opaSpeed;
                Slot3.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot3Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot3Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }

            if ((Slot4.GetComponent<SpriteRenderer>().color.a > 0) && (Slot4 == pos3 || Slot4 == pos4))
            {
                float Alphaa = Slot4.GetComponent<SpriteRenderer>().color.a;
                Alphaa -= Time.deltaTime * opaSpeed;
                Slot4.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot4Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot4Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }
            else if ((Slot4.GetComponent<SpriteRenderer>().color.a < 1) && (Slot4 != pos3 && Slot4 != pos4))
            {
                float Alphaa = Slot4.GetComponent<SpriteRenderer>().color.a;
                Alphaa += Time.deltaTime * opaSpeed;
                Slot4.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot4Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot4Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }

            if ((Slot5.GetComponent<SpriteRenderer>().color.a > 0) && (Slot5 == pos3 || Slot5 == pos4))
            {
                float Alphaa = Slot5.GetComponent<SpriteRenderer>().color.a;
                Alphaa -= Time.deltaTime * opaSpeed;
                Slot5.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot5Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot5Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }
            else if ((Slot5.GetComponent<SpriteRenderer>().color.a < 1) && (Slot5 != pos3 && Slot5 != pos4))
            {
                float Alphaa = Slot5.GetComponent<SpriteRenderer>().color.a;
                Alphaa += Time.deltaTime * opaSpeed;
                Slot5.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot5Item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alphaa);
                Slot5Quantity.GetComponent<Text>().color = new Color(1, 0, 0, Alphaa);
            }
        }

        if (this.GetComponent<PickupCircle>().activeMenu != "Inventory")
        {
            Slot1Quantity.text = "";
            Slot2Quantity.text = "";
            Slot3Quantity.text = "";
            Slot4Quantity.text = "";
            Slot5Quantity.text = "";

            Slot1Item.SetActive(false);
            Slot2Item.SetActive(false);
            Slot3Item.SetActive(false);
            Slot4Item.SetActive(false);
            Slot5Item.SetActive(false);
        }
        else
        {
            Slot1Item.SetActive(true);
            Slot2Item.SetActive(true);
            Slot3Item.SetActive(true);
            Slot4Item.SetActive(true);
            Slot5Item.SetActive(true);
        }
    }
}
