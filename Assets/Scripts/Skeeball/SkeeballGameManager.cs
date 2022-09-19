using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Skeeball
{
    public class QuizQuestionAndAnswer
    {
        public string Question { get; set; }
        public string CorrectAnswer  { get; set; }
        public string FakeAnswer1 { get; set; }
        public string FakeAnswer2 { get; set; }
    }
    
    public class SkeeballGameManager : MonoBehaviour
    {
        public List<QuizQuestionAndAnswer> questionsAndAnswers = null;
        public Transform skeeballSpawnPoint;
        public QuizQuestionAndAnswer currentQuestionAndAnswer;
        public int quizQuestionsLeft;

        [SerializeField] private GameObject _skeeball;
        [SerializeField] private List<int> numbersForSettingQuizAnswers = new List<int>() { 0, 1, 2 };
        [SerializeField] private ScoreManager _scoreManager;

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
                        Question = "Terry walks his _____ every morning.", CorrectAnswer = "Dog", FakeAnswer1 = "Carrot",
                        FakeAnswer2 = "House"
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "Eliza _____ her mother every day on the phone.", CorrectAnswer = "Calls",
                        FakeAnswer1 = "Walks", FakeAnswer2 = "Learns"
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "Can you pick me up milk at the grocery _____?", CorrectAnswer = "Store",
                        FakeAnswer1 = "Mail", FakeAnswer2 = "City"
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "_____ is Han's favorite season of the year.", CorrectAnswer = "Winter", FakeAnswer1 = "Computer",
                        FakeAnswer2 = "Backpack"
                    }
                },
                {
                    new QuizQuestionAndAnswer
                    {
                        Question = "Steve and Michelle exchanged business _____ at the end of the meeting.",
                        CorrectAnswer = "Cards", FakeAnswer1 = "Jobs", FakeAnswer2 = "Glasses"
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

            _quizScreen.SetActive(true);
            _scoreScreen.SetActive(true);
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

        public void SpawnSkeeball()
        {
            Instantiate(_skeeball, skeeballSpawnPoint.position, Quaternion.identity);
        }

        public void SubmitAnswer(int answerNumber)
        {
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
            SpawnSkeeball();
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
            
            _quizScreen.SetActive(true);
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
        }

        public void RestartGame()
        {
            _scoreManager.ResetScore();
            ResetNumbersForQuizAnswers();
            InitializeQuizQuestions();
            InitializeUIScreens();
        }
    }
}
