using Cysharp.Threading.Tasks;
using Runtime.Infrastructures.JSON;

namespace Runtime.Common
{
    public interface ICommandResponder
    {
        public UniTask Run(JsonData data);
    }
}