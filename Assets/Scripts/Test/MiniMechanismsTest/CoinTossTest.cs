using Runtime.GameBase;
using UnityEngine;
using UnityEngine.UI;

namespace Test.MiniMechanismsTest
{
    public class CoinTossTest : MonoBehaviour
    {
        public Vector3 startPos;
        public Vector3 force;
        public Vector3 torque;

        public Transform coins;
        public GameObject coinPrefab;
        
        public Button btnToss;
        public Button btnClear;
        
        public void Awake()
        {
            btnToss.onClick.AddListener(TossCoinRandomly);
            btnClear.onClick.AddListener(ClearExistingCoins);
        }

        private void TossCoinRandomly()
        {
            var coin = Instantiate(coinPrefab, coins).GetComponent<CoinBehaviour>();
            coin.Toss(new TossData(startPos, force, torque));
        }

        private void ClearExistingCoins()
        {
            foreach (var coin in coins.GetComponentsInChildren<CoinBehaviour>())
            {
                Destroy(coin.gameObject);
            }
        }
    }
}