using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScriptOld: MonoBehaviour
{
    public string[] Inventory = new string[4]; //contains the item IDs of the 4 inventory slots
    public int numberOfItems = 0; //tracks the number of items in inventory


    public GameObject peeledBanana;
    public SlotManager SlotManager;
    public PlayerFart PlayerFart;

    // Start is called before the first frame update
    void Start()
    {
        string[] Inventory = {"", "", "", ""};
        numberOfItems = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) //checks if the player has collided with a valid item and places it in their inventory if there is a slot
    {
        string Item = other.gameObject.tag;

        if (numberOfItems < 4)
        {
            switch (Item)
            {
                case "Banana":
                    Destroy(other.gameObject);
                    InventoryManager("Banana");
                    break;
                case "Fries":
                    Destroy(other.gameObject);
                    InventoryManager("Fries");
                    break;
                case "Onion":
                    Destroy(other.gameObject);
                    InventoryManager("Onion");
                    break;
                case "Egg":
                    Destroy(other.gameObject);
                    InventoryManager("Egg");
                    break;
                case "Kimchi":
                    Destroy(other.gameObject);
                    InventoryManager("Kimchi");
                    break;
            }
        }

    }

    void InventoryManager(string Item) //Places an item in the first available slot
    {
        for (int SlotNumber = 0; SlotNumber < 4; SlotNumber++)
        {
            if (Inventory[SlotNumber] == "")
            {
                Inventory.SetValue(Item, SlotNumber);
                numberOfItems++;
                SlotManager.handleItemIcon(SlotNumber);
                break;
            }
        }
    }

    public void ItemManager(int SlotNumber) //Handles Item effects when the corresponding inventory slot is tapped
    {
        string Item = Inventory[SlotNumber];

        switch (Item)
        {
            case "Banana":
                Inventory[SlotNumber] = "PeeledBanana";
                SlotManager.handleItemIcon(SlotNumber);
                if (gameObject.GetComponent<PlayerHealth>().health > 0)
                {
                    gameObject.GetComponent<PlayerHealth>().health = 4;
                }
                break;
            case "PeeledBanana":
                Inventory[SlotNumber] = "";
                numberOfItems--;
                SlotManager.handleItemIcon(SlotNumber);
                Instantiate(peeledBanana, transform.position, Quaternion.identity);
                break;
            case "Fries":
                Inventory[SlotNumber] = "";
                numberOfItems--;
                SlotManager.handleItemIcon(SlotNumber);
                if (gameObject.GetComponent<PlayerHealth>().health > 0 && gameObject.GetComponent<PlayerHealth>().health < 3)
                {
                    gameObject.GetComponent<PlayerHealth>().health += 1;
                }
                else
                {
                    gameObject.GetComponent<PlayerHealth>().health = 4;
                }
                break;
            default:
                break;
        }
    }
}
    
