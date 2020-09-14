using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootBoxSystem
{
    [CreateAssetMenu(fileName = "NewLootBox", menuName = "Project/CreateNewLootBox", order = 0)]
    public class SurpriseMechanics : ScriptableObject
    {
        [SerializeField] string lootBoxName;
        [SerializeField] int lootAmount;
        [SerializeField] int lootBoxCost;
        [SerializeField] LootBoxType lootBoxType;
        [SerializeField] LootBoxRarity[] containedRarities;

        public string LootBoxName { get => lootBoxName; }
        public int LootBoxCost { get => lootBoxCost; }
        public LootBoxType LootBoxType { get => lootBoxType; }
        public int LootAmount { get => lootAmount; }
        public LootBoxRarity[] ContainedRarities { get => containedRarities; }
    }

    [System.Serializable]
    public class LootBoxRarity
    {
        public Rarity rarity;
        [Range(0.0f, 1.0f)]
        public float chance = 1f;
    }
}