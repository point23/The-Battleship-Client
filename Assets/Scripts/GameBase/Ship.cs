using System;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GameBase
{
    public class Ship : MonoBehaviour
    {
        [HideInInspector] 
        public ShipData data;
        
        public GridLayoutGroup layoutGroup;
        public GameObject shipGridTemplate;

        public Vector2Int BoundingBox => data.boundingBox;
        public Vector2Int TopLeft => data.topLeft;
        public List<Vector2Int> Grids => data.grids;
        public int GridsCount => BoundingBox.x * BoundingBox.y;
        public List<Grid> GridList => layoutGroup.GetComponentsInChildren<Grid>().ToList();

        public void Start()
        {
            GetComponent<DragDropItem>().OnDraggedEvent.AddListener(OnDragged);
            GetComponent<MultiClickHandler>().onMultiClickedEvent.AddListener(OnRotated);
        }

        public void Render(ShipData data)
        {
            this.data = data;
            SetPosition();
            SetLayoutGroup();
            InstantiateGridObjects();
            RenderGrids();
        }

        private void RenderRotation()
        {
            SetLayoutGroup();
            SetPosition();
            RenderGrids();
        }

        private void SetLayoutGroup()
        {
            layoutGroup.constraintCount = BoundingBox.x;
        }

        private void SetPosition()
        {
            var topLeftPos = AppManager.Instance.gridsHandler.GetPosOfGrid(TopLeft);
            var bottomRight = TopLeft + BoundingBox - Vector2Int.one;
            var bottomRightPos = AppManager.Instance.gridsHandler.GetPosOfGrid(bottomRight);
            // DebugLogPos(bottomRight, topLeftPos, bottomRightPos);
            transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        private void RenderGrids()
        {
            GridList.ForEach(grid => grid.Render(isActive: Grids.Contains(grid.Pos))); 
        }

        private void InstantiateGridObjects()
        {
            for (var i = 0; i < BoundingBox.x; i++)
            {
                for (var j = 0; j < BoundingBox.y; j++)
                {
                    var go = Instantiate(shipGridTemplate, layoutGroup.transform);
                    go.GetComponent<Grid>().data = new GridData(i, j);
                }
            }
        }

        private void ClearAllGrids()
        {
            foreach (var grid in GridList)
            {
                Destroy(grid.gameObject);
            }
        }

        #region Event Listeners
        
        private void OnDragged(Vector2Int delta)
        {
            data.topLeft += delta;
        }

        private void OnRotated()
        {
            data.Rotate();
            RenderRotation();
        }

        #endregion

        #region Debug Helpers

        private void DebugLogPos(Vector2Int bottomRight, Vector3 topLeftPos, Vector3 bottomRightPos)
        {
            Debug.Log("topLeft: " + TopLeft);
            Debug.Log("bottomRight: " + bottomRight);
            Debug.Log("topLeftGridPos: " + topLeftPos);
            Debug.Log("bottomRightPos: " + bottomRightPos);
            Debug.Log("position: " + transform.localPosition);
        }

        #endregion

    }
}