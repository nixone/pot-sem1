using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class GameResult
    {
        public int Score { get; set; }
        
        [Key]
        public String PlayerName { get; set; }
        
        public GameResult()
        {
            // For entity framework
        }

        public GameResult(String playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }
}
