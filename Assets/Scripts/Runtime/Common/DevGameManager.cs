using Cysharp.Threading.Tasks;
using Models;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Common
{
    public class DevGameManager
    {
        public async UniTask<string> Post(string uri, string field)
        {
            var request = UnityWebRequest.Post(uri, field);
            await request.SendWebRequest();
            var responseText = request.downloadHandler.text;
            return JsonUtility.ToJson(responseText);
        }
        
        public async UniTask<string> Get(string uri)
        {
            RestClient.Get<User>(uri + "/1").Then(firstUser => {
                EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(firstUser, true), "Ok");
            });
            
            var request = UnityWebRequest.Get(uri);
            await request.SendWebRequest();
            var responseText = request.downloadHandler.text;
            return responseText;
        }
    }
}
