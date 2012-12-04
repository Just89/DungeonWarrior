using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Dungeon.level;


namespace Dungeon.Characters.Monster
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Skeleton : Monster
    {
      
        public bool moveSkeleton = true;
        public Skeleton(Game game)
            : base(game, "mobs/Skeleton", 75)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
           // monster.SetStartPosition(300, 300);
           
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            SkeletonMove();
            
            base.Update(gameTime);
        }
        private void SkeletonMove()
        {
            Point currentTile = new Point((int)(position.X / 32), (int)(position.Y / 32));

            //Making sure the skeleton cant walk left and right through walls
            if (moveSkeleton == true)
            {
                if (movement.X > 0)
                {
                    if (Level.TileID(currentTile.X + 1, currentTile.Y) != Tile.Collision)
                    {
                        //If there is no collision on his right the skeleton is free to walk
                        position += movement;
                    }
                    else
                    {
                        //Otherwise if there is collision he will go the other way
                        movement.X *= -1;
                        position += movement;
                    }
                }
                else if (movement.X < 0)
                {
                    if (Level.TileID(currentTile.X, currentTile.Y) != Tile.Collision)
                    {
                        //If there is no collision on his left the skeleton is free to walk
                        position += movement;
                    }
                    else
                    {
                        //Otherwise if there is collision he will go the other way
                        movement.X *= -1;
                        position += movement;
                    }
                }
            }
        }
        public void MeleeAttack()
        {
            //Not implented yet
        }
    }
}