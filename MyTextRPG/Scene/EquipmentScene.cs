using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class EquipmentScene : Scene
    {
        public enum EquipmentCommand
        {
            Invalid = -1,
            Quit,

        }

        private string GetItemDescriptionString(int index, int itemId)
        {
            ItemData itemData = ResourceManager.Instance.GetItemData(itemId);

            if (itemData == null)
                return string.Empty;

            string EquipMark;
            if (GameManager.Instance.Player.equipList.ContainsValue(itemData.Id))
            {
                EquipMark = "[E]";
            }
            else
            {
                EquipMark = "";
            }
           
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

            return $"{ConsolePrintManager.PadRightToWidth($"- {index} {EquipMark}{itemData.Name}", 22)} | {ConsolePrintManager.PadRightToWidth($"{StatType} + {itemData.Value}", 12)} | {ConsolePrintManager.PadRightToWidth(itemData.Description, 60)}";
        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            int itemIndex = 1;

            foreach (int itemId in GameManager.Instance.Player.inventory)
            {

                ItemData itemData = ResourceManager.Instance.GetItemData(itemId);

                if (itemData != null)
                {
                    Console.WriteLine(GetItemDescriptionString(itemIndex, itemId));
                    itemIndex++;

                }
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
                if (ret <= GameManager.Instance.Player.inventory.Count)
                {
                    GameManager.Instance.Player.OnEquipItem?.Invoke(GameManager.Instance.Player.inventory[ret - 1]);
                    return ret;
                }
            }

            EquipmentCommand cmd = (EquipmentCommand)ret;

            switch (cmd)
            {
                case EquipmentCommand.Quit:
                    GameManager.Instance.CurrentScene = new InventoryScene();
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
