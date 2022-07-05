using DataTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace GameBase
{
    public class Grid : MonoBehaviour
    {
        public GridData data;
        
        #region Properties
        
        public Coord Coord => data.coord;
        public bool IsOccupied => data.isOccupied;
        public bool IsValid
        {
            get => data.isValid;
            set => data.isValid = value;
        }
        
        public bool IsInteractable
        {
            get => BtnGrid.interactable;
            set => BtnGrid.interactable = value;
        }
        
        private Button BtnGrid => GetComponent<Button>();

        public Vector3 Position => transform.position;
        public Vector3 LocalPosition => transform.localPosition;
        public UnityEvent<Coord> onGridClicked;
        
        private Image GridImage => GetComponentInChildren<Image>();

        #endregion
        
        public void Init(GridData gridData, bool isInteractable=true)
        {
            data = gridData;
            IsInteractable = isInteractable;
            BtnGrid.onClick.AddListener(OnClick);
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
        
        private void OnClick()
        {
            onGridClicked.Invoke(Coord);
        }

        #region Convertors

        public override string ToString()
        {
            return data.ToString();
        }

        #endregion
    }
}
