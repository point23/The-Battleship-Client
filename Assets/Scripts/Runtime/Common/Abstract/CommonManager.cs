using Runtime.Common.Responders;
using UnityEngine;

namespace Runtime.Common.Abstract
{
    public abstract class CommonManager : MonoBehaviour
    {
        public DataSource dataSource;
        public CommandHub commandHub;

        protected void Init()
        {
            dataSource = new DataSource();
            commandHub = new CommandHub();
            commandHub.Register("DataSource", dataSource);
            PostInit();
        }
        
        public void RegisterNewGameObject(string id, GameObject go)
        {
            dataSource.AddNewGameObject(id, go);
        }

        public GameObject TryGetGameObject(string id)
        {
            return dataSource.TryGetGameObject(id);
        }

        protected virtual void PostInit() { }
    }
}