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
        public GameResult(String playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }
}
