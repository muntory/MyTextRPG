using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTextRPG
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Shield,


    }
    internal class ItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public CharacterStat.CharacterStatType StatType { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
