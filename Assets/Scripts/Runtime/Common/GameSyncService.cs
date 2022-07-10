using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Models;
using Proyecto26;
using Runtime.Core;
using Runtime.Infrastructures.Helper;
using UnityEditor;
using UnityEngine;

namespace Runtime.Common
{
    public class GameSyncService
    {
        // POST
        public void EnterGameZone(string gameZoneId, Func<string, UniTask> thenFunc)
        {
            var uri = AppManager.instance.uriRootLocal + "zone_sessions/{" + gameZoneId + "}";
            var request = new RequestHelper() { Uri = uri };
            RestClient.Post(request).Then(response =>
            {
                thenFunc(response.Text);
            }).Done();
        }
    }
}