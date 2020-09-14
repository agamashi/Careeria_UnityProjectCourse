using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootBoxSystem
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Project/NewItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] string itemName;
        [SerializeField] Rarity itemRarity;
        [SerializeField] LootType itemLootType;

        public string ItemName { get => itemName; }
        public Rarity ItemRarity { get => itemRarity; }
        public LootType ItemLootType { get => itemLootType; }
    }
}