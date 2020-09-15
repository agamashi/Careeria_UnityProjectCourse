using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootBoxSystem
{
    /// <summary>
    /// Scripti joka hallinnoi Itemeiden dataa. ItemData objekti saadaan lootboksista
    /// </summary>
    [CreateAssetMenu(fileName = "NewItem", menuName = "Project/NewItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] string itemName;
        [SerializeField] Rarity itemRarity;
        [SerializeField] LootType itemLootType;

        // Getterit ItemDatan muuttujille, jotta niitä voidaan vain hakea. (Näitä arvoja ei tarvitse ikinä muuttaa pelin aikana)
        public string ItemName { get => itemName; }
        public Rarity ItemRarity { get => itemRarity; }
        public LootType ItemLootType { get => itemLootType; }
    }
}