using DataTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameBase
{
    public class Grid : MonoBehaviour
    {
        [HideInInspector]
        public GridData data;
        [HideInInspector]
        public UnityEvent<GridData> gridClickedEvent;
        public Coord Coord => data.coord;
        public Vector3 Position => transform.position;
        public Vector3 LocalPosition => transform.localPosition;
        private Image GridImage => GetComponentInChildren<Image>();
        private Button BtnGrid => GetComponentInChildren<Button>();

        public void Start()
        {
            BtnGrid.onClick.AddListener(OnClicked);
        }

        public void Render(bool isActive, bool isValid = true)
        {
            if (!isActive)
            {
                GridImage.color = new Color();
            }
            else if (isValid)
            {
                GridImage.color = Color.black;
            }
            else
            {
                GridImage.color = Color.red;
            }
        }

        public string ToJson()
        {
            return data.ToJson();
        }

        private void OnClicked()
        {
            gridClickedEvent.Invoke(data);
        }
    }
}
