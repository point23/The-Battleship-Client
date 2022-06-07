using UnityEngine;

namespace GameBase
{
    public class AppManager : MonoBehaviour
    {
        public static AppManager Instance = new AppManager();
        public string ShipJsonDataPath = Application.dataPath + "/Data/" + "ships_data.json";
        public Vector2 CellSize = new Vector2(100, 100);

        public GridsHandler gridsHandler;
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
    }
}