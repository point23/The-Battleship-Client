using UnityEngine;

namespace Runtime.GameBase
{
    public class CoinBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        public void Toss(TossData data)
        {
            _rigidbody.useGravity = true;
            
            transform.position = data.startPos;
            _rigidbody.AddForce(data.force);
            _rigidbody.AddTorque(data.torque);
        }
        
    }
    
    public class TossData
    {
        public Vector3 startPos;
        public Vector3 force;
        public Vector3 torque;

        public TossData(Vector3 startPos, Vector3 force, Vector3 torque)
        {
            this.startPos = startPos;
            this.force = force;
            this.torque = torque;
        }
    }
}