using System;
using System.Collections.Generic;
using ThirdParty.SimpleJSON;

namespace Runtime.GameBase
{
    [Serializable]
    public class FleetData
    {
        public string boardId;
        public List<PolyominoData> polyominoes;

        public FleetData(JSONNode json)
        {
            boardId = json["boardId"].Value;
            polyominoes = PolyominoData.ReadListFromJson(json["polyominoes"]);
        }
    }
}