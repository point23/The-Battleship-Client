using DataTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameBase
{
    public class Grid : MonoBehaviour
    {
        public GridData data;
        [HideInInspector] public UnityEvent<GridData> gridClickedEvent;

        #region Properties

        public Coord Coord => data.coord;
        public bool IsOccupied => data.isOccupied;
        public bool IsValid
        {
            get => data.isValid;
            set => data.isValid = value;
        }
        public Vector3 Position => transform.position;
        public Vector3 LocalPosition => transform.localPosition;
        private Image GridImage => GetComponentInChildren<Image>();
        private Button BtnGrid => GetComponentInChildren<Button>();

        #endregion

        public void Start()
        {
            BtnGrid.onClick.AddListener(OnClicked);
        }

        public void Init(GridData gridData)
        {
            data = gridData;
        }

        public void Render()
        {
            if (!IsOccupied)
            {
                GridImage.color = new Color();
            }
            else if (IsValid)
            {
                GridImage.color = Color.white;
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
