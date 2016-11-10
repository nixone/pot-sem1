using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class BstrnkGame : Game
    {
        public static BstrnkGame createNew()
        {
            BstrnkGame game = new BstrnkGame();

            game.Add(new Room("bonaparte", "Your home"));

            game.Add(new Room("sumracna", "Your work"));

            game.Add(new Room("vahostav", "Vahostav HQ", "contracts"));
            game.Add(new Room("court", "Court of Slovak Republic", "court-materials"));
            game.Add(new Room("naka", "National Criminal Agency", "recordings"));

            game.Add(new Item("contracts", "Compromising contracts with self employed contractors", "vahostav", 1000000));
            game.Add(new Item("court-materials", "Compromising court materials about Bstrnk", "court", 500000));
            game.Add(new Item("recordings", "Recordings from famous Vazovova street", "naka", 1500000));

            game.Add(new Passage("bonaparte-sumracna", 10, "bonaparte", "sumracna"));
            game.Add(new Passage("vahostav-sumracna", 40, "vahostav", "sumracna"));
            game.Add(new Passage("sumracna-naka", 15, "sumracna", "naka"));
            game.Add(new Passage("naka-vahostav", 20, "naka", "vahostav"));
            game.Add(new Passage("sumracna-court", 13, "sumracna", "court"));
            game.Add(new Passage("naka-court", 13, "naka", "court"));

            game.Add(new ScartingRoom());
            game.Add(new Passage("bonaparte-scarting-room", 0, "bonaparte", ScartingRoom.ID));
            game.Add(new ScartingMachine());

            game.Start("bonaparte");

            return game;
        }

        public int _eurosSpent = 0;

        private int GetTotalCompromisingMaterialsCost()
        {
            int missingCompromisingMaterialsCost = 0;
            foreach (Item item in GetAll<Item>())
            {
                if (Get<Room>(item._roomIdToBeIn)._hasItem)
                {
                    missingCompromisingMaterialsCost += item._compromisingEuroCost;
                }
            }
            return missingCompromisingMaterialsCost;
        }

        public override string ToString()
        {
            int missingCompromisingMaterialsCost = GetTotalCompromisingMaterialsCost();
            if (missingCompromisingMaterialsCost == 0)
            {
                return "You are now in [" + GetCurrentRoom() + "] with no compromising materials left, you spent "+_eurosSpent+" euros";
            }
            else
            {
                return "You are now in [" + GetCurrentRoom() + "] with "+missingCompromisingMaterialsCost+" euros of cost materials left, you spent " + _eurosSpent + " euros";
            }
        }

        public override int GetScore()
        {
            return GetTotalCompromisingMaterialsCost() + _eurosSpent;
        }

        public override string GetTitle()
        {
            return "Game of BSTRNK";
        }

        public override string GetStory()
        {
            return "BSTRNK Story";
        }

        public override string GetObjective()
        {
            return "BSTRNK Objective";
        }
    }
}
