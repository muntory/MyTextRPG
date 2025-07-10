using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTextRPG
{
    [System.Serializable]
    internal class SaveData
    {
        public string Name { get; set; }
        public string RootClass { get; set; }
        public int Gold { get; set; }
        public int DungeonCountToLevelUp { get; set; }

        public int Level { get; set; }
        public double BaseAttack { get; set; }
        public double ModifierAttack { get; set; }
        public double BaseDefense { get; set; }
        public double ModifierDefense { get; set; }
        public int Health { get; set; }

        public List<int> Inventory { get; set; }
        public Dictionary<ItemType, int> EquipList { get; set; }

    }
}
