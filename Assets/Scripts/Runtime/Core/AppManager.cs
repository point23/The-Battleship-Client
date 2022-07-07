using UnityEngine;

namespace Runtime.Core
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
        
        #endregion

    }
}