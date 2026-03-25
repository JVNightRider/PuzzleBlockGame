using TMPro;
using UnityEngine;

namespace PuzzleBlock.UI
{
    /// <summary>
    /// Manages user interface elements and popups for the game.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        [Tooltip("UI Label where remaining user movements will be visible")]
        private TMP_Text movesText;

        [SerializeField]
        [Tooltip("UI Label where user score will be visible")]
        private TMP_Text scoreText;

        [Header("Popups")]
        [SerializeField]
        [Tooltip("Reference to the Game Over popup UI element.")]
        private GameOverPopup gameOverPopup;

        /// <summary>
        /// Updates the displayed score value.
        /// </summary>
        /// <param name="score">The new score to display.</param>
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

        /// <summary>
        /// Updates the displayed number of movements.
        /// </summary>
        /// <param name="movements">The number of movements to display.</param>
        public void UpdateMovements(int movements)
        {
            movesText.text = movements.ToString();
        }

        /// <summary>
        /// Displays the game over popup.
        /// </summary>
        public void ShowGameOver()
        {
            gameOverPopup.Show();
        }
    }
}