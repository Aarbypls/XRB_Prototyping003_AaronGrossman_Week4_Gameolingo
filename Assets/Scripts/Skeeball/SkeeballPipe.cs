using UnityEngine;

namespace Skeeball
{
    public class SkeeballPipe : MonoBehaviour
    {
        [SerializeField] private int _scoreValue;
        [SerializeField] private ScoreManager _scoreManager;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<SkeeballBall>())
            {
                return;
            }
            
            _scoreManager.AddToScore(_scoreValue);
            Destroy(other.gameObject);
        }
    }
}
