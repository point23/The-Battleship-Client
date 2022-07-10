using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Proyecto26;
using Runtime.Common;
using Runtime.Common.Abstract;
using Runtime.Infrastructures.JSON;
using UnityEditor;
using UnityEngine.Events;

namespace Runtime.Core
{
    public class AppSyncService : AbstractSyncService
    {
        public readonly UnityEvent<List<Command>> onInit = new UnityEvent<List<Command>>();
        public readonly UnityEvent<List<Command>> onLogin = new UnityEvent<List<Command>>();
        
        public void InitService(string uri)
        {
            var request = new RequestHelper {Uri = uri};
            GetRequest(request, response =>
            {
                onInit.Invoke(new JsonDataList(response.Text).ToCommands());
                return UniTask.CompletedTask;
            });
        }
        
        public void LoginService(string uri)
        {
            var request = new RequestHelper {Uri = uri};
            PostRequest(request, response =>
            {
                EditorUtility.DisplayDialog("response", response.Text, "Ok");
                onLogin.Invoke(new JsonDataList(response.Text).ToCommands());
                return UniTask.CompletedTask;
            });
        }
    }
}