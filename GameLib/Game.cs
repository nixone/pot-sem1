﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public abstract class Game
    {
        private Dictionary<String, IIdentifiable> _identifiables = new Dictionary<String, IIdentifiable>();

        private String _currentRoomId;

        private bool _isRunning = false;

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Add(IIdentifiable identifiable)
        {
            if (_identifiables.ContainsKey(identifiable.GetId()))
            {
                throw new ArgumentException(identifiable.GetId()+" already exists in the game");
            }
            _identifiables.Add(identifiable.GetId(), identifiable);
        }

        public IIdentifiable Get(String id)
        {
            if (!_identifiables.ContainsKey(id))
            {
                throw new IndexOutOfRangeException(id);
            }
            return _identifiables[id];
        }

        public T Get<T>(String id)
        {
            IIdentifiable identifiable = Get(id);
            if (!(identifiable is T))
            {
                throw new IndexOutOfRangeException(id + " is not of correct type, but it is "+identifiable.GetType());
            }
            return (T)identifiable;
        }

        public ISet<T> GetAll<T>()
        {
            HashSet<T> result = new HashSet<T>();
            foreach (IIdentifiable identifiable in _identifiables.Values)
            {
                if (identifiable is T)
                {
                    result.Add((T)identifiable);
                }
            }
            return result;
        }

        public void Start(String startingRoomId)
        {
            Get<IRoom>(startingRoomId);
            _currentRoomId = startingRoomId;
            _isRunning = true;
        }

        public void Finish()
        {
            _isRunning = false;
        }

        public IRoom GetCurrentRoom()
        {
            return Get<IRoom>(_currentRoomId);
        }

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

        public abstract String GetTitle();

        public abstract int GetScore();
 
        public abstract String GetStory();

        public abstract String GetObjective();
    }
}
