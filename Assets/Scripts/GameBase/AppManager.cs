using UnityEngine;

namespace GameBase
{
    public class AppManager
    {
        public static AppManager instance;
        public string ShipJsonDataPath = Application.dataPath + "/Data/" + "ships_data.json";
    }
}