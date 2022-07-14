using System;
using Cysharp.Threading.Tasks;
using Runtime.Infrastructures.Helper;
using UnityEngine;

namespace Runtime.GameBase
{
    public class CoinBehaviour : MonoBehaviour
    {
        private Rigidbody Rigidbody => GetComponent<Rigidbody>();

        public void Awake()
        {
            Rigidbody.useGravity = false;
        }

        public async void TossAsync(TossData data)
        {
            Rigidbody.useGravity = true;
            transform.position += data.startPosDelta;
            Rigidbody.AddForce(data.force);
            Rigidbody.AddTorque(data.torque);
        }
    }
    
    public class TossData
    {
        public Vector3 startPosDelta;
        public Vector3 force;
        public Vector3 torque;

        public TossData(Vector3 startPosDelta, Vector3 force, Vector3 torque)
        {
            this.startPosDelta = startPosDelta;
            this.force = force;
            this.torque = torque;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}