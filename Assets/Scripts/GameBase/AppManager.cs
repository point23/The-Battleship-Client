using UnityEngine;
using UnityEngine.Serialization;

namespace GameBase
{
    public class AppManager : MonoBehaviour
    {
        public static AppManager Instance = new AppManager();
        public Vector2 CellSize = new Vector2(100, 100);
        
        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
        }

        #region Data Paths
        
        [HideInInspector] 
        public string shipJsonDataPath = Application.dataPath + "/Data/" + "ships_data.json";
        [HideInInspector] 
        public string testShipsJsonDataPath =  Application.dataPath + "/Data/Test/ships_data_test.json";
        
        #endregion

    }
}