using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootBoxSystem
{
    public class LootManager : Singleton<LootManager>
    {
        LootRandomizer LR;
        Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();

        public SurpriseMechanics Common;
        public SurpriseMechanics Rare;
        public SurpriseMechanics Epic;
        public SurpriseMechanics Legendary;

        // Start is called before the first frame update
        void Awake()
        {
            LR = new LootRandomizer();
            LR.GetItemData();
        }

        public void PrintInventory()
        {
            foreach (var item in inventory)
            {
                Debug.Log("Item: " + item.Key.ItemName + ", amount: " + item.Value);
            }
        }

        public void OpenCommonBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Common);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        public void OpenRareBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Rare);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        public void OpenEpicBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Epic);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        public void OpenLegendaryBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Legendary);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        void AddItemToInventory(ItemData data, int amount)
        {
            if (!inventory.ContainsKey(data))
            {
                inventory.Add(data, amount);
            } else
            {
                inventory[data] += amount;
            }
        }
    }
}