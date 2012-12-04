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
using System.Diagnostics;
using Dungeon.level;
using Dungeon.Characters.Monster;

namespace Dungeon.Characters
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private int hp = 100;

        private int startPositionX = 60;
        private int startPositionY = 560;

        private SpriteBatch spriteBatch;

        public Vector2 position;
        public Vector2 movement;
        public KeyboardState keyboard;

        public Rectangle playerRectangle;
        private Texture2D character;

        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            Game.Content.RootDirectory = "Content";
            
        }
       
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //Setting the player startposition

            position.X = startPositionX;
            position.Y = startPositionY;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //healthbar, not used at the moment
            //rectangle = Healthbar(200, 20);
            
            // TODO: use this.Content to load your game content here
            character = Game.Content.Load<Texture2D>("mobs/Player");
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //Player collision rectangle
            playerRectangle = new Rectangle((int)this.position.X, (int)this.position.Y,
                this.character.Width, this.character.Height);

            Control();
            base.Update(gameTime);
        }
        
        //Checking the collision for the player, to make sure he cant move through walls, and that
        //he can only move up and down when he is supposed to.
        public static Vector2 CheckCollision(Vector2 position, Vector2 movement)
        {
            //Tracking the current tile the player is on

            Point currentTile = new Point((int)(position.X / 32), (int)(position.Y / 32));
            if (movement.X < 0)
            {
                //Making sure the player can walk left and right through walls
                if (Level.TileID(currentTile.X, currentTile.Y) == Tile.Collision)
                {
                    movement.X = 0;
                }
            }
            if (movement.X > 0)
            {
                if (Level.TileID(currentTile.X + 1, currentTile.Y) == Tile.Collision)
                {
                    movement.X = 0;
                }

            }
            if (movement.Y < 0)
            {
                //If player hits stairs he can move up and down
                if (Level.TileID(currentTile.X, currentTile.Y) != Tile.Stair)
                {
                    movement.Y = 0;
                }
            }

            if (movement.Y > 0)
            {
                if (Level.TileID(currentTile.X, currentTile.Y + 1) != Tile.Stair)
                {
                    movement.Y = 0;
                }
            }
            return position + movement;
        }

        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(character, position, Color.White);
            
            //drawing the healtbar, not used at the moment
            //spriteBatch.Draw(rectangle, new Vector2(550,50), Color.White);
            spriteBatch.End();
        }

        //Function that handles the controls via gamepad and keyboard
        private void Control()
        {
            movement = new Vector2(0,0);

            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                //GamePad Controls
                movement.X = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                movement.Y = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1;

                //If A is clicked the player does a melee attack
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) == true)
                {
                    //Attack
                    //Not implented yet
                }
                //If B is clicked and the player is on the position of a chest he will gain hp
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B) == true)
                {
                    //action
                    Point currentTile = new Point((int)(position.X / 32), (int)(position.Y / 32));

                    if (Level.TileID(currentTile.X, currentTile.Y) == Tile.Chest)
                    {
                        //Not implented yet
                        //gain hp
                    }
                }
            }
            else
            {
                //Keyboard Controls
                keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.Up) == true)
                {
                    movement.Y = -1.5f;
                }
                if (keyboard.IsKeyDown(Keys.Down) == true)
                {
                    movement.Y = 1.5f;
                }
                if (keyboard.IsKeyDown(Keys.Left) == true)
                {
                    movement.X = -1.5f;
                }
                if (keyboard.IsKeyDown(Keys.Right) == true)
                {
                    movement.X = 1.5f;
                }
                //attack
                //If A is clicked the player does a melee attack
                if (keyboard.IsKeyDown(Keys.A) == true)
                {
                    //attack
                    //Not implented yet
                }
                //action
                //If B is clicked and the player is on the position of a chest he will will gain hp
                if (keyboard.IsKeyDown(Keys.B) == true)
                {
                    Point currentTile = new Point((int)(position.X / 32), (int)(position.Y / 32));
                    
                    if (Level.TileID(currentTile.X, currentTile.Y) == Tile.Chest)
                    {
                        //gain hp  
                        //Not implented yet
                    }
                }
            }
            position = CheckCollision(position, movement);
        }

        //this draws a red hp bar, not used at the moment
        /*public Texture2D Healthbar(int width, int height)
        {
            // create the rectangle texture
            Texture2D rectangleTexture = new Texture2D(GraphicsDevice, width, height, 1, TextureUsage.None,
            SurfaceFormat.Color);
            //set the color to the amount of pixels
            Color[] color = new Color[width * height];
            //loop through all the colors setting them to the color red
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = new Color(255, 0, 0, 225);
            }
            rectangleTexture.SetData(color);//set the color data on the texture
            return rectangleTexture;
        }*/
        private void MeleeAttack()
        {
            //Not implented yet
        }

        private void DamageTaken(int hp)
        {
            //Not implented yet
        }
    }
}