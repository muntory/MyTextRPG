using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class ResourceManager
    {
        private static ResourceManager instance;
        public static ResourceManager Instance
        {
            get { return instance; }
        }

        Dictionary<int, ItemData> itemList = new Dictionary<int, ItemData>();
        Dictionary<int, StoreItemData> storeItemList = new Dictionary<int, StoreItemData>();

        public ResourceManager()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        
        public void LoadItemData(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ItemData> itemDataList = JsonSerializer.Deserialize<List<ItemData>>(json, options);

            foreach (ItemData item in itemDataList)
            {
                itemList.Add(item.Id, item);
            }

        }

        public void LoadStoreItemData(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<StoreItemData> itemDataList = JsonSerializer.Deserialize<List<StoreItemData>>(json, options);

            foreach (StoreItemData item in itemDataList)
            {
                storeItemList.Add(item.Id, item);
            }

        }

        public ItemData GetItemData(int id)
        {
            if (id <= 0 || id > itemList.Count)
            {
                return null;
            }

            return itemList[id];
        }

        public StoreItemData GetStoreItemData(int id)
        {
            if (id <= 0 || id > storeItemList.Count)
            {
                return null;
            }

            return storeItemList[id];
        }


    }
}
