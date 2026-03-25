using UnityEngine;

namespace PuzzleBlock.Settings
{
    /// <summary>
    /// Provides configuration settings for the PuzzleBlock game, including player movement limits and timing options.
    /// </summary>
    [CreateAssetMenu(fileName = "PuzzleBlock Settings", menuName = "PuzzleBlock/Create Settings", order = 0)]
    public class PuzzleBlockSettings : ScriptableObject
    {
        [Header("Player Settings")]
        [SerializeField, Tooltip("Number of movements that player can perform by game session")]
        private int playerMovements = 5;

        [SerializeField, Tooltip("Time that brick removal will take before re configure bricks grid")]
        [Range(0.1f, 2f)]
        private float delayAfterMovement = 1;

        /// <summary>
        /// Gets the number of movements allowed for the player in a game session.
        /// </summary>
        public int PlayerMovements => playerMovements;

        /// <summary>
        /// Gets the delay time that game will wait after a movement
        /// </summary>
        public float DelayAfterMovement => delayAfterMovement;
    }
}