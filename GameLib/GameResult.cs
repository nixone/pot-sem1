using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Game result representation
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Reached score
        /// </summary>
        public int Score { get; set; }
        
        /// <summary>
        /// Name of the player who did play
        /// </summary>
        [Key]
        public String PlayerName { get; set; }

        /// <summary>
        /// Time when game was started
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Time when game was stopped
        /// </summary>
        public DateTime StopTime { get; set; }

        /// <summary>
        /// Machine name which finished the game
        /// </summary>
        public String MachineName { get; set; }
        
        /// <summary>
        /// Construtor not to be used in code, only for serialization purposes
        /// </summary>
        public GameResult()
        {
            // For entity framework
        }

        /// <summary>
        /// Constructor to be used in code.
        /// </summary>
        /// <param name="playerName">name of player</param>
        /// <param name="score">reached score</param>
        /// <param name="startTime">time when game started</param>
        /// <param name="stopTime">time when game stopped</param>
        /// <param name="machineName">machine name the game was finished</param>
        public GameResult(String playerName, int score, DateTime startTime, DateTime stopTime, String machineName)
        {
            PlayerName = playerName;
            Score = score;
            StartTime = startTime;
            StopTime = stopTime;
            MachineName = machineName;
        }
    }
}
