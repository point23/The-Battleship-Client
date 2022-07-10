using System;
using Cysharp.Threading.Tasks;
using Proyecto26;

namespace Runtime.Common.Abstract
{
    public abstract class AbstractSyncService
    {
        public void PostRequest(RequestHelper request, Func<ResponseHelper, UniTask> handler)
        {
            RestClient.Post(request).Then(response =>
            {
                handler.Invoke(response);
            });
        }
        
        public void GetRequest(RequestHelper request, Func<ResponseHelper, UniTask> handler)
        {
            RestClient.Get(request).Then(response =>
            {
                handler.Invoke(response);
            });
        }
        
        public void PutRequest(RequestHelper request, Func<ResponseHelper, UniTask> handler)
        {
            RestClient.Put(request).Then(response =>
            {
                handler.Invoke(response);
            });
        }
        
        public void DeleteRequest(RequestHelper request, Func<ResponseHelper, UniTask> handler)
        {
            RestClient.Delete(request).Then(response =>
            {
                handler.Invoke(response);
            });
        }
    }
}