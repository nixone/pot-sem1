using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Description of storage for storing and retrieving high scores
    /// </summary>
    public interface IHighScoreStorage
    {
        /// <summary>
        /// Get top ten results
        /// </summary>
        /// <returns>top ten game results</returns>
        IList<GameResult> GetTopTen();

        /// <summary>
        /// Save a result of the game
        /// </summary>
        /// <param name="result">game result</param>
        void SaveResult(GameResult result);
    }
}
