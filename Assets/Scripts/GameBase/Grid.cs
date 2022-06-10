using DataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class Grid : MonoBehaviour
    {
        [HideInInspector]
        public GridData data;

        public Coord Pos => data.pos;
        
        private Image GridImage => GetComponentInChildren<Image>();
        
        public void Render(bool isActive)
        {
            GridImage.color = isActive ? Color.black : new Color();
        }

        public string ToJson()
        {
            return data.ToJson();
        }
    }
}
