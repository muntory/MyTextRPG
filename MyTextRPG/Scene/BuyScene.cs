using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class BuyScene : Scene
    {
        public enum BuyCommand
        {
            Invalid = -1,
            Quit,

        }

        Store store;

        public BuyScene()
        {
            store = new Store();
        }

        private string GetItemDescriptionString(int index, int itemId)
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

            return $"{ConsolePrintManager.PadRightToWidth($"- {index} {itemData.Name} ", 22)} | {ConsolePrintManager.PadRightToWidth($"{StatType} + {itemData.Value}", 12)} | {ConsolePrintManager.PadRightToWidth(itemData.Description, 60)} | {ConsolePrintManager.PadLeftToWidth((GameManager.Instance.Player.inventory.Contains(itemId) ? "구매완료" : $"{storeItemData.Price} G"), 8)}";

        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.Instance.Player.Gold} G");
            Console.WriteLine();


            int itemIndex = 1;
            foreach (int itemId in store.itemList)
            {
                ItemData itemData = ResourceManager.Instance.GetItemData(itemId);
                if (itemData == null) continue;

                Console.WriteLine(GetItemDescriptionString(itemIndex, itemId));
                itemIndex++;

            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            PrintErrorMsg();


        }

        public override int GetInput()
        {
            int ret = base.GetInput();

            if (ret > 0)
            {
                if (ret <= store.itemList.Count)
                {
                    int? result = GameManager.Instance.Player.OnBuyItem?.Invoke(store.itemList[ret - 1]);

                    if (result.HasValue)
                    {
                        if (result > 0)
                        {
                            msg = "구매를 완료했습니다.";
                        }
                        else if (result < 0)
                        {
                            msg = "이미 구매한 아이템입니다.";
                        }
                        else
                        {
                            msg = "Gold가 부족합니다.";
                        }
                    }
                    return ret;
                }
                
            }

            BuyCommand cmd = (BuyCommand)ret;

            switch (cmd)
            {
                case BuyCommand.Quit:
                    GameManager.Instance.CurrentScene = new StoreScene();
                    break;
                default:
                    ret = -1;
                    msg = "잘못된 입력입니다.";
                    break;
            }

            return ret;

        }
        
    }
}
