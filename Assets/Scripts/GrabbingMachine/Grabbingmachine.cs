using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace GrabbingMachine
{
    public class QuizQuestionAndAnswer
    {
        public string Question { get; set; }
        public string CorrectAnswer  { get; set; }
        public string FakeAnswer1 { get; set; }
        public string FakeAnswer2 { get; set; }

        public int prefabindex { get; set; }
    }
    
    public class Grabbingmachine : MonoBehaviour
    {
        public List<QuizQuestionAndAnswer> questionsAndAnswers = null;
        public Transform animalBallSpawnPoint;
        public Transform clawSpawnPoint;
        public QuizQuestionAndAnswer currentQuestionAndAnswer;
        public int quizQuestionsLeft;
        private GameObject gm;
        public float timeRemaining = 10;
        public bool timerIsRunning = false;
        public TMP_Text timeText;

        [SerializeField] private GameObject _animalBall;
        [SerializeField] private List<GameObject> _animalBalls;
        [SerializeField] private List<int> numbersForSettingQuizAnswers = new List<int>() { 0, 1, 2 };
        [SerializeField] private Animator _anim;
        
        //[SerializeField] private ScoreManager _scoreManager;

        [Header("UI Screens")] 
        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _instructionsScreen;
        [SerializeField] private GameObject _scoreScreen;
        [SerializeField] private GameObject _quizScreen;
        [SerializeField] private GameObject _correctAnswerScreen;
        [SerializeField] private GameObject _incorrectAnswerScreen;
        [SerializeField] private TMP_Text _incorrectAnswerCorrectAnswerText;
        [SerializeField] private GameObject _gameFinishedScreen;

        [Header("Quiz Text And Answers")] 
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private List<QuizAnswer> _quizAnswers;

        void Start()
        {
            InitializeQuizQuestions();
            InitializeUIScreens();
            
        }

        private void InitializeUIScreens()
        {
            // Safety check
            _scoreScreen.SetActive(false);
            _instructionsScreen.SetActive(false);
            _quizScreen.SetActive(false);
            _correctAnswerScreen.SetActive(false);
            _incorrectAnswerScreen.SetActive(false);
            _gameFinishedScreen.SetActive(false);
            
            // Only one active at game start should be the start screen
            _startScreen.SetActive(true);
        }
        
        private void InitializeQuizQuestions()
        {
            questionsAndAnswers = new List<QuizQuestionAndAnswer>()
            {
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "", CorrectAnswer = "Dog", FakeAnswer1 = "Parrot",
                        FakeAnswer2 = "Mouse", prefabindex = 0
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "", CorrectAnswer = "Bird",
                        FakeAnswer1 = "Cat", FakeAnswer2 = "Wolf", prefabindex = 1
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "", CorrectAnswer = "Fish",
                        FakeAnswer1 = "Pig", FakeAnswer2 = "Snake", prefabindex = 2
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "", CorrectAnswer = "Elephant", FakeAnswer1 = "Turtle",
                        FakeAnswer2 = "Dolphin", prefabindex = 3
                    }
                }
            };

            quizQuestionsLeft = questionsAndAnswers.Count;
        }

        public void StartGame()
        {
            _startScreen.SetActive(false);

            SetQuizQuestion();
            SetQuizAnswers();
            _anim.SetBool("getBall",true);
            gm = Instantiate(_animalBalls[currentQuestionAndAnswer.prefabindex], clawSpawnPoint.position, Quaternion.identity);
            gm.transform.parent = clawSpawnPoint;
            gm.GetComponent<Rigidbody>().isKinematic = true;
            gm.GetComponent<AudioSource>().playOnAwake = true;
            _quizScreen.SetActive(true);
            timerIsRunning = true;
            //_scoreScreen.SetActive(true);
            
            
        }

        private void SetQuizAnswers()
        {
            // Very dumb way to write this logic
            // Should be a For or Foreach loop, but we're fine hard coding 3 answers for now
            int correctAnswerNumber = Random.Range(0, 3);
            numbersForSettingQuizAnswers.RemoveAt(correctAnswerNumber);
            _quizAnswers[correctAnswerNumber]._answerText.SetText(currentQuestionAndAnswer.CorrectAnswer);

            // This is only SEMI-randomized for the wrong answers
            // It would have to be changed in an MVP or full product
            // The wrong answers will always be in the same order, but - easy fix
            _quizAnswers[numbersForSettingQuizAnswers[0]]._answerText.SetText((currentQuestionAndAnswer.FakeAnswer1));
            _quizAnswers[numbersForSettingQuizAnswers[1]]._answerText.SetText((currentQuestionAndAnswer.FakeAnswer2));
        }

        private void SetQuizQuestion()
        {
            // Set the question text, and remove the question from the list of possible ones for the future
            // Weird logic to avoid issue with Random.Range having 0 min and max
            int questionNumber = quizQuestionsLeft == 1 ? 0 : Random.Range(0, quizQuestionsLeft);
            quizQuestionsLeft--;
            currentQuestionAndAnswer = questionsAndAnswers[questionNumber];
            questionsAndAnswers.RemoveAt(questionNumber);
            _questionText.SetText(currentQuestionAndAnswer.Question);
        }

        public void ShowInstructions()
        {
            _startScreen.SetActive(false);
            _instructionsScreen.SetActive(true);
        }

        public void ShowStartScreen()
        {
            _instructionsScreen.SetActive(false);
            _startScreen.SetActive(true);
        }

        public void SpawnAnimalBall()
        {
            Instantiate(_animalBalls[currentQuestionAndAnswer.prefabindex], animalBallSpawnPoint.position, Quaternion.identity);
            if (quizQuestionsLeft > 0)
            {
                ProceedWithGame();
            }
            else
            {
                HandleGameFinishedState();
            }
        }

        public void SubmitAnswer(int answerNumber)
        {
            _anim.SetBool("getBall",false);
            Destroy(gm);
            if (_quizAnswers[answerNumber]._answerText.text == currentQuestionAndAnswer.CorrectAnswer)
            {
                HandleCorrectAnswer();
            }
            else
            {
                HandleIncorrectAnswer();
            }
        }

        private void HandleCorrectAnswer()
        {
            // TO-DO: Probably need to spend more time here making this good
            // - sound effects
            // - visual effects
            // - timer for the next screen
            // - etc.

            _quizScreen.SetActive(false);
            _correctAnswerScreen.SetActive(true);
            SpawnAnimalBall();
            
        }

        private void HandleIncorrectAnswer()
        {
            // TO-DO: Probably need to spend more time here making this good
            // - sound effects
            // - visual effects
            // - timer for the next screen
            // - etc.

            if (quizQuestionsLeft > 0)
            {
                ShowIncorrectAnswerScreenAndCorrectAnswer();
                ProceedWithGame(5f);
            }
            else
            {
                // Game finished
                ShowIncorrectAnswerScreenAndCorrectAnswer();
                Invoke(nameof(HandleGameFinishedState),5f);
            }

        }

        private void ShowIncorrectAnswerScreenAndCorrectAnswer()
        {
            _quizScreen.SetActive(false);
            _incorrectAnswerCorrectAnswerText.text =
                "The correct answer was \n\"" + currentQuestionAndAnswer.CorrectAnswer + "\"";
            _incorrectAnswerScreen.SetActive(true);
        }

        public void ProceedWithGame(float timer = 2f)
        {
            Invoke(nameof(Proceed),timer);
        }

        private void Proceed()
        {
            _correctAnswerScreen.SetActive(false);
            _incorrectAnswerScreen.SetActive(false);

            ResetNumbersForQuizAnswers();
            SetQuizQuestion();
            SetQuizAnswers();
            
            _anim.SetBool("getBall",true);
            gm = Instantiate(_animalBalls[currentQuestionAndAnswer.prefabindex], clawSpawnPoint.position, Quaternion.identity);
            gm.transform.parent = clawSpawnPoint;
            gm.GetComponent<Rigidbody>().isKinematic = true;
            gm.GetComponent<AudioSource>().playOnAwake = true;
            
            _quizScreen.SetActive(true);
            timeRemaining = 10;
            timerIsRunning = true;
            
            
        }

        private void ResetNumbersForQuizAnswers()
        {
            numbersForSettingQuizAnswers = new List<int>() { 0, 1, 2 };
        }

        public void HandleGameFinishedState()
        {
            _correctAnswerScreen.SetActive(false);
            _incorrectAnswerScreen.SetActive(false);
            
            ResetNumbersForQuizAnswers();
            InitializeQuizQuestions();

            _gameFinishedScreen.SetActive(true);
            _anim.SetBool("getBall",false);
        }

        public void RestartGame()
        {
            ResetNumbersForQuizAnswers();
            InitializeQuizQuestions();
            InitializeUIScreens();
        }
        

        void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    timeText.text = "Time has run out!";
                    timeRemaining = 0;
                    timerIsRunning = false;
                }
            }
        }
        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timeText.text = string.Format("{1:00}", seconds);
            timeText.text += " s left";
        }
    }
}

