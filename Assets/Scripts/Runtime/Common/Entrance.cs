using Runtime.Core;
using ThirdParty.SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Common
{
    public class Entrance : MonoBehaviour
    {
        public Button btnEnter;
        public TextMeshProUGUI textMesh;
        private string _gameUri;

        public void Awake()
        {
            btnEnter.onClick.AddListener(EnterGame);
        }

        public void Init(JSONNode data)
        {
            textMesh.text = data["name"];
            _gameUri = data["links"].AsArray[0]["href"];
        }

        private void EnterGame()
        {
            AppManager.instance.EnterGame(_gameUri);
        }
    }
}