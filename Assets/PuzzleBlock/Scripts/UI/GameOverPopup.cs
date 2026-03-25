using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PuzzleBlock.UI
{
    /// <summary>
    /// Displays a popup UI when the game is over and provides functionality to restart the current scene.
    /// </summary>
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("UI Button that will trigger the restart game event")]
        private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        /// <summary>
        /// Activates the Game Over UI popup.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void RestartGame()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}