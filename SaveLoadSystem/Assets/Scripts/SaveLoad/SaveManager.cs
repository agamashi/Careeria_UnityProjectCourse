using LootBoxSystem;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField] bool saveDataOnApplicationQuit = false;
        [SerializeField] GameObject player;

        public void SerializationSave()
        {
            BinaryFormatter bf = new BinaryFormatter();                                             // Serialisoi datan
            FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");      //luo uuden tiedoston ja lukee sitä
            SaveData data = new SaveData();                                                         // Luodaan uusi SaveData (luokka joka luotiin)

            // Tallennetaan avattujen laatikkojen määrä
            data.savedOpenedBoxes = LootManager.Instance.OpenedBoxes;
            data.savedPosition = new SavedVector(player.transform.position.x, player.transform.position.y, player.transform.position.z, 0);
            data.savedRotation = new SavedVector(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);

            // Tallennetaan pelaajan inventory. Inventory on muotoa Dictionary, joten pitää hakea jokainen Key/Value pari ja muuttaa
            // se muotoon "SavedInventoryData", jossa "Key" on ItemData.itemName ja "Value" on itemeiden määrä inventoryssä
            foreach (var inventoryItem in LootManager.Instance.Inventory)
            {
                data.savedInventory.Add(new SavedInventoryData(inventoryItem.Key.ItemName, inventoryItem.Value));
            }

            bf.Serialize(file, data);                                                               // Serialisoidaan data tiedostoon, eli tallennetaan
            file.Close();                                                                           //suljetaan tiedoston lukeminen
            Debug.Log("Game data saved with Serialization!");

        }

        public void SerializationLoad()
        {
            // Test: Clear inventory before loading
            LootManager.Instance.Inventory.Clear();

            if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))                                     // Tarkistetaan onko tiedostoa olemassa
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);       //Luetaan tiedostoa
                SaveData data = (SaveData)bf.Deserialize(file);                                                      // Deserialisoidaan tiedosto
                file.Close();                                                                                        // Suljetaan tiedoston lukeminen

                LootManager.Instance.OpenedBoxes = data.savedOpenedBoxes;
                player.transform.position = new Vector3(data.savedPosition.x, data.savedPosition.y, data.savedPosition.z);
                player.transform.rotation = new Quaternion(data.savedPosition.x, data.savedPosition.y, data.savedPosition.z, 0f);

                // Lisätään LootManagerin inventoryyn (Dictionary) kaikki itemit jotka olivat tallennettu tiedostoo
                foreach (var savedItem in data.savedInventory)
                {
                    LootManager.Instance.AddItemToInventory(savedItem.itemName, savedItem.amount);
                }

                Debug.Log("Game data loaded with Serialization");
            }
            else
            {
                Debug.Log("No serialized game data was found!");
            }
        }

        public void SerializationClear()
        {
            if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
            {
                File.Delete(Application.persistentDataPath + "/MySaveData.dat");
                Debug.Log("Save Data was reset successfully");
            }
            else
            {
                Debug.Log("No save data to clear");
            }
        }

        // Player Prefs tallennukseen ja lataamiseen tehdyt koodit
        #region PlayerPrefs
        /// <summary>
        /// Tallentaa datan käyttäen Player Prefsiä
        /// </summary>
        public void PlayerPrefsSave()
        {
            PlayerPrefs.SetInt("openedBoxes", LootManager.Instance.OpenedBoxes);
            PlayerPrefs.Save();
            Debug.Log("Player Data Saved with PlayerPrefs");
        }

        /// <summary>
        /// Lataa ja noutaa datan käyttäen Player Prefsiä
        /// </summary>
        public void PlayerPrefsLoad()
        {
            if (PlayerPrefs.HasKey("openedBoxes"))
            {
                LootManager.Instance.OpenedBoxes = PlayerPrefs.GetInt("openedBoxes");
                Debug.Log("Player Data Loaded with PlayerPrefs");
            }
            else
            {
                Debug.Log("No Save Data Found");
            }
        }

        /// <summary>
        /// Poistaa datan PlayerPrefs tiedostosta
        /// </summary>
        public void PlayerPrefsClear()
        {
            PlayerPrefs.DeleteAll();
            LootManager.Instance.OpenedBoxes = 0;
        }
        #endregion

        private void OnApplicationQuit()
        {
            if (saveDataOnApplicationQuit)
            {
                SerializationSave();
                PlayerPrefsSave();
            }
        }
    }

    /// <summary>
    /// SaveData luokka, jota käytetään Serialisointi tallennus systeemissä. Sisältää ne muuttujat jota halutaan tallentaa
    /// </summary>
    [System.Serializable]
    class SaveData
    {
        public int savedOpenedBoxes;
        public List<SavedInventoryData> savedInventory = new List<SavedInventoryData>();
        public SavedVector savedPosition;
        public SavedVector savedRotation;
    }

    /// <summary>
    /// Inventory data joka halutaan tallentaa Serialisointi taktiikalla. Tänne lisätään ItemDatat jotka ovat LootManagerin Dictionary Inventory muuttujassa.
    /// Itemit tallennetaan stringeillä, jotka sitten haetaa AllItems listasta ja lisätään oikeaan inventoryyn
    /// </summary>
    [System.Serializable]
    class SavedInventoryData
    {
        public string itemName;  // Dictionary Key (as string => ItemData.ItemName)
        public int amount;     // Dictionary Value

        public SavedInventoryData(string _itemName, int _amount)
        {
            itemName = _itemName;
            amount = _amount;
        }
    }

    [Serializable]
    class SavedVector
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public SavedVector(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public Vector3 GetAsVector3()
        {
            return new Vector3(x, y, z);
        }

        public Quaternion GetAsQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
    }
}