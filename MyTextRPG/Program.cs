using System.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyTextRPG
{
    internal class Program
    {
        ResourceManager resourceManager;
        GameManager gameManager;
        
        static void Main(string[] args)
        {
            Program program = new Program();

            program.Init();

            while (true)
            {
                program.Render();
                program.Update();
            }

        }

        private void Init()
        {
            gameManager = new GameManager();
            GameManager.Instance.CurrentScene = new IntroScene();
            GameManager.Instance.Player = new Character();
            GameManager.Instance.LoadGameData();

            resourceManager = new ResourceManager();
            resourceManager.LoadItemData("Data/ItemDataList.json");
            resourceManager.LoadStoreItemData("Data/StoreItemDataList.json");

        }

        private void Update()
        {
            GameManager.Instance.UserInput = GameManager.Instance.CurrentScene.GetInput();
        }

        private void Render()
        {
            GameManager.Instance.CurrentScene.Render();
        }
        
    }
}
