using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Describes a room in a game
    /// </summary>
    public interface IRoom : IIdentifiable
    {
        /// <summary>
        /// Tells the room that you walked to it from some passage
        /// </summary>
        /// <param name="game">game</param>
        /// <param name="throughPassage">passage used</param>
        /// <param name="fromRoom">room that player came from</param>
        void WalkIn(Game game, IPassage throughPassage, IRoom fromRoom);

        /// <summary>
        /// Get items currently in the room
        /// </summary>
        /// <param name="game">game</param>
        /// <returns>set of items</returns>
        ISet<IItem> GetItems(Game game);
    }
}
