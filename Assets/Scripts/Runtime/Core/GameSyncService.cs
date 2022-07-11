using Cysharp.Threading.Tasks;
using Proyecto26;
using Runtime.Common;
using Runtime.Common.Abstract;
using ThirdParty.SimpleJSON;
using UnityEngine.Events;

namespace Runtime.Core
{
    public class GameSyncService : AbstractSyncService
    {
        public readonly UnityEvent<CommandList> onEnterGame = new UnityEvent<CommandList>();
        
        public void EnterGameService(string uri)
        {
            var request = new RequestHelper {Uri = uri};
            PostRequest(request, response =>
            {
                var commands = (JSONNode) JSON.Parse(response.Text);
                var commandList = new CommandList(commands.AsArray);
                onEnterGame.Invoke(commandList);
                return UniTask.CompletedTask;
            });
        }
    }
}