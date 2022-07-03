using DataTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameBase
{
    public class Grid : MonoBehaviour
    {
        public GridData data;
        public UnityEvent<GridData> gridClickedEvent;
        public Coord Coord => data.coord;
        public bool IsOccupied => data.isOccupied;
        [HideInInspector] public bool IsValid
        {
            get => data.isValid;
            set => data.isValid = value;
        }

        [HideInInspector] public Vector3 Position => transform.position;
        public Vector3 LocalPosition => transform.localPosition;
        private Image GridImage => GetComponentInChildren<Image>();
        private Button BtnGrid => GetComponentInChildren<Button>();

        public void Start()
        {
            BtnGrid.onClick.AddListener(OnClicked);
        }

        public void Init(GridData data)
        {
            this.data = data;
        }

        public void Render()
        {
            if (!IsOccupied)
            {
                GridImage.color = new Color();
            }
            else if (IsValid)
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
