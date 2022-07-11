using Cysharp.Threading.Tasks;
using ThirdParty.SimpleJSON;

namespace Runtime.Common.Interface
{
    public interface ICommandResponder
    {
        public UniTask Run(string action, JSONNode data);
    }
}