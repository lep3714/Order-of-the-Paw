using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//Tyler Lynch
//Holds the texture for the dungeon tiles.
namespace OrderofthePaw
{   
    enum TileType
    {
        Floor,
        Door,
        LeftWall,
        RightWall,
        BottomWall,
        TopWall,
        TLCorner,
        TRCorner,
        BLCorner,
        BRCorner
    }
    class Tile
    {
        const int T2D_WIDTH = 64;
        const int T2D_HEIGHT = 36;
        TileType type;

        Texture2D texture;
        Rectangle rect;//Holds the size and location info of the tile;
        Wall collision;//Contains collision information.
        Wall collision2;//Used for corner pieces.
        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        public Wall Collision
        {
            get { return collision; }
            set { collision = value; }
        }

        public Wall Collision2
        {
            get { return collision2; }
            set { collision2 = value; }
        }

        public TileType TileType
        {
            get { return type; }
        }

        public Rectangle CLocation
        {
            get { return collision.Position; }
            set { collision.Position= value; }
        }

        public Rectangle C2Location
        {
            get { return collision2.Position; }
            set { collision2.Position = value; }
        }

        public Tile(Texture2D t)
        {
            texture = t;
            rect = new Rectangle();
            collision = new Wall();
            collision2 = new Wall();

            switch (texture.Name)//Used to determine the position of the collision triangle, or if one even exists.
            {
                case "Wall_Left": type = TileType.LeftWall; break;
                case "Wall_Right": type = TileType.RightWall; break;
                case "Wall_Top": type = TileType.TopWall; break;
                case "Wall_Bottom": type = TileType.BottomWall; break;
                case "Wall_BRcorner": type = TileType.BRCorner; break;
                case "Wall_BLcorner": type = TileType.BLCorner; break;
                case "Wall_TRcorner": type = TileType.TRCorner; break;
                case "Wall_TLcorner": type = TileType.TLCorner; break;
                case "Door_Top": type = TileType.Door; break;
                case "Door_Bottom": type = TileType.Door; break;
                default: type = TileType.Floor; break;
            }
        }
    }
}
