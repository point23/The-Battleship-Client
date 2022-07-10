using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.JSON;

namespace Runtime.Common.Responders
{
    public class DialogBuilder : ICommandResponder 
    {
        public UniTask Run(string action, JsonData data)
        {
            return UniTask.CompletedTask;
        }

        public void I()
        {
            
        }
    }
}