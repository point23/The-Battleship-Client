using Runtime.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Common.Responders
{
    public class LoginDialog : MonoBehaviour
    {
        private VisualElement _root;
        
        public void Awake()
        {
            _root = GetComponentInChildren<UIDocument>().rootVisualElement;
            _root.Q<Button>("guestLogin-button").clicked += AppManager.instance.Login;
        }
    }
}