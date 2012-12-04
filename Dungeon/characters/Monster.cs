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
    public class Monster : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int hp;
        
        private SpriteBatch spriteBatch;

        public Vector2 position = Vector2.Zero;

        public Texture2D skin;
        private string skinLocation;

        public Vector2 movement = new Vector2(0.75f, 0);

        public Monster(Game game, string skinLocation, int hp)
            : base(game)
        {
            // TODO: Construct any child components here
            this.skinLocation = skinLocation;
           
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
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            skin = Game.Content.Load<Texture2D>(skinLocation);
            // TODO: use this.Content to load your game content here
           

        }
        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(skin, position, Color.White);
            spriteBatch.End();
        }
        //Function to set all monsters positions
        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }
    }
}