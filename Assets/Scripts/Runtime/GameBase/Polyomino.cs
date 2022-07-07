using System.Collections.Generic;
using System.Linq;
using Runtime.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Runtime.GameBase
{
    public class Polyomino : MonoBehaviour
    {
        public GridLayoutGroup layoutGroup;
        public GameObject polyominoGridTemplate;
        [HideInInspector] public PolyominoData data;
        [HideInInspector] public PolyominoesHandler handler;
        [HideInInspector] public UnityEvent<Polyomino> relocatedEvent;

        #region Properties

        public Coord TopLeft
        {
            get => data.topLeft;
            private set => data.topLeft = value;
        }
        public bool IsGridsValid
        {
            get => GridList.All(grid => grid.IsValid);
            set => GridList.ForEach(grid => grid.IsValid = value);
        }
        public Coord BottomRight => TopLeft + DiagonalVector;
        public List<Coord> GridCoordsInWorldSpace => GridCoords.Select(FromLocalToWorldCoord).ToList();
        private BoundingBox Bounds => data.bounds;
        private int Angle => data.angle;
        private List<Coord> GridCoords => data.gridCoords;
        private Vector2 DiagonalVector => RotateVectorClockwise(Bounds.ToDiagonalVector(), Angle);
        private int GridsCount => Bounds.height * Bounds.width;
        private List<Grid> GridList => layoutGroup.GetComponentsInChildren<Grid>().ToList();
        private DragDropItemGroup DragDropItemGroup => GetComponent<DragDropItemGroup>();
        private MultiClickItemGroup MultiClickItemGroup => GetComponent<MultiClickItemGroup>();
        private bool AllGridsInBounds => handler.AllGridsInBounds(this);
        private IEnumerable<int> OccupiedGridsIndex => from gridCoord in GridCoords select Bounds.ConvertCoordToIndex(gridCoord);

        #endregion
        
        public void Start()
        {
            DragDropItemGroup.draggedEvent.AddListener(OnDragged);
            MultiClickItemGroup.multiClickedEvent.AddListener(OnRotated);
        }

        public void Init(PolyominoesHandler handler, PolyominoData data)
        {
            this.handler = handler;
            this.data = data;
            Render();
        }
        
        public void RenderGrids()
        {
            GridList.ForEach(grid => grid.Render());
        }

        private void Render()
        {
            SetLayoutGroup();
            InstantiateGridObjects();
            InitGrids();
            
            OnRelocated();
            CheckGridsValidity();
            RenderGrids();
            InitDragDropGroup();
            InitMultiClickItemGroup();
        }

        private void InitMultiClickItemGroup()
        {
            MultiClickItemGroup.Init(DragDropItemGroup);
        }

        private void InitDragDropGroup()
        {
            DragDropItemGroup.Init(new Diastimeter(handler.board), MultiClickItemGroup);
        }

        private void OnRelocated()
        {
            relocatedEvent.Invoke(this);
        }

        private void SetLayoutGroup()
        {
            layoutGroup.constraintCount = Bounds.width;
        }

        private void InstantiateGridObjects()
        {
            for (var count = 0; count < GridsCount; count++)
            {
                Instantiate(polyominoGridTemplate, layoutGroup.transform).GetComponent<Grid>();
            }
        }

        private void InitGrids()
        {
            for (var row = 0; row < Bounds.height; row++)
            {
                for (var col = 0; col < Bounds.width; col++)
                {
                    var coord = new Coord(row, col);
                    var index = Bounds.ConvertCoordToIndex(coord);
                    var isActive = OccupiedGridsIndex.Contains(index);
                    GridList[index].Init(new GridData(coord, isActive));
                    GridList[index].GetComponent<DragDropItem>().IsActive = isActive;
                    GridList[index].GetComponent<MultiClickItem>().IsActive = isActive;
                    // DebugPG13.Log(new Dictionary<object, object>()
                    // {
                    //     {"grid coord", coord.ToJson()},
                    //     {"index", coord.ToIndex(Bounds.width)},
                    //     {"isOccupied", isOccupied},
                    //     {"isActive", GridList[gridIndex].GetComponent<DragDropItem>().isActive}
                    // });
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

        private bool TryMove(Vector2Int delta)
        {
            if (!handler.AnyGridsInBoundsAfterMove(this, delta))
                return false;

            TopLeft += delta;
            return true;
        }

        private void CheckGridsValidity()
        {
            IsGridsValid = IsGridsValid && AllGridsInBounds;
        }

        private Coord FromLocalToWorldCoord(Coord localCoord)
        {
            var coord = new Coord(TopLeft);
            var delta = RotateVectorClockwise(localCoord.ToVector2(), Angle);
            coord += delta;
            return coord;
        }

        private Vector3 RotateVectorClockwise(Vector2 vector, int angle)
        {
            return Quaternion.Euler(0, 0, angle) * vector;
        }

        private void RotateClockwiseAround(Vector2 centerPoint)
        {
            data.RotateClockwiseAround(centerPoint);
            transform.Rotate(0,0,-90);
        }

        #region Event Listeners

        private void OnDragged(Vector2Int delta)
        {
            if (!TryMove(delta)) 
                return;
            
            OnRelocated();
            CheckGridsValidity();
            RenderGrids();
        }

        private void OnRotated(MultiClickItem item)
        {
            // DebugPG13.Log(new Dictionary<object, object>()
            // {
            //     {"center coord before", item.GetComponent<Grid>().Coord.ToJson()},
            //     {"center before", FromLocalToWorldCoord(item.GetComponent<Grid>().Coord).ToVector2()},
            //     {"diagonal before", DiagonalVector},
            //     {"topLeft before", TopLeft.ToJson()}
            // });
            RotateClockwiseAround(FromLocalToWorldCoord(item.GetComponent<Grid>().Coord).ToVector2());
            // DebugPG13.Log(new Dictionary<object, object>()
            // {
            //     {"diagonal after", DiagonalVector},
            //     {"center after", FromLocalToWorldCoord(item.GetComponent<Grid>().Coord).ToVector2()},
            //     {"topLeft after", TopLeft.ToJson()}
            // });
            OnRelocated();
            CheckGridsValidity();
            RenderGrids();
            // DebugPG13.Log("Ship Data", data.ToJson());
        }
        
        #endregion
        
        public string ToJson()
        {
            return data.ToJson();
        }
    }
}