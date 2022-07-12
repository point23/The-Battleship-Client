using Runtime.Common.Responders;
using UnityEngine;

namespace Runtime.Common.Abstract
{
    public abstract class CommonManager : MonoBehaviour
    {
        protected DataSource dataSource;
        protected CommandHub commandHub;

        protected void Init()
        {
            dataSource = new DataSource();
            commandHub = new CommandHub();
            commandHub.Register("DataSource", dataSource);
            PostInit();
        }

        protected virtual void PostInit() { }
    }
}