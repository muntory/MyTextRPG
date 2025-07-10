using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class StoreScene : Scene
    {

        Store store;
        // itemId, 입력 명령어

        public enum StoreCommand
        {
            Invalid = -1,
            Quit,
            Buy,
            Sell,

        }

        private string GetItemDescriptionString(int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
            StoreItemData storeItemData = ResourceManager.Instance.GetStoreItemData(itemId);
            if (itemData == null)
                return string.Empty;

            
            string StatType;
            switch (itemData.StatType)
            {
                case CharacterStat.CharacterStatType.Attack:
                    StatType = "공격력";
                    break;
                case CharacterStat.CharacterStatType.Defense:
                    StatType = "방어력";
                    break;
                case CharacterStat.CharacterStatType.Health:
                    StatType = "체력";
                    break;
                default:
                    StatType = "";
                    break;
            }


            return $"- {itemData.Name}\t| {StatType} {(itemData.Value >= 0 ? "+" : "-")}{itemData.Value}\t| {itemData.Description}\t| {(GameManager.Instance.Player.inventory.Contains(itemId) ? "구매완료" : $"{storeItemData.Price} G")}";
        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.Instance.Player.Gold} G");
            Console.WriteLine();

            foreach (int itemId in store.itemList)
            {
                ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
                if (itemData == null) continue;

                Console.WriteLine(GetItemDescriptionString(itemId));

            }
            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            PrintErrorMsg();

        }

        public override int GetInput()
        {
            int ret = base.GetInput();

            StoreCommand cmd = (StoreCommand)ret;

            switch (cmd)
            {
                case StoreCommand.Quit:
                    GameManager.Instance.CurrentScene = new IntroScene();
                    break;
                case StoreCommand.Buy:
                    GameManager.Instance.CurrentScene = new BuyScene();
                    break;
                case StoreCommand.Sell:
                    GameManager.Instance.CurrentScene = new SellScene();
                    break;
                default:
                    ret = -1;
                    errorMsg = "잘못된 입력입니다.";
                    break;
            }

            return ret;

        }

        public StoreScene()
        {
            store = new Store();

        }
    }
}
