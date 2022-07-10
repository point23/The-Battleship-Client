using Cysharp.Threading.Tasks;
using Runtime.Infrastructures.Helper;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Runtime.Common
{
    public class GameManager : MonoBehaviour
    {
        public string gameZoneId;
        public CommandHub commandHub;
        private GameSyncService _syncService;

        public void Awake()
        {
            _syncService = new GameSyncService();
        }

        public void Enter()
        {
            _syncService.EnterGameZone(gameZoneId, response =>
            {
                EditorUtility.DisplayDialog("response", response, "Ok");
                return UniTask.CompletedTask;
            });
        }

    }
}