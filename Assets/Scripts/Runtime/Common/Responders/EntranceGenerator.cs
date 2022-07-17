using Runtime.Common.Abstract;
using ThirdParty.SimpleJSON;

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