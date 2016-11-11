using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Describes the item to be used in a game
    /// </summary>
    public interface IItem : IIdentifiable
    {
        /// <summary>
        /// Tell the item to be used in a game
        /// </summary>
        /// <param name="game">game to be used in</param>
        void Use(Game game);
    }
}
