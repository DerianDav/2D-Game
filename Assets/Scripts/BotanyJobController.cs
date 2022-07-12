using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanyJobController : MonoBehaviour
{
    //plant # affects Growth Time of the plant
    public enum Plant
    {
        None = -1,
        Tomato = 10,
        Onion = 150,
        Potato = 50,
        Lettuce = 75,
        Pepper = 90
    }

    public GameObject botanyGrower1;

    public class BotanyGrower
    {
        public Plant plantType { get; private set; }
        public int waterLevel { get; set; }
        public int GrowthTime { get; set; }
        public int grownPlants { get; private set;}
        public BotanyGrower()
        {
            plantType = Plant.None;
            waterLevel = 0;
            GrowthTime = 0;
        }

        /*Return:
            0 = Nothing Wrong/Normal Return
            1 = Trying to plant "None"
            2 = New plant is the same as the one thats already planted
        */
        public int PlantNewPlant(Plant newPlantType)
        {
            if (plantType == Plant.None)
                return 1;
            if (plantType == newPlantType)
                return 2;
            plantType = newPlantType;
            GrowthTime = 0;
            return 0;
        }

        public void GrowPlant()
        {
            if (plantType == Plant.None)
                return;
            GrowthTime++;
            if ((int)plantType / GrowthTime == 1 && (int)plantType % GrowthTime == 0)
            {
                Debug.Log("Tomato ready");
                GrowthTime = 0;
            }
        }

        public int pickPlant(int amount)
        {
            int grownPlantsRes;
            if (grownPlants > amount)
            {
                grownPlantsRes = grownPlants;
                grownPlants = 0;
                return grownPlantsRes;
            }

            grownPlantsRes = amount;
            grownPlants = grownPlants - amount;
            return grownPlantsRes;
        }
    }

    BotanyGrower[] growers;
    int idealWaterLevel = 50;

    // Start is called before the first frame update
    void Start()
    {
        growers = new BotanyGrower[10];
        for (int i = 0; i < 10; i++) { growers[i] = new BotanyGrower();}
        growers[0].PlantNewPlant(Plant.Tomato);
    }

    // Update is called once per frame
    int currentTick = 0;
    void FixedUpdate()
    {
        currentTick++;
        if (currentTick % 25 != 0)
            return;

        Debug.Log(currentTick);
        updateGrowerWater(51);
    }

    private void updateGrowerWater(int amount)
    {
        for (int i = 0; i < 10; i++)
        {
            growers[i].waterLevel = Mathf.Clamp(growers[i].waterLevel += amount, 0, 100);
            if (growers[i].waterLevel > idealWaterLevel)
                growers[i].GrowPlant();
        }
    }

}
