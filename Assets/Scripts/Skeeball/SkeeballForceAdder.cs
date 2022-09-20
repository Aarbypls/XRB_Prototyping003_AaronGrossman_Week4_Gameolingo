using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Skeeball
{
    public class SkeeballForceAdder : MonoBehaviour
    {
        [SerializeField] private XRBaseControllerInteractor _controller;

        void Start()
        {
            _controller.selectExited.AddListener(SelectExited);

        }

        private void SelectExited(SelectExitEventArgs arg0)
        {
            if (arg0.interactableObject.transform.gameObject.TryGetComponent(out SkeeballBall ball))
            {
                ball.AddForceOnThrow();
            }
        }
    }
}
