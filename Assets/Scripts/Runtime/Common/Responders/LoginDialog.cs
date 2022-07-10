using Runtime.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Common.Responders
{
    public class LoginDialog : MonoBehaviour
    {
        public Button btnGuestLogin;

        public void Awake()
        {
            btnGuestLogin.onClick.AddListener(AppManager.instance.Login);
        }
    }
}