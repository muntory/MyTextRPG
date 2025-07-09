using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyTextRPG.BuyScene;
using static System.Formats.Asn1.AsnWriter;

namespace MyTextRPG
{
    internal class SellScene : Scene
    {
        public enum SellCommand
        {
            Invalid = -1,
            Quit,

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
                case CharacterStat.CharacterStatType.Armor:
                    StatType = "방어력";
                    break;
                case CharacterStat.CharacterStatType.Health:
                    StatType = "체력";
                    break;
                default:
                    StatType = "";
                    break;
            }

            return $"- {index} {itemData.Name}\t| {StatType} {(itemData.Value >= 0 ? "+" : "-")}{itemData.Value}\t| {itemData.Description}\t| {(int)(storeItemData.Price * 0.85f)} G";
        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("보유중인 아이템을 판매할 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Program.Player.Gold} G");
            Console.WriteLine();


            int itemIndex = 1;
            foreach (int itemId in Program.Player.inventory)
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
                if (ret <= Program.Player.inventory.Count)
                {
                    Program.Player.OnSellItem?.Invoke((Program.Player.inventory[ret - 1]));

                    errorMsg = "판매를 완료했습니다.";
                    return ret;
                }

            }

            SellCommand cmd = (SellCommand)ret;

            switch (cmd)
            {
                case SellCommand.Quit:
                    Program.CurrentScene = new StoreScene();
                    break;
                default:
                    ret = -1;
                    errorMsg = "잘못된 입력입니다.";
                    break;
            }

            return ret;

        }
    }

    
}
