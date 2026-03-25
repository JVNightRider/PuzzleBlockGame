using UnityEngine;

namespace PuzzleBlock.Settings
{
    [CreateAssetMenu(fileName = "PuzzleBlock Settings", menuName = "PuzzleBlock/Create Settings", order = 0)]
    public class PuzzleBlockSettings : ScriptableObject
    {
        [Header("Player Settings")]
        [SerializeField, Tooltip("Number of movements that player can perform by game session")]
        private int playerMovements = 5;

        /// <summary>
        /// Gets the number of movements allowed for the player in a game session.
        /// </summary>
        public int PlayerMovements => playerMovements;
    }
}