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
    public class Startscreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game mygame;
        private Texture2D beginScreen;
        private SpriteBatch spriteBatch;

        public Startscreen(Game game)
            : base(game)
        {
            mygame = game;
            beginScreen = game.Content.Load<Texture2D>("Dungeon_Hunter");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(beginScreen, new Vector2(0, 0), Color.White);
            spriteBatch.End();  
            base.Update(gameTime);
        }
    }
}