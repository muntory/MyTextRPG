using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyTextRPG
{
    internal class Character
    {
        public string Name { get; set; }
        public string RootClass { get; set; }
        public int Gold { get; set; }

        public Action<int> OnEquipItem;
        public Func<int, int> OnBuyItem;
        public Action<int> OnSellItem;
        public Func<int> OnRest;

        public CharacterStat characterStat;
        public List<int> inventory;
        public Dictionary<ItemType, int> equipList;
        

        public Character()
        {
            Name = "Chad";
            RootClass = "전사";
            characterStat = new CharacterStat();
            inventory = new List<int>();
            inventory.Add(1);
            inventory.Add(2);
            inventory.Add(3);

            OnEquipItem += EquipItem;
            OnBuyItem += BuyItem;
            OnSellItem += SellItem;
            OnRest += Rest;
            equipList = new Dictionary<ItemType, int>();
            Gold = 1500;
        }

        public void EquipItem(int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
            if (itemData == null) return;
            int modifierValue;
            int equippedItemId;

            if (equipList.Values.Contains(itemId))
            {
                equipList.Remove(itemData.Type);
                modifierValue = -itemData.Value;

            }
            else if (equipList.ContainsKey(itemData.Type))
            {
                ItemData equippedItemData = ResourceManager.Instance.GetItemData(equipList[itemData.Type]);
                equipList.Remove(equippedItemData.Type);
                modifierValue = -equippedItemData.Value;

                equipList.Add(itemData.Type, itemData.Id);
                modifierValue += itemData.Value;
            }
            else
            {
                equipList.Add(itemData.Type, itemData.Id);
                modifierValue = itemData.Value;
            }

            switch (itemData.StatType)
            {
                case CharacterStat.CharacterStatType.Attack:
                    characterStat.Attack += modifierValue;
                    break;
                case CharacterStat.CharacterStatType.Defense:
                    characterStat.Defense += modifierValue;
                    break;
                case CharacterStat.CharacterStatType.Health:
                    characterStat.Health += modifierValue;
                    break;
                default:
                    break;
            }

        }

        // return
        // -1 : 이미 구매한 아이템
        // 0 : 금액 부족
        // 1 : 구매 성공
        public int BuyItem(int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);

            if (inventory.Contains(itemId))
            {
                return -1;
            }

            StoreItemData storeItemData = ResourceManager.Instance.GetStoreItemData(itemId);

            if (storeItemData.Price > Gold)
            {
                return 0;
            }

            Gold -= storeItemData.Price;
            inventory.Add(itemId);

            return 1;
        }

        public void SellItem(int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
            StoreItemData storeItemData = ResourceManager.Instance.GetStoreItemData(itemId);
            if (!inventory.Contains(itemId))
                return;

            if (equipList.ContainsKey(itemData.Type))
            {
                if (equipList[itemData.Type] == itemId)
                {
                    EquipItem(itemId);
                }
            }

            Gold += (int)(storeItemData.Price * 0.85f);
            inventory.Remove(itemId);
        }

        public int Rest()
        {
            if (Gold < RestScene.Fare)
            {
                return 0;
            }

            Gold -= RestScene.Fare;
            characterStat.Health = Math.Min(characterStat.Health + RestScene.HealAmount, 0);
            return 1;
            
        }

      
    }
}
