using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController
{
    public int currentMoney{ get; private set; }

    public MoneyController() { currentMoney = 100; }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void addMoney(int amount) { Debug.Log(currentMoney + " amount added" + amount); currentMoney += amount; }

    public int subtractMoney(int amount)
    {
        if (amount > currentMoney)
            return -1;
        currentMoney -= amount;
        return 0;
    }

}
