﻿using Runtime.Common.Abstract;
using ThirdParty.SimpleJSON;

namespace Runtime.Common.Responders
{
    public class DialogBuilder : GameObjectBuilder
    {
        protected override void PostBuild(JSONNode json)
        {
            BuildAs(json["name"], json["id"]);
        }
    }
}