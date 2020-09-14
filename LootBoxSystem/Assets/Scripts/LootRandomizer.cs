using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootBoxSystem
{
    public class LootRandomizer
    {
        ItemDatabase CommonItemDatabase = new ItemDatabase(Rarity.Common);
        ItemDatabase RareItemDatabase = new ItemDatabase(Rarity.Rare);
        ItemDatabase EpicItemDatabase = new ItemDatabase(Rarity.Epic);
        ItemDatabase LegendaryItemDatabase = new ItemDatabase(Rarity.Legendary);

        public void GetItemData()
        {
            Object[] data = Resources.LoadAll("Items", typeof(ItemData));

            foreach (ItemData item in data)
            {
                switch (item.ItemRarity)
                {
                    case Rarity.Common:
                        CommonItemDatabase.items.Add(item);
                        break;
                    case Rarity.Rare:
                        RareItemDatabase.items.Add(item);
                        break;
                    case Rarity.Epic:
                        EpicItemDatabase.items.Add(item);
                        break;
                    case Rarity.Legendary:
                        LegendaryItemDatabase.items.Add(item);
                        break;
                }
            }
        }

        public List<ItemData> InitializeLootbox(SurpriseMechanics selectedLootBox)
        {
            return RandomizeLootBox(selectedLootBox);
        } 

        List<ItemData> RandomizeLootBox(SurpriseMechanics selectedLootBox)
        {
            List<ItemData> AcquiredLoot = new List<ItemData>();

            for (int i = 0; i < selectedLootBox.LootAmount; i++)
            {
                AcquiredLoot.Add(CalculateItemDrop(selectedLootBox));
            }

            if (AcquiredLoot.Count != selectedLootBox.LootAmount)
            {
                Debug.LogError("Lootbox loot amount was not the right amount");
                return null;
            }

            return AcquiredLoot;
        }


        ItemData CalculateItemDrop(SurpriseMechanics selectedLootBox)
        {
            Rarity selectedRarity = Rarity.Common;

            float rand = Random.value;

            for (int i = selectedLootBox.ContainedRarities.Length - 1; i >= 0; i--)
            {
                if (selectedLootBox.ContainedRarities[i].chance >= rand)
                {
                    selectedRarity = selectedLootBox.ContainedRarities[i].rarity;
                    break;
                }
            }

            ItemData acquiredItem = null;

            switch (selectedRarity)
            {
                case Rarity.Common:
                    acquiredItem = GetRandomItem(CommonItemDatabase);
                    break;
                case Rarity.Rare:
                    acquiredItem = GetRandomItem(RareItemDatabase);
                    break;
                case Rarity.Epic:
                    acquiredItem = GetRandomItem(EpicItemDatabase);
                    break;
                case Rarity.Legendary:
                    acquiredItem = GetRandomItem(LegendaryItemDatabase);
                    break;
            }

            Debug.Log("Acquired: " + acquiredItem.ItemName + " of rarity " + acquiredItem.ItemRarity + ", item type " + acquiredItem.ItemLootType);

            if (acquiredItem != null)
                return acquiredItem;

            return null;
        }

        ItemData GetRandomItem(ItemDatabase database)
        {
            int randomNum = Random.Range(0, database.items.Count);

            return database.items[randomNum];
        }
    }

    public class ItemDatabase
    {
        public Rarity rarity;
        public List<ItemData> items = new List<ItemData>();

        public ItemDatabase(Rarity _rarity)
        {
            rarity = _rarity;
        }
    }
}