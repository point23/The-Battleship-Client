using Runtime.Common.Abstract;
using Runtime.GameBase;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;

namespace Runtime.Common.Responders
{
    public class BoardGenerator : GameObjectGenerator
    {
        protected override void GenerateEach(JSONNode json)
        {
            DebugPG13.Log("data", json);
            var board = GenerateAs(json["id"].Value).GetComponent<Board>();
            board.Init(new BoardData(json));
            board.GenerateGrids();
        }
    }
}