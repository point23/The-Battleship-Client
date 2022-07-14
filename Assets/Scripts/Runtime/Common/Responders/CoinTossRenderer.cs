using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.GameBase;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace Runtime.Common.Responders
{
    public class CoinTossRenderer : MonoBehaviour, ICommandResponder
    {
        public GameObject coinTossPrefab;
        public GameObject coinPrefab;
        
        public GameObject view;
        private List<TossData> _headTossData;
        private List<TossData> _tailTossData;
        
        public void Awake()
        {
            view.SetActive(false);
            
            _headTossData = new List<TossData>() { new TossData
                (new Vector3(0, 5, 0),
                new Vector3(0, 600, 0),
                new Vector3(100, 0, 100))
            };
            _tailTossData = new List<TossData>() { new TossData
                (new Vector3(0, 5, 0),
                    new Vector3(0, 300,0),
                    new Vector3(100, 0, 100))
            };
        }
        
        public UniTask Run(string action, JSONNode data)
        {
            DebugPG13.Log(new Dictionary<object, object>
            {
                {"action", action},
                {"data", data}
            });
            
            switch (action)
            {
                case "Toss": 
                    Toss(data);
                    break;
            }
            
            return UniTask.CompletedTask;
        }

        private async void Toss(JSONNode data)
        {
            view.SetActive(true);

            var tossData = data["result"].Value == "head" ? PickOne(_headTossData) : PickOne(_tailTossData);
            var coinToss = Instantiate(coinTossPrefab);
            var coin = Instantiate(coinPrefab, coinToss.transform).GetComponent<CoinBehaviour>();
            
            coin.TossAsync(tossData);
            
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            
            Destroy(coinToss);
            Destroy(coin);
            
            view.SetActive(false);
        }

        private TossData PickOne(List<TossData> list)
        {
            var count = list.Count;
            var index = Random.Range(0, count);
            return list[index];
        }
    }
}