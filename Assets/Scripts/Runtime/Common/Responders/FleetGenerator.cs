using Runtime.Common.Abstract;
using Runtime.Core;
using Runtime.GameBase;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;

namespace Runtime.Common.Responders
{
    public class FleetGenerator : GameObjectGenerator
    {
        protected override void GenerateEach(JSONNode json)
        {
            DebugPG13.Log("data", json);

            var fleetData = new FleetData(json);
            var board = GameManager.instance.TryGetGameObject(fleetData.boardId).GetComponent<Board>();
            board.polyominoesHandler.GeneratePolyominoes(fleetData.polyominoes);
        }
    }
}