using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.GameBase
{
    public class Grid : MonoBehaviour
    {
        public GridData data;
        
        #region Properties
        
        public Coord Coord => data.coord;

        public bool IsActive
        {
            get => data.isActive;
            set
            {
                data.isActive = value;
                IsVisible = value;
            }
        }

        public bool IsVisible
        {
            get => data.isVisible;
            set
            {
                data.isVisible = value;
                IsInteractable = value;
                Render();
            }
        }
        
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
            if (!IsActive || !IsVisible)
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

    public class NullGrid : Grid
    {
        
    }
}
