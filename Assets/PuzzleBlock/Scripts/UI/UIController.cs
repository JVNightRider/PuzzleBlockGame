using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleBlock.UI
{
    public class UIController : MonoBehaviour
    {
        public event Action OnMakeMovementButtonPressed;

        [Header("UI")]
        [SerializeField]
        private TMP_Text movesText;

        [SerializeField]
        private TMP_Text scoreText;

        [Header("Popups")]
        [SerializeField]
        private GameOverPopup gameOverPopup;

        [SerializeField, Header("Debug")]
        private Button makeMovementButton;

        private void Start()
        {
            makeMovementButton.onClick.AddListener(MakeMovementButtonPressed);
        }

        public void UpdateScoreAndMovements(int score, int movements)
        {
            movesText.text = movements.ToString();
            scoreText.text = score.ToString();
        }

        public void ShowGameOver()
        {
            gameOverPopup.Show();
        }

        private void MakeMovementButtonPressed()
        {
            OnMakeMovementButtonPressed?.Invoke();
        }
    }
}