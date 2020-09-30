using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Drawing;
//using System;

public class SlotManager : MonoBehaviour
{
    public InventoryScript inventoryScript;

    public Image[] ItemHolder = new Image[4];

    public Sprite EmptySlot;

    public Object[] iconArr;

    Sprite[] itemIconsArr;

    // Start is called before the first frame update
    void Start()
    {
        itemIconsArr = Resources.LoadAll<Sprite>("ItemIcons");
        //iconArr = Resources.LoadAll("Item icons", typeof(Sprite));
        for (int i = 0; i < 4; i++)
        {
            handleItemIcon(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void regSlotClick0()
    {
        inventoryScript.ItemManager(0);
        
    }

    public void regSlotClick1()
    {
        inventoryScript.ItemManager(1);
    }

    public void regSlotClick2()
    {
        inventoryScript.ItemManager(2);
    }

    public void regSlotClick3()
    {
        inventoryScript.ItemManager(3);
    }

    public void handleItemIcon(int id)
    {
        string itemname = InventoryScript.inventory[id];
        if(InventoryScript.inventory[id] == "")
        {
            ItemHolder[id].sprite = EmptySlot;
            transform.GetChild(id).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
        }
        else
        {
            //When going to next level and item carry forward, if not sprite, just leave it as dark gray blank, if it isnt replaced by an existing sprite
            ItemHolder[id].sprite = EmptySlot;
            transform.GetChild(id).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.65f);
            for (int i = 0; i < itemIconsArr.Length; i++)
            {
                if (itemIconsArr[i].name == itemname)
                {
                    ItemHolder[id].sprite = itemIconsArr[i];
                    break;
                }
            }
        }
    }
}
