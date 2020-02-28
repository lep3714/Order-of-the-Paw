using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OrderofthePaw
{
    class EntityManager
    {
        //Fields
        private List<Entity> entities;

        public List<Entity> Entities
        {
            get { return entities; }
            set { entities = value; }
        }
        //Constructor
        public EntityManager()
        {
            entities = new List<Entity>();
        }

        //Methods
        /// <summary>
        /// Removes the given entity from the list
        /// </summary>
        /// <param name="entity">the entity you are removing from the list</param>
        public void RemoveEntity(Entity entity)
        {
            if(entities.Contains(entity))
            {
                entities.RemoveAt(entities.IndexOf(entity));
            }
        }

        /// <summary>
        /// Adds the given entity to the list
        /// </summary>
        /// <param name="entity">The entity you are adding to the list</param>
        public void AddEntity(Entity entity)
        {
            if(entity != null)
            {
                entities.Add(entity);
            }
        }

        /// <summary>
        /// Retrieve the entity for use in the Game
        /// </summary>
        /// <param name="entity">The entity you wish to retrieve</param>
        /// <returns></returns>
        public Entity IndexEntity(Entity entity)
        {
            return entities[entities.IndexOf(entity)];
        }

        /// <summary>
        /// Call the Draw methods for all the entities in the list
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawEntities(SpriteBatch spriteBatch, int maxX, int maxY)
        {
            if (entities.Count > 0)
            {
                foreach (Entity entity in this.entities)
                {
                    if(entity is Dog)
                    {
                        Dog dog = (Dog)entity;
                        dog.Draw(spriteBatch);
                    }
                    else if(entity is Projectile)
                    {
                        Projectile proj = (Projectile)entity;
                        proj.Draw(spriteBatch);
                    }
                    else
                    {
                        entity.Draw(spriteBatch);
                    }
                }
            }

        }

        /// <summary>
        /// Call the Move methods for all the entities in the list
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void MoveEntities(GraphicsDeviceManager graphics)
        {
            bool noNull = false;
            while (!noNull)
            {
                if (entities.Contains(null))
                {
                    entities.RemoveAt(entities.IndexOf(null));
                }
                else
                {
                    noNull = true;
                }
            }

            if (entities.Count > 0)
            {
                foreach (Entity entity in entities)
                {
                    if(entity is Dog)
                    {
                        Dog dog = (Dog)entity;
                        dog.Move(graphics);
                    }
                    else if(entity is Projectile)
                    {
                        Projectile proj = (Projectile)entity;
                        proj.Move(graphics);

                        /*if (proj.Position.X < graphics.GraphicsDevice.Viewport.Width && proj.Position.Y < graphics.GraphicsDevice.Viewport.Height && proj.Position.X > 0 && proj.Position.Y > 0)
                        {
                            this.RemoveEntity(proj);
                        }*/

                    }
                    entity.Move(graphics);
                }
            }
        }


        /////Cycling Between the various types of entity managed by the list
        /// <summary>
        /// Return a list of all the projectiles in the Manager
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Projectile> CycleByType()
        {
            List<Projectile> projectiles = new List<Projectile>();
            foreach(Entity proj in this.entities )
            {
                if (proj is Projectile)
                {
                    projectiles.Add((Projectile)proj);
                }
            }

            return projectiles;
        }

        /// <summary>
        /// Return a list of all the projectiles in the Manager
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Dog> CycleByType(String listName)
        {
            List<Dog> dogs = new List<Dog>();
            foreach (Entity entity in this.entities)
            {
                if(entity is Dog)
                {
                    Dog doggo = (Dog)entity;
                    dogs.Add(doggo);
                }
            }
            return dogs;
        }

        public void Reset()
        {
            entities = new List<Entity>();
        }
    }
}
