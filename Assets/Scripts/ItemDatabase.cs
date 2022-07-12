using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance { get; private set; }

    

    private Dictionary<int, Item> items = new Dictionary<int, Item>();

    private void Awake()
    {
        instance = this;

        string path = "Assets/Resources/items.txt";
        StreamReader reader = new StreamReader(path);
        reader.ReadLine();//skip first line

        while (reader.Peek() != -1)
        {
            string[] line = reader.ReadLine().Split(' ');

            int id = int.Parse(line[0]);
            string name = line[1].Replace('_', ' ');
            string prefabName = line[2].Replace('_', ' ');
            int sellPrice = int.Parse(line[3]);
            int buyPrice = int.Parse(line[4]);
            
            items.Add(id, new Item(id, name, prefabName, sellPrice, buyPrice));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public Item getItem(int id)
    {
        return items[id];
    }
}
