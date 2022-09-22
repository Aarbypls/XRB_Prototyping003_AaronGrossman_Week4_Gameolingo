using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Skeeball
{
    public class SkeeballBall : MonoBehaviour
    {
        private Rigidbody _myRigidbody;
        
        private void Start()
        {
            _myRigidbody = GetComponent<Rigidbody>();
            _myRigidbody.maxAngularVelocity = Mathf.Infinity;
        }

        private void FixedUpdate()
        {
            _myRigidbody.WakeUp();
        }

        public void AddForceOnThrow()
        {
            _myRigidbody.AddRelativeForce(Vector3.forward * 4f, ForceMode.Impulse);
            _myRigidbody.angularVelocity = _myRigidbody.angularVelocity * 5f;
        }
    }
}
