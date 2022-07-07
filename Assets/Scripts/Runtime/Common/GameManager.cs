using UnityEngine;

namespace Runtime.Common
{
    public class GameManager : MonoBehaviour
    {
        public DataSourceHub dataSourceHub;
        public CommandHub commandHub;
        public GameSyncService syncService;
    }
}