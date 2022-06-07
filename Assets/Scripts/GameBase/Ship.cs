using System;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class Ship : MonoBehaviour
    {
        [HideInInspector] public ShipData data;
        public GridLayoutGroup layoutGroup;
        public GameObject shipGridTemplate;
        
        public Vector2Int BoundingBox => data.boundingBox;
        public Vector2Int TopLeft => data.topLeft;
        public List<Vector2Int> Grids => data.grids;
        public int GridsCount => (int) (BoundingBox.x * BoundingBox.y);

        public void Start()
        {
            layoutGroup = GetComponent<GridLayoutGroup>();
        }

        public void Render(ShipData data)
        {
            this.data = data;
            ResetPosition();
            ReShape();
        }

        private void ResetPosition()
        {
            var topLeftPos = AppManager.Instance.gridsHandler.GetPosOfGrid(TopLeft);
            var bottomRight = TopLeft + BoundingBox - Vector2Int.one;
            var bottomRightPos = AppManager.Instance.gridsHandler.GetPosOfGrid(bottomRight);
            Debug.Log("topLeft: " + TopLeft);
            Debug.Log("bottomRight: " + bottomRight);

            Debug.Log("topLeftGridPos: " + topLeftPos);
            Debug.Log("bottomRightPos: " + bottomRightPos);
            
            Debug.Log("position: " + transform.localPosition);

            transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        private void ReShape()
        {
            layoutGroup.constraintCount = (int) data.boundingBox.x;
            for (var i = 0; i < GridsCount; i++)
            {
                Instantiate(shipGridTemplate, layoutGroup.transform);
            }
            
            var gridsList = layoutGroup.GetComponentsInChildren<Grid>();
            foreach (var grid in Grids)
            {
                gridsList[grid.x * BoundingBox.x + grid.y].GetComponentInChildren<Image>().color = Color.black;
            }
        }
    }
}