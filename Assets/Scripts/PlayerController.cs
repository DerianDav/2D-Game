using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Temp Key Holder
    KeyCode inventoryKey = KeyCode.I;
    public Image inventoryUI;
    public Image characterUI;
    public Sprite testSprite;
    private MoneyController playerWallet;
    public Text moneyTextUI;

    public float moveSpeed = 3.0f;

    public int maxHealth = 100;
        int currentHealth;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = 25;
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        inventoryStart();
        addItem(ItemDatabase.instance.getItem(3));

        playerWallet = new MoneyController();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(inventoryKey))
        {
            toggleInventoryUI();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        position.y = position.y + moveSpeed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        GameObject obj = Resources.Load("prefabs/Wooden Box") as GameObject;
        Instantiate(obj);
        addMoney(50);

        
    }


    // ----INVENTORY----
    public Image[] inventoryImages;

    int numberOfItems;
    List<Item> inventory = new List<Item>();

    void inventoryStart()
    {
        numberOfItems = 0;
        inventoryUI.enabled = false;
        for (int i = 0; i < 12; i++)
        {
            inventoryImages[i].enabled = false;
        }
    }

    public int addItem(Item item)
    {
        if (numberOfItems >= 12)
            return -1;
        numberOfItems++;
        inventory.Add(item);
        refreshInventoryImages();
        return 1;
    }

    public int takeItem(Item item)
    {
        if (inventory.Contains(item) == false)
            return -1;
        
        inventory.Remove(item);
        numberOfItems--;
        return 1;
    }

    //refreshes the inventoryImages with the new list
    void refreshInventoryImages()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            string path = "prefabs/" + inventory[i].prefabName;
            Debug.Log(path);
            Sprite newImg = ((Resources.Load(path)as GameObject).GetComponent<SpriteRenderer>()).sprite;
            inventoryImages[i].sprite = newImg;
        }
    }

    //toggles visibility of the Inventory screen
    void toggleInventoryUI()
    {
        if (inventoryUI.enabled)
        {
            inventoryUI.enabled = false;
            for (int i = 0; i < 12; i++)
            {
                inventoryImages[i].enabled = false;
            }
        }
        else
        {
            inventoryUI.enabled = true;
            for (int i = 0; i < numberOfItems; i++)
            {
                inventoryImages[i].enabled = true;
            }
        }
    }


    //------MONEY----

    public void addMoney(int amount) { playerWallet.addMoney(amount); refreshMoneyUI(); }

    public int subtractMoney(int amount)
    {
        int returnVal = playerWallet.subtractMoney(amount);
        refreshMoneyUI();
        return returnVal;
    }

    private void refreshMoneyUI()
    {
        moneyTextUI.text = "$" + playerWallet.currentMoney;
    }
}
