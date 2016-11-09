using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public interface IRoom : IIdentifiable
    {
        void WalkIn(Game game, IPassage throughPassage, IRoom fromRoom);

        ISet<IItem> GetItems(Game game);
    }
}
