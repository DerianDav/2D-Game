using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Item
{
    public int ID { get; }
    public string name { get; }
    public string prefabName { get; }
    public int Quantity { get; }
    public int sellPrice { get; }
    public int buyPrice { get; }

    public Item(int id, string name, string prefabName, int sellPrice, int buyPrice)
    {
        this.ID = id;
        this.name = name;
        this.prefabName = prefabName;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;

        this.Quantity = 0;
    }
}