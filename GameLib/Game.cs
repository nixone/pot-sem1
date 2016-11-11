using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GameLib
{
    /// <summary>
    /// Abstract base for a game with score and some objects with identifiers.
    /// </summary>
    public abstract class Game
    {
        public SerializableHashSet<IIdentifiable> _identifiables = new SerializableHashSet<IIdentifiable>();

        public String _currentRoomId;

        public bool _isRunning = false;

        /// <summary>
        /// Is the game still running
        /// </summary>
        /// <returns>whether the game is running or not</returns>
        public bool IsRunning()
        {
            return _isRunning;
        }

        /// <summary>
        /// Adds a new object to the game
        /// </summary>
        /// <param name="identifiable">object</param>
        public void Add(IIdentifiable identifiable)
        {
            _identifiables.Add(identifiable);
        }

        /// <summary>
        /// Returns the object from game
        /// </summary>
        /// <param name="id">id of the object</param>
        /// <returns></returns>
        public IIdentifiable Get(String id)
        {
            foreach (IIdentifiable identifiable in _identifiables)
            {
                if (identifiable.GetId().Equals(id))
                {
                    return identifiable;
                }
            }
            throw new IndexOutOfRangeException("id not found");
        }

        /// <summary>
        /// Same as Get(String), but checks and returns the correct type
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="id">id</param>
        /// <returns></returns>
        public T Get<T>(String id)
        {
            IIdentifiable identifiable = Get(id);
            if (!(identifiable is T))
            {
                throw new IndexOutOfRangeException(id + " is not of correct type, but it is "+identifiable.GetType());
            }
            return (T)identifiable;
        }

        /// <summary>
        /// Returns all the identifiables of the specified type
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns>objects with type</returns>
        public ISet<T> GetAll<T>()
        {
            HashSet<T> result = new HashSet<T>();
            foreach (IIdentifiable identifiable in _identifiables)
            {
                if (identifiable is T)
                {
                    result.Add((T)identifiable);
                }
            }
            return result;
        }

        /// <summary>
        /// Starts the game at a specified room
        /// </summary>
        /// <param name="startingRoomId">room id</param>
        public void Start(String startingRoomId)
        {
            Get<IRoom>(startingRoomId);
            _currentRoomId = startingRoomId;
            _isRunning = true;
        }

        /// <summary>
        /// Marks the game as finished
        /// </summary>
        public void Finish()
        {
            _isRunning = false;
        }

        /// <summary>
        /// Returns a room, which the player is in right now
        /// </summary>
        /// <returns></returns>
        public IRoom GetCurrentRoom()
        {
            return Get<IRoom>(_currentRoomId);
        }

        /// <summary>
        /// Tries to go to a room with specified id
        /// </summary>
        /// <param name="id">room id</param>
        public void GoInto(String id)
        {
            IRoom current = GetCurrentRoom();
            IRoom next = Get<IRoom>(id);
            ISet<IRoom> validRooms = GetRoomsToWalkTo(current);
            
            if (!GetRoomsToWalkTo(current).Contains(next))
            {
                throw new ArgumentException("you dont have a passage to go this direction");
            }

            IPassage passage = GetPassageBetween(current, next);
            passage.Use(this);
            next.WalkIn(this, passage, current);

            _currentRoomId = id;
        }

        /// <summary>
        /// Get a passage between two rooms
        /// </summary>
        /// <param name="from">room from</param>
        /// <param name="to">room to</param>
        /// <returns>IPassage, if exists</returns>
        public IPassage GetPassageBetween(IRoom from, IRoom to)
        {
            foreach (IPassage passage in GetAll<IPassage>())
            {
                if (passage.OneEnd(this).Equals(from) && passage.SecondEnd(this).Equals(to))
                {
                    return passage;
                }
                else if (passage.OneEnd(this).Equals(to) && passage.SecondEnd(this).Equals(from))
                {
                    return passage;
                }
            }
            throw new ArgumentException("couldnt find a passage between these rooms");
        }

        /// <summary>
        /// Get rooms that player can walk to from a specified room
        /// </summary>
        /// <param name="from">room to go from</param>
        /// <returns>rooms to go to</returns>
        public ISet<IRoom> GetRoomsToWalkTo(IRoom from)
        {
            HashSet<IRoom> rooms = new HashSet<IRoom>();
            foreach (IPassage passage in GetAll<IPassage>())
            {
                if (passage.OneEnd(this).Equals(from))
                {
                    rooms.Add(passage.SecondEnd(this));
                }
                else if (passage.SecondEnd(this).Equals(from))
                {
                    rooms.Add(passage.OneEnd(this));
                }
            }
            return rooms;
        }

        /// <summary>
        /// Get title of the game
        /// </summary>
        /// <returns>title</returns>
        public abstract String GetTitle();

        /// <summary>
        /// Get current score of the game
        /// </summary>
        /// <returns>current score</returns>
        public abstract int GetScore();
 
        /// <summary>
        /// Get a story
        /// </summary>
        /// <returns>game story</returns>
        public abstract String GetStory();

        /// <summary>
        /// Get a game objective
        /// </summary>
        /// <returns>game objective</returns>
        public abstract String GetObjective();
    }
}
