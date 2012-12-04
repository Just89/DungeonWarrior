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
using Dungeon.Characters;
using Dungeon.Characters.Monster;
using System.Diagnostics;

namespace Dungeon
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Startscreen startScreen;
        Endscreen endScreen;
        Level level;

        Song backgroundSong; 

        Player player;

        Skeleton skeleton1;
        Skeleton skeleton2;
        Skeleton skeleton3;

        Mage mage1;
        Mage mage2;
        Mage mage3;

        Boss boss;

        bool Skeleton1Collision = false;
        bool Skeleton2Collision = false;
        bool Skeleton3Collision = false;

        bool Mage1Collision = false;
        bool Mage2Collision = false;
        bool Mage3Collision = false;

        bool BossCollision = false;

        public Game1()
        {
             //Setting wich level needs to be loaded
            level = new Level(this, "Level1");
            //Adding player
            player = new Player(this);

            //Adding skeletons + giving them a start position
            skeleton1 = new Skeleton(this);
            skeleton1.Position = new Vector2(350, 560);

            skeleton2 = new Skeleton(this);
            skeleton2.Position = new Vector2(217, 431);

            skeleton3 = new Skeleton(this);
            skeleton3.Position = new Vector2(550, 367);

            //Adding Mages + giving them a start position
            mage1 = new Mage(this);
            mage1.Position = new Vector2(600, 495);

            mage2 = new Mage(this);
            mage2.Position = new Vector2(350, 302);

            mage3 = new Mage(this);
            mage3.Position = new Vector2(250, 175);

            //Adding boss + giving him a start position
            boss = new Boss(this);
            boss.Position = new Vector2(150, 43);

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Setting the Height&Width from the game window
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 640;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here     

            startScreen = new Startscreen(this);
            endScreen = new Endscreen(this);
            Components.Add(startScreen);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Adding background song to the game
            backgroundSong = Content.Load<Song>("dungeon_siege");
            //Making it repeat itself
            MediaPlayer.IsRepeating = true; 
            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
        
            //When the player presses A the game will start because loadlevel will become true 
          if (Level.loadLevel == false && (Keyboard.GetState().IsKeyDown(Keys.A) == true || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) == true))
          {
              init();
          }
          //If the level is loaded collision will be added
          if (Level.loadLevel == true)
          {
              gameCollision();
          }
          // If the player reaches the door and presses B the game will stop
          if (Level.loadLevel == true && (Keyboard.GetState().IsKeyDown(Keys.B) == true || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B) == true))
          {
              var position = player.position;
              Point currentTile = new Point((int)(position.X / 32), (int)(position.Y / 32));
              if (Level.TileID(currentTile.X, currentTile.Y) == Tile.Door)
              {
                  cleanUp();
              }
          }
           // TODO: Add your update logic here
            
           base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
     
            base.Draw(gameTime);
        }

        //Function init will give the necessary components to play the game
        private void init()
        {
            //removing the startscreen
            Components.Remove(startScreen);

            //Start the background song when the game starts
            MediaPlayer.Play(backgroundSong);
         
            //When everything is cleared the game will start adding the necessary components to play
            Components.Add(level);
            Components.Add(skeleton1);
            Components.Add(skeleton2);
            Components.Add(skeleton3);
            Components.Add(mage1);
            Components.Add(mage2);
            Components.Add(mage3);
            Components.Add(boss);
            Components.Add(player);
            Level.loadLevel = true;
        }

        private void cleanUp()
        {
            //Stop the background song when the game starts
            MediaPlayer.Stop();

            //Removing the gamecomponents
            Components.Remove(level);
            Components.Remove(skeleton1);
            Components.Remove(skeleton2);
            Components.Remove(skeleton3);
            Components.Remove(mage1);
            Components.Remove(mage2);
            Components.Remove(mage3);
            Components.Remove(boss);
            Components.Remove(player);

            //adding the startscreen
            Components.Add(endScreen);

            Level.loadLevel = false;
        }

        private void gameCollision()
        {
            //Skeletons collision rectangles
            Rectangle skeleton1Rectangle = new Rectangle((int)skeleton1.Position.X, (int)skeleton1.Position.Y,
                skeleton1.skin.Width, skeleton1.skin.Height);
            Rectangle skeleton2Rectangle = new Rectangle((int)skeleton2.Position.X, (int)skeleton2.Position.Y,
                skeleton2.skin.Width, skeleton2.skin.Height);
            Rectangle skeleton3Rectangle = new Rectangle((int)skeleton3.Position.X, (int)skeleton3.Position.Y,
                skeleton3.skin.Width, skeleton3.skin.Height);

            //Mages collision rectangles
            Rectangle mage1Rectangle = new Rectangle((int)mage1.Position.X, (int)mage1.Position.Y,
                mage1.skin.Width, mage1.skin.Height);
            Rectangle mage2Rectangle = new Rectangle((int)mage2.Position.X, (int)mage2.Position.Y,
                mage2.skin.Width, mage2.skin.Height);
            Rectangle mage3Rectangle = new Rectangle((int)mage3.Position.X, (int)mage3.Position.Y,
                mage3.skin.Width, mage3.skin.Height);

            Rectangle bossRectangle = new Rectangle((int)boss.Position.X, (int)boss.Position.Y,
                boss.skin.Width, boss.skin.Height);

            //setting up intersects between player and mobs
            if (player.playerRectangle.Intersects(skeleton1Rectangle)) Skeleton1Collision = true;
            if (player.playerRectangle.Intersects(skeleton2Rectangle)) Skeleton2Collision = true;
            if (player.playerRectangle.Intersects(skeleton3Rectangle)) Skeleton3Collision = true;
            if (player.playerRectangle.Intersects(mage1Rectangle)) Mage1Collision = true;
            if (player.playerRectangle.Intersects(mage2Rectangle)) Mage2Collision = true;
            if (player.playerRectangle.Intersects(mage3Rectangle)) Mage3Collision = true;
            if (player.playerRectangle.Intersects(bossRectangle)) BossCollision = true;

            var gamepadA = GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A);
            var keyboardA = Keyboard.GetState().IsKeyDown(Keys.A);
            //Skeletons will sort off follow on collision with player
            if (Skeleton1Collision) 
            {
                skeleton1.movement = player.movement;
                //Die skeleton!
                if (gamepadA == true || keyboardA == true) { Components.Remove(skeleton1); }
            }
            if (Skeleton2Collision) 
            {
                skeleton2.movement = player.movement;
                if (gamepadA == true || keyboardA == true) { Components.Remove(skeleton2); }
            }
            if (Skeleton3Collision) 
            {
                skeleton3.movement = player.movement;
                if (gamepadA == true || keyboardA == true) { Components.Remove(skeleton3); }
            }

            //Mages will stop moving on collision with player
            if (Mage1Collision) 
            { 
                mage1.moveMage = false;
                //Die mage!
                if (gamepadA == true || keyboardA == true) { Components.Remove(mage1); }
            }
            if (Mage2Collision) 
            { 
                mage2.moveMage = false;
                if (gamepadA == true || keyboardA == true) { Components.Remove(mage2); }
            }
            if (Mage3Collision) 
            { 
                mage3.moveMage = false;
                if (gamepadA == true || keyboardA == true) { Components.Remove(mage3); }
            }

            if (BossCollision) 
            {
                //Die boss!
                if (gamepadA == true || keyboardA == true) { Components.Remove(boss); } 
            }
        }
    }
}
