using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Utilities;
using ThirdParty.SimpleJSON;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Runtime.GameBase
{
    [Serializable]
    public struct PolyominoData
    {
        public bool isVisible;
        public Coord topLeft;
        public BoundingBox bounds;
        public int angle;
        public List<Coord> gridCoords;

        public PolyominoData(Coord topLeft, BoundingBox bounds, int angle, List<Coord> gridCoords, bool isVisible = true)
        {
            this.isVisible = isVisible;
            this.topLeft = topLeft;
            this.bounds = bounds;
            this.angle = angle;
            this.gridCoords = gridCoords;
        }

        private PolyominoData(JSONNode json)
        {
            isVisible = json["isVisible"].AsBool;
            topLeft = new Coord(json["topLeft"]);
            bounds = new BoundingBox(json["bounds"]);
            angle = json["angle"].AsInt;
            gridCoords = Coord.ReadListFromJson(json["gridCoords"]);
        }

        public void RotateClockwiseAround(Vector2 centerPoint)
        {
            topLeft.RotateClockwiseAround(centerPoint, -90);
            RotateDiagonalVectorClockwise();
        }

        public void RotateDiagonalVectorClockwise()
        {
            angle = (angle - 90) % 360;
        }

        public static List<PolyominoData> ReadListFromJson(JSONNode jsonList)
        {
            return jsonList.Children.Select(json => new PolyominoData(json)).ToList();
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}