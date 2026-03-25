using TMPro;
using UnityEngine;

namespace PuzzleBlock.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private TMP_Text movesText;

        [SerializeField]
        private TMP_Text scoreText;

        [Header("Popups")]
        [SerializeField]
        private GameOverPopup gameOverPopup;

        private void Start()
        {
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
    }
}