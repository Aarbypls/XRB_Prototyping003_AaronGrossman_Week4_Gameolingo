using UnityEngine;

namespace Skeeball
{
    public class SkeeballPipe : MonoBehaviour
    {
        [SerializeField] private int _scoreValue;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private SkeeballGameManager _skeeballGameManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<SkeeballBall>())
            {
                return;
            }
            
            _scoreManager.AddToScore(_scoreValue);

            if (_skeeballGameManager.quizQuestionsLeft > 0)
            {
                Destroy(other.gameObject);
                _skeeballGameManager.ProceedWithGame();
            }
            else
            {
                // Game finished
                Destroy(other.gameObject);
                _skeeballGameManager.HandleGameFinishedState();
            }
            

        }
    }
}
