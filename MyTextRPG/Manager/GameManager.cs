using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get { return instance; }
        }
        
        public Character Player;
        public int UserInput;
        public Scene CurrentScene;
        

        public GameManager()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        /// <summary>
        /// 게임 데이터를 JSON 파일로 저장
        /// </summary>
        public void SaveGameData()
        {
            string savePath = $"{ResourceManager.GameRootDir}/Save/Character.json";
            string directory = Path.GetDirectoryName(savePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            

            SaveData saveData = GetSaveData();
            
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }

            };

            string json = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText(savePath, json);
        }

        /// <summary>
        /// JSON 데이터 파일을 로드해서 플레이어 캐릭터에 저장된 값을 적용
        /// </summary>
        public void LoadGameData()
        {
            string savePath = $"{ResourceManager.GameRootDir}/Save/Character.json";

            if (!File.Exists(savePath))
            {
                return;
            }

            string json = File.ReadAllText(savePath);

            var options = new JsonSerializerOptions
            { 
                Converters = { new JsonStringEnumConverter() }
            };

            SaveData saveData = JsonSerializer.Deserialize<SaveData>(json, options);

            Player.Name = saveData.Name;
            Player.RootClass = saveData.RootClass;
            Player.Gold = saveData.Gold;
            Player.DungeonCountToLevelUp = saveData.DungeonCountToLevelUp;

            Player.characterStat.Level = saveData.Level;

            Player.characterStat.BaseAttack = saveData.BaseAttack;
            Player.characterStat.ModifierAttack = saveData.ModifierAttack;

            Player.characterStat.BaseDefense = saveData.BaseDefense;
            Player.characterStat.ModifierDefense = saveData.ModifierDefense;

            Player.characterStat.Health = saveData.Health;

            Player.inventory = saveData.Inventory;
            Player.equipList = saveData.EquipList;
        }

        /// <summary>
        /// 현재 플레이어 캐릭터의 데이터를 저장하기 위한 SaveData를 Return
        /// </summary>
        /// <returns>캐릭터 저장 정보가 포함되어 있는 SaveData</returns>
        private SaveData GetSaveData()
        {
            SaveData saveData = new SaveData();

            saveData.Name = Player.Name;
            saveData.RootClass = Player.RootClass;
            saveData.Gold = Player.Gold;
            saveData.DungeonCountToLevelUp = Player.DungeonCountToLevelUp;

            saveData.Level = Player.characterStat.Level;
            saveData.BaseAttack = Player.characterStat.BaseAttack;
            saveData.ModifierAttack = Player.characterStat.ModifierAttack;

            saveData.BaseDefense = Player.characterStat.BaseDefense;
            saveData.ModifierDefense = Player.characterStat.ModifierDefense;

            saveData.Health = Player.characterStat.Health;

            saveData.Inventory = Player.inventory;
            saveData.EquipList = Player.equipList;
            
            return saveData;
        }

        
    }
}
