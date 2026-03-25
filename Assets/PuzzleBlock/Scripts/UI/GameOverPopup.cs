using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PuzzleBlock.UI
{
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField]
        private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

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