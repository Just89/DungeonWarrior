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


namespace Dungeon
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Endscreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game mygame;
        private Texture2D endScreen;
        private SpriteBatch spriteBatch;
        public Endscreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            mygame = game;
            endScreen = game.Content.Load<Texture2D>("Dungeon_Hunter2");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(endScreen, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Update(gameTime);
        }
    }
}