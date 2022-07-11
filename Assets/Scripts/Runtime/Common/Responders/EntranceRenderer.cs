using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Common.Responders
{
    public class EntranceRenderer : MonoBehaviour, ICommandResponder
    {
        public Transform layout;
        public GameObject entrancePrefab;

        public UniTask Run(string action, JSONNode data)
        {
            switch (action)
            {
                case "Render":
                    RenderEntrances(data);
                    break;
            }
            return UniTask.CompletedTask;
        }

        private void RenderEntrances(JSONNode dataList)
        {
            DebugPG13.Log("data", dataList);
            foreach (var data in dataList.Children)
            {
                RenderEntrance(data);
            }
        }

        private void RenderEntrance(JSONNode data)
        {
            var entrance = Instantiate(entrancePrefab, layout).GetComponent<Entrance>();
            entrance.Init(data);
        }
    }
}