using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public interface IPassage : IIdentifiable
    {
        IRoom OneEnd(Game game);

        IRoom SecondEnd(Game game);

        void Use(Game game);
    }
}
