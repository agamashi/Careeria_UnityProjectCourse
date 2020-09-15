using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LootBoxSystem
{
    /// <summary>
    /// Manager joka hallinnoi lootin ja itemeiden hakemisen ja inventory systeemiin lisäämisen.
    /// </summary>
    public class LootManager : Singleton<LootManager>
    {
        [SerializeField] TextMeshProUGUI openedBoxesUI;

        LootRandomizer LR;
        Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();
        int openedBoxes = 0;

        // Includes all the items in the Resources/Items folder
        List<ItemData> AllItems = new List<ItemData>();

        public SurpriseMechanics Common;
        public SurpriseMechanics Rare;
        public SurpriseMechanics Epic;
        public SurpriseMechanics Legendary;

        public int OpenedBoxes
        {
            get { return openedBoxes; }

            set
            {
                // Muutetaan openedBoxes arvo uuteen jonka jälkeen päivitetään määrä textmeshpro objektiin pelissä
                openedBoxes = value;
                openedBoxesUI.text = openedBoxes.ToString();
            }
        }

        public Dictionary<ItemData, int> Inventory { get => inventory; }


        // Start is called before the first frame update
        void Awake()
        {
            // Luodaan uusi LootRandomizer ja haetaa itemi datat Resources kansiosta
            LR = new LootRandomizer();
            AllItems = LR.GetItemData();

            openedBoxesUI.text = OpenedBoxes.ToString();
        }

        /// <summary>
        /// Testi metodi joka näyttää konsolissa inventoryn
        /// </summary>
        public void PrintInventory()
        {
            foreach (var item in Inventory)
            {
                Debug.Log("Item: " + item.Key.ItemName + ", amount: " + item.Value);
            }
        }

        /// <summary>
        /// Testi metodi: avaa Common laatikon
        /// </summary>
        public void OpenCommonBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Common);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        /// <summary>
        /// Testi metodi: avaa Rare laatikon
        /// </summary>
        public void OpenRareBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Rare);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        /// <summary>
        /// Testi metodi: avaa Epic laatikon
        /// </summary>
        public void OpenEpicBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Epic);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        /// <summary>
        /// Testi metodi: avaa Legendary laatikon
        /// </summary>
        public void OpenLegendaryBox()
        {
            List<ItemData> Items = LR.InitializeLootbox(Legendary);

            foreach (ItemData item in Items)
            {
                AddItemToInventory(item, 1);
            }
        }

        /// <summary>
        /// LIsää ItemDatan inventoryyn ItemData scriptable objekti referenssillä
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="amount"></param>
        public void AddItemToInventory(ItemData itemData, int amount)
        {
            if (!Inventory.ContainsKey(itemData))
            {
                // Mikäli ItemDataa ei ole olemassa, luodaan uusi ItemData Dictionaryyn
                Inventory.Add(itemData, amount);
            }
            else
            {
                // Mikäli ItemData on olemassa, lisätään kyseiseen ItemDataan "+= amount" lisää
                Inventory[itemData] += amount;
            }
        }

        /// <summary>
        /// LIsää ItemDatan inventoryyn ItemData itemin nimellä
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="amount"></param>
        public void AddItemToInventory(string itemName, int amount)
        {
            // käy läpi jokaisen itemin AllItems listasta, ja lisää itemin inventoryyn käyttämällä ItemData muuttujana
            foreach (ItemData item in AllItems)
            {
                if (item.ItemName.Equals(itemName))
                {
                    Debug.Log("Added item with name " + itemName + " to inventory");
                    AddItemToInventory(item, amount);
                }
            }
        }
    }
}