using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static string[] inventory = new string[4] { "", "", "", "" }; //contains the item IDs of the 4 inventory slots
    public int numberOfItems = 0; //tracks the number of items in inventory
    public bool gameOver;

    public GameObject peeledBanana; //Reference to the banana peel object
    public SlotManager SlotManager; //Reference to the inventory slots script
    public PlayerFart PlayerFart; //Reference to the script that controls the player's fart
    public PlayerHealth PlayerHealth; //Reference to the PlayerHealth script
    public BoyHealth BoyHealth; //Reference to the BoyHealth script
    public SceneControl SceneControl; //Reference to the scene control script
    public SegmentBarmanager SegmentBarmanager; //Reference to the script that translates the player's health into a segmented bar
    public PlayerMovement PlayerMovement;
   
    void Start()
    {
        BoyHealth = GetComponent<BoyHealth>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerMovement = GetComponent<PlayerMovement>();
        numberOfItems = 0;
        gameOver = false;
    }

    private void OnTriggerEnter(Collider other) //checks if the player has collided with a valid item and places it in their inventory if there is a slot
    {
        string Item = other.gameObject.tag;

        if (numberOfItems < 4)
        {
            switch (Item)
            {
                case "Durian":
                case "Cookie":
                case "WatermelonSlice":
                case "Banana":
                case "Fries":
                case "Egg":
                case "Kimchi":
                case "Onion":
                case "BakedBeans":
                case "EnergyDrink":
                case "Plastic Bag":
                    InventoryManager(Item);
                    Destroy(other.gameObject);
                    break;
                default:
                    break;
            }

        }
        if (Item == "Teddy Bear")
        {
            SceneControl.BearPickup();
            Destroy(other.gameObject);
        }

    }

    void InventoryManager(string Item) //Places an item in the first available slot
    {
        for (int SlotNumber = 0; SlotNumber < 4; SlotNumber++)
        {
            if (inventory[SlotNumber] == "")
            {
                inventory.SetValue(Item, SlotNumber);
                numberOfItems++;
                SlotManager.handleItemIcon(SlotNumber);
                break;
            }
        }
    }

    public void ItemManager(int SlotNumber) //Handles Item effects when the corresponding inventory slot is tapped
    {
        string Item = inventory[SlotNumber];

        if (!gameOver)
        {
            switch (Item)
            {
                case "Durian":
                    inventory[SlotNumber] = "";
                    numberOfItems--;
                    PlayerFart.BeginDurationEffect(5f);
                    break;

                case "Cookie":
                    inventory[SlotNumber] = "";
                    numberOfItems--;
                    PlayerMovement.AddSpeedMultiplier(1.5f, 2f);
                    break;

                case "WatermelonSlice":
                    inventory[SlotNumber] = "";
                    numberOfItems--;
                    PlayerMovement.AddSpeedMultiplier(1.25f, 4f);
                    break;
                case "Banana"://Heals the player fully
                    inventory[SlotNumber] = "PeeledBanana";
                    if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
                    {
                        SegmentBarmanager.UpdateHealth(BoyHealth.maxHealth - BoyHealth.currentHealth);
                        BoyHealth.currentHealth = BoyHealth.maxHealth;
                    }
                    if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
                    {
                        SegmentBarmanager.UpdateHealth(PlayerHealth.maxHealth - PlayerHealth.health);
                        PlayerHealth.health = PlayerHealth.maxHealth;
                    }
                    break;
                case "PeeledBanana"://Drops a banana peel below the player
                    inventory[SlotNumber] = "";
                    numberOfItems--;
                    Instantiate(peeledBanana, transform.position, Quaternion.Euler(0f, 180f, 0f));
                    break;
                case "Fries"://Player heals one health, changes fart type for 5 seconds
                    if (PlayerFart.effectOn == false)
                    {
                        inventory[SlotNumber] = "";
                        numberOfItems--;
                        if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
                        {
                            if (PlayerHealth.health < PlayerHealth.maxHealth)
                            {
                                SegmentBarmanager.UpdateHealth(1);
                                PlayerHealth.health += 1;
                            }
                        }
                        if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
                        {
                            if (BoyHealth.currentHealth < BoyHealth.maxHealth)
                            {
                                SegmentBarmanager.UpdateHealth(1);
                                BoyHealth.currentHealth += 1;
                            }
                        }
                            PlayerFart.FartChange(1, 5f);
                    }
                    break;
                case "Egg"://Changes fart type for 5 seconds
                    if (PlayerFart.effectOn == false)
                    {
                        inventory[SlotNumber] = "";
                        numberOfItems--;
                        PlayerFart.FartChange(3, 5f);
                    }
                    break;
                case "Kimchi": //Player takes one damage, changes fart type for 5 seconds
                    if (PlayerFart.effectOn == false)
                    {
                        inventory[SlotNumber] = "";
                        numberOfItems--;
                        SegmentBarmanager.UpdateHealth(-1);
                        PlayerHealth.health -= 1;
                        PlayerFart.FartChange(4, 5f);
                    }
                    break;
                case "Onion"://Changes fart type for 5 seconds
                    if (PlayerFart.effectOn == false)
                    {
                        inventory[SlotNumber] = "";
                        numberOfItems--;
                        PlayerFart.FartChange(2, 5f);
                    }
                    break;
                case "EnergyDrink": //Player moves faster by 2 times and for 2 seconds
                    inventory[SlotNumber] = "";
                    numberOfItems--;
                    PlayerMovement.AddSpeedMultiplier(2f, 2f);
                    break;
                case "BakedBeans": //Player's fart type gets changed to 5 for 4 seconds
                    if (PlayerFart.effectOn == false)
                    {
                        inventory[SlotNumber] = "";
                        numberOfItems--;
                        PlayerFart.FartChange(5, 4f);
                    }
                    break;
                case "Plastic Bag": //Player stores the next fart in a plastic bag that is then placed on the ground
                    inventory[SlotNumber] = "";
                    numberOfItems--;
                    PlayerFart.bagActive = true;
                    break;
                default:
                    break;
            }

            SlotManager.handleItemIcon(SlotNumber);
        }
    }
}
    
