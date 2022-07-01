using UnityEngine;

namespace GameBase
{
    public class AppManager : MonoBehaviour
    {
        public static AppManager instance;
        public Vector2 cellSize = new Vector2(100, 100);
        
        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }
        }

        #region Data Paths
        public string ShipJsonDataPath => Application.dataPath + "/Data/" + "ships_data.json";
        public static string TestShipsJsonDataPath =>  Application.dataPath + "/Data/Test/ships_data_test.json";
        
        #endregion

    }
}