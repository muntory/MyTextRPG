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

        public CharacterStat characterStat;
        public List<int> inventory;
        public List<int> equipList;

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
            equipList = new List<int>();
            Gold = 1500;
        }

        public void EquipItem(int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
            if (itemData == null) return;
            int modifierValue;
            
            if (equipList.Contains(itemId))
            {
                modifierValue = -itemData.Value;
                equipList.Remove(itemId);
            }
            else
            {
                modifierValue = itemData.Value;
                equipList.Add(itemId);
            }

            switch (itemData.StatType)
            {
                case CharacterStat.CharacterStatType.Attack:
                    characterStat.Attack += modifierValue;
                    break;
                case CharacterStat.CharacterStatType.Armor:
                    characterStat.Armor += modifierValue;
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

      
    }
}
