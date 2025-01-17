﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public static class EntityManager
    {

        static List<Entity> entityList = new List<Entity>();
        static List<Entity> addQueue = new List<Entity>();

        public static List<Entity> GetEntities()
        {
            return entityList;
        }

        public static void Clear()
        {
            entityList.Clear();
        }

        public static void AddEntity(Entity e)
        {
            addQueue.Add(e);
        }

        public static void RemoveEntity(Entity e)
        {
            entityList.Remove(e);
        }

        public static void Update()
        {
            foreach(Entity e in entityList)
            {
                e.Update();
            }

            for(int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].forRemoval)
                {
                    RemoveEntity(entityList[i]);
                    i--;
                }
            }

            //add from the add queue
            entityList.AddRange(addQueue);
            addQueue.Clear();
        }

        public static Entity FindFirstEntityOfType(Type t)
        {
            foreach(Entity e in entityList)
            {
                if (e.GetType() == t)
                    return e;
            }

            return null;
        }

    }
}
