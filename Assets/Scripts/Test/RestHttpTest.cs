using Runtime.Common;
using Runtime.Infrastructures.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class RestHttpTest : MonoBehaviour
    {
        public string uri = "http://localhost:8080/";
        public Button btnGet;
        public Button btnPost;

        public TextMeshProUGUI textMesh;
        private DevGameManager _manager;

        public void Awake()
        {
            btnGet.onClick.AddListener(GetRequest);
            btnPost.onClick.AddListener(PostRequest);
            _manager = new DevGameManager();
        }

        private async void GetRequest()
        {
            DebugPG13.Log("----> GET request", "");
            var response = await _manager.Get(uri);
            RenderResponse(response);
        }
        
        private async void PostRequest()
        {
            var field = "";
            DebugPG13.Log("----> POST request", field);
            var response = await _manager.Post(uri, field);
            RenderResponse(response);
        }

        private void RenderResponse(string response)
        {
            textMesh.text = response;
        }

    }
}