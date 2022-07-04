using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;

namespace GameBase
{
    public class Board : MonoBehaviour
    {
        public GameObject gridTemplate;
        public GridLayoutGroup gridsLayout;
        public Transform chessLayout;
        public int rows = 9;
        public int cols = 9;
        public List<Grid> grids;
        public Bounds Bounds => GetComponent<BoxCollider>().bounds;
        public BoundingBox BoundingBox => new BoundingBox(cols, rows);
        private Vector2 CellSize
        {
            get => gridsLayout.cellSize;
            set => gridsLayout.cellSize = value;
        }
        public UnityEvent<GridData> onGridClickedEvent;
        
        //  *________
        // |__|__|__|
        // |__|__|__|
        // |__|__|__|
        private Vector3 TopLeftPos => grids.First().transform.localPosition;
        private BoxCollider Collider => GetComponent<BoxCollider>();
        private float Height => CellSize.y * rows;
        private float Width => CellSize.x * cols;
        private Vector3 Size => new Vector3(Height, Width);

        public void Awake()
        {
            CellSize = AppManager.instance.cellSize;
            grids = new List<Grid>();
            Collider.size = Size;
        }

        public void GenerateGrids(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    GenerateGrid(i, j);
                }
            }
        }

        public Vector3 LocalPositionOfCoord(Coord coord)
        {
            if (BoundingBox.IsCoordIn(coord))
            {
                var index = coord.ToIndex(cols);
                return grids[index].transform.localPosition;
            }
            
            var delta = coord.CalculateDelta(Coord.Zero);
            var displacement = new Vector2(delta.y, -delta.x) * CellSize;
            var pos = TopLeftPos + (Vector3) displacement;
            return pos;
        }

        public Coord CoordOfPosition(Vector3 position)
        {
            var localPosition = transform.InverseTransformPoint(position);
            var deltaX = localPosition.x - grids[0].LocalPosition.x + (CellSize.x / 2);
            var deltaY = grids[0].LocalPosition.y - localPosition.y + (CellSize.y / 2);
            var coord = new Coord( (int) (deltaY / CellSize.y), (int) (deltaX / CellSize.x));
            // DebugPG13.Log(new Dictionary<object, object>()
            // {
            //     {"deltaX", deltaX},
            //     {"deltaY", deltaY},
            //     {"coord", coord.ToJson()}
            // });
            return coord;
        }
        
        private void GenerateGrid(int i, int j)
        {
            var grid = Instantiate(gridTemplate, gridsLayout.transform).GetComponent<Grid>();
            grid.data = new GridData(i, j);
            grids.Add(grid);
            grid.gridClickedEvent.AddListener(OnGridClicked);
        }
        
        private void OnGridClicked(GridData data)
        {
            onGridClickedEvent.Invoke(data);
        }

    }
}
