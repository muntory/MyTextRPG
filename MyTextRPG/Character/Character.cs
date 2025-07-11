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
        int dungeonCountToLevelUp = 1;

        /// <summary>
        /// 레벨업 하기까지 남은 던전 클리어 횟수 ex) Lv.1 -> Lv.2 던전 1회 클리어 필요
        /// </summary>
        public int DungeonCountToLevelUp
        {
            get
            {
                return dungeonCountToLevelUp;
            }
            set
            {
                dungeonCountToLevelUp = value;

                if (dungeonCountToLevelUp == 0)
                {
                    LevelUp();
                    dungeonCountToLevelUp = characterStat.Level;
                }
                
            }
        }

        public Action<int> OnEquipItem;
        public Func<int, int> OnBuyItem;
        public Action<int> OnSellItem;
        public Action OnDie;
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
            equipList = new Dictionary<ItemType, int>();

            OnEquipItem += EquipItem;
            OnBuyItem += BuyItem;
            OnSellItem += SellItem;
            OnDie += Die;
            OnRest += Rest;

            Gold = 1500;
        }

        /// <summary>
        /// <paramref name="itemId"/> 아이템을 장착, 이미 장착중이라면 장착해제, 같은 타입의 아이템 장착시 기존의 아이템 장착해제 후 장착
        /// </summary>
        /// <param name="itemId"></param>
        public void EquipItem(int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
            if (itemData == null) return;
            int modifierValue;

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
                    characterStat.ModifierAttack += modifierValue;
                    break;
                case CharacterStat.CharacterStatType.Defense:
                    characterStat.ModifierDefense += modifierValue;
                    break;
                default:
                    break;
            }

        }

        // return
        // -1 : 이미 구매한 아이템
        // 0 : 금액 부족
        // 1 : 구매 성공
        /// <summary>
        /// <paramref name="itemId"/>해당 아이템 구매
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>int (이미 구매한 아이템 : -1, 금액 부족 : 0, 구매 성공 : 1)</returns>
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

        /// <summary>
        /// 장착 중일시 해당 아이템 장착 해제 후 판매
        /// </summary>
        /// <param name="itemId"></param>
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
            characterStat.Health += RestScene.HealAmount;
            return 1;

        }

        public void Die() // 죽으면 랜덤으로 아이템 잃어버리는 시스템
        {
            if (inventory.Count == 0)
            {
                Gold = (int)(Gold * 0.7f);
            }
            else
            {
                int randomItemId = inventory[new Random().Next(inventory.Count)];

                if (equipList.ContainsValue(randomItemId))
                {
                    EquipItem(randomItemId); // 장착중인 아이템은 해제
                }
                inventory.Remove(randomItemId); // 아이템 제거
            }

            characterStat.Health = 1;
            GameManager.Instance.CurrentScene = new IntroScene();
        }

        public void LevelUp()
        {
            characterStat.Level++;
            characterStat.BaseAttack += 0.5;
            characterStat.BaseDefense += 1.0;

        }
    }
}
