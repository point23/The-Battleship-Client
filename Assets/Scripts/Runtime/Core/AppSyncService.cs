using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto26;
using Runtime.Common;
using Runtime.Common.Abstract;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Core
{
    public class AppSyncService : AbstractSyncService
    {
        public readonly UnityEvent<CommandList> onInitApp = new UnityEvent<CommandList>();
        public readonly UnityEvent<CommandList> onLogin = new UnityEvent<CommandList>();
        public readonly UnityEvent<CommandList> onEnterGame = new UnityEvent<CommandList>();

        public void InitService(string uri)
        {
            var request = new RequestHelper {Uri = uri};
            GetRequest(request, response =>
            {
                var commands = (JSONNode) JSON.Parse(response.Text);
                var commandList = new CommandList(commands.AsArray);
                onInitApp.Invoke(commandList);
                return UniTask.CompletedTask;
            });
        }
        
        public void LoginService(string uri)
        {
            var request = new RequestHelper {Uri = uri};
            PostRequest(request, response =>
            {
                var commands = (JSONNode) JSON.Parse(response.Text);
                var commandList = new CommandList(commands.AsArray);
                onInitApp.Invoke(commandList);
                return UniTask.CompletedTask;
            });
        }
    }
}