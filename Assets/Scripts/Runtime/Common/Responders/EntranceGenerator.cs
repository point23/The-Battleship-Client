using Cysharp.Threading.Tasks;
using Runtime.Common.Abstract;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Common.Responders
{
    public class EntranceGenerator : GameObjectGenerator
    {
        protected override void GenerateEach(JSONNode data)
        {
            var entrance = GenerateAs(data["id"]).GetComponent<Entrance>();
            entrance.Init(data);
        }
    }
}