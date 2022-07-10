using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Proyecto26;
using Runtime.Infrastructures.Helper;
using UnityEditor;
using UnityEngine.Device;

namespace Runtime.Common
{
    public class AppSyncService
    {
        public void CreateSession(string uri)
        {
            var request = new RequestHelper {Uri = uri,};
            RestClient.Post(request).Then(response =>
            {
                EditorUtility.DisplayDialog("response", response.Text, "Ok");
            });
        }
    }
}