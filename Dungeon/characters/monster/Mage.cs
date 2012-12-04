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
    public class Mage : Monster
    {
        public bool moveMage = true;
        public Mage(Game game)
            : base(game, "mobs/Mage", 50)
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

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MageMove();
            base.Update(gameTime);
        }
        private void MageMove()
        {
            Point currentTile = new Point((int)(position.X / 32), (int)(position.Y / 32));
            // TODO: Add your update code here
            //Making sure the mage cant walk left and right through walls
            if (moveMage == true)
            {
                if (movement.X > 0)
                {
                    if (Level.TileID(currentTile.X + 1, currentTile.Y) != Tile.Collision)
                    {
                        //If there is no collision on his right the mage is free to walk
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
                        //If there is no collision on his left the mage is free to walk
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
        private void RangedAttack()
        {
            //Not implented yet
        }
    }
}