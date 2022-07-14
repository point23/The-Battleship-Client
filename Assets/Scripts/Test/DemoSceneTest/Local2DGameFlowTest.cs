using Runtime.Games;
using UnityEngine;
using UnityEngine.UI;

namespace Test.DemoSceneTest
{
    public class Local2DGameFlowTest : MonoBehaviour
    {
        public Button btnEnter;
        public Local2DGameDriver gameDriver;

        public void Start()
        {
            btnEnter.onClick.AddListener(EnterGame);
        }

        private void EnterGame()
        {
            btnEnter.gameObject.SetActive(false);
            gameDriver.EnterGame();
        }
    }
}