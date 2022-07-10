using Runtime.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestBasicGameFlow2DRemote : MonoBehaviour
    {
        public GameManager gameManager;
        public Button btnInit;

        public void Awake()
        {
            btnInit.onClick.AddListener(gameManager.Enter);
        }
    }
}