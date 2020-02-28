using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
//Tyler Lynch
//Holds static methods to move the dungeon when the character exits a door.
namespace OrderofthePaw
{
    class Camera
    {
        const int ROOM_WIDTH = 800;
        const int ROOM_HEIGHT = 480;

        //Methods that move the camera around the dungeon.
        //The method direction names are determined by which direction off of the screen the player goes, not by the direction the rooms move.
        public static void Up(Dungeon d, EntityManager e)
        {
            foreach(Room r in d.Rooms)
            {
                r.Y = r.Y+ ROOM_HEIGHT;

                //This foreach moves the collision within the 
                if (r.file != "0")
                {
                    foreach (Tile t in r.Map)
                    {
                        Room.GenerateRectangle(t);
                    }

                }
            }
            
            foreach (Entity en in e.Entities)
            {
                if (en is Dog)
                {
                    Dog dog = (Dog)en;
                    dog.Y = dog.Y + ROOM_HEIGHT;
                }

            }
            
        }
        public static void Down(Dungeon d, EntityManager e)
        {
            foreach (Room r in d.Rooms)
            {
                r.Y = r.Y - ROOM_HEIGHT;

                if(r.file != "0")
                {
                    foreach (Tile t in r.Map)
                    {
                        Room.GenerateRectangle(t);
                    }

                }
            }
            foreach (Entity en in e.Entities)
            {
                if (en is Dog)
                {
                    Dog dog = (Dog)en;
                    dog.Y = dog.Y - ROOM_HEIGHT;
                }

            }
        }
        public static void Left(Dungeon d, EntityManager e)
        {
            foreach (Room r in d.Rooms)
            {
                r.X = r.X + ROOM_WIDTH;
                if (r.file != "0")
                {
                    foreach (Tile t in r.Map)
                    {
                        Room.GenerateRectangle(t);
                    }

                }
            }
            foreach (Entity en in e.Entities)
            {
                if (en is Dog)
                {
                    Dog dog = (Dog)en;
                    dog.X = dog.X + ROOM_WIDTH;
                }
                
            }
        }
        public static void Right(Dungeon d, EntityManager e)
        {
            foreach (Room r in d.Rooms)
            {
                r.X = r.X - ROOM_WIDTH;
                if (r.file != "0")
                {
                    foreach (Tile t in r.Map)
                    {
                        Room.GenerateRectangle(t);
                    }

                }
            }
            
            foreach (Entity en in e.Entities)
            {
                if(en is Dog)
                {
                    Dog dog = (Dog)en;
                    dog.X = dog.X - ROOM_WIDTH;
                }
                
            }
        }
    }
}
