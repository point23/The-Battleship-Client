using Cysharp.Threading.Tasks;
using Runtime.Infrastructures.JSON;

namespace Runtime.Common.Interface
{
    public interface ICommandResponder
    {
        public UniTask Run(string action, JsonData data);
    }
}