using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Description of passage between two rooms
    /// </summary>
    public interface IPassage : IIdentifiable
    {
        /// <summary>
        /// Get one end of the passage
        /// </summary>
        /// <param name="game">game where to look</param>
        /// <returns>room</returns>
        IRoom OneEnd(Game game);

        /// <summary>
        /// Get second end of the passage
        /// </summary>
        /// <param name="game">game where to look</param>
        /// <returns>room</returns>
        IRoom SecondEnd(Game game);

        /// <summary>
        /// Tell the passage to be used in specified game
        /// </summary>
        /// <param name="game">game to be used in</param>
        void Use(Game game);
    }
}
