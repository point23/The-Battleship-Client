using System.Collections;
using System.Collections.Generic;
using DataTypes;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class Grid : MonoBehaviour
    {
        [HideInInspector]
        public GridData data;

        public Vector2Int Pos => data.Pos;
        
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
