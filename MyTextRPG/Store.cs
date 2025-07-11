using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    public class Store
    {
        public List<int> itemList = new List<int>();

        public Store()
        {
            for (int i = 0; i < ResourceManager.Instance.GetLoadedItemDataCount(); ++i)
            {
                itemList.Add(i + 1);
            }

        }
    }
}
