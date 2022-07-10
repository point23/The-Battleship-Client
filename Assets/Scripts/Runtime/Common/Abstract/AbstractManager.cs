using Runtime.Common.Responders;
using UnityEngine;

namespace Runtime.Common.Abstract
{
    public abstract class AbstractManager : MonoBehaviour
    {
        protected DataSource dataSource;
        protected CommandHub commandHub;

        protected void Init()
        {
            dataSource = new DataSource();
            commandHub = new CommandHub();
            PostInit();
        }

        protected virtual void PostInit() { }
    }
}