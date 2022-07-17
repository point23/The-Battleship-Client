using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.GameBase;
using Runtime.Utilities;
using ThirdParty.SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Common.Responders
{
    public class FleetsHandler : MonoBehaviour, ICommandResponder
    {
        public Button btnReady;
        public Button btnRandomDeploy;
    
        public PolyominoesHandler userPolyominosHandler;

        public void Awake()
        {
            btnRandomDeploy.onClick.AddListener(RandomDeploy);
        }
    
        public UniTask Run(string action, JSONNode data)
        {
            switch (action)
            {
                case "Render":
                    Render(data);
                    break;
            }
            
            return UniTask.CompletedTask;
        }
    
        private void Render(JSONNode data)
        {
            RandomDeploy();
        }

        private void RandomDeploy()
        {
            var rows = userPolyominosHandler.board.rows;
            var cols = userPolyominosHandler.board.cols;
            
            foreach (var polyomino in userPolyominosHandler.polyominos)
            {
                while (!polyomino.IsGridsValid)
                {
                    var delta = Coord.Random(rows, cols).CalculateDeltaInt(polyomino.TopLeft);
                    polyomino.OnDragged(delta);
                }
                polyomino.RenderRelocation();
            } 
        }
    }
}

