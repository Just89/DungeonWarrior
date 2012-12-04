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
using DungeonGameLibrary;


namespace Dungeon.level
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Level : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private string levelName;
        private static LevelData levelData;
        public static bool loadLevel = false;

        private SpriteBatch spriteBatch;
              
        private Texture2D[] tiles = new Texture2D[18];
        public int backgroundTileID;
        public int colisionTileID;

        public Level(Game game, String levelName)
            : base(game)
        {
       
            this.levelName = levelName;

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

        protected override void LoadContent()
        {
            //Loading the given leven
            levelData = Game.Content.Load<LevelData>(levelName);

            //Loading its Content (tiles)
            tiles[1] = Game.Content.Load<Texture2D>("tiles/top");
            tiles[2] = Game.Content.Load<Texture2D>("tiles/wall");
            tiles[3] = Game.Content.Load<Texture2D>("tiles/topRight_corner");
            tiles[4] = Game.Content.Load<Texture2D>("tiles/topLeft_corner");
            tiles[5] = Game.Content.Load<Texture2D>("tiles/bottem");
            tiles[6] = Game.Content.Load<Texture2D>("tiles/chest");
            tiles[7] = Game.Content.Load<Texture2D>("tiles/bottemRight_corner");
            tiles[8] = Game.Content.Load<Texture2D>("tiles/bottemLeft_corner");
            tiles[9] = Game.Content.Load<Texture2D>("tiles/stairs");
            tiles[10] = Game.Content.Load<Texture2D>("tiles/bottem_stairs");
            tiles[11] = Game.Content.Load<Texture2D>("tiles/topLeft_door");
            tiles[12] = Game.Content.Load<Texture2D>("tiles/topRight_door");
            tiles[13] = Game.Content.Load<Texture2D>("tiles/top_stairs");
           // tiles[14] = Game.Content.Load<Texture2D>("tiles/");
            tiles[15] = Game.Content.Load<Texture2D>("tiles/bottemLeft_door");
            tiles[16] = Game.Content.Load<Texture2D>("tiles/bottemRight_door");
            tiles[17] = Game.Content.Load<Texture2D>("tiles/top");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.LoadContent();

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

        public override void Draw(GameTime gameTime)
        {
            if(loadLevel == true)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                //Looping through the 2 dimensional array's and drawing the required tiles on the screen
                for (int x = 0; x < levelData.BackgroundLayer.Length; x++)
                {
                    for (int y = 0; y < levelData.BackgroundLayer[x].Length; y++)
                    {
                        backgroundTileID = levelData.BackgroundLayer[x][y];
                        colisionTileID = levelData.ColisionLayer[x][y];
                        if (backgroundTileID != 0)
                        {
                            spriteBatch.Draw(tiles[backgroundTileID], new Vector2(x * 32, y * 32), Color.White);
                        }
                        if (colisionTileID != 0)
                        {
                            spriteBatch.Draw(tiles[colisionTileID], new Vector2(x * 32, y * 32), Color.White);
                        }
                    }
                }
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
        
        //Deciding what to return when hitting a certain tile
        public static Tile TileID(int x, int y)
        {
            int id = levelData.ColisionLayer[x][y];
            //door
            if (id == 11 || id == 12 || id == 15 || id == 16)
            {
                return Tile.Door;
            }
            //stairs
            if (id == 9 || id == 10 || id == 13)
            {
                return Tile.Stair;
            }
            //chests
            if (id == 6 || id == 17)
            {
                return Tile.Chest;
            }
            //bigger then 0 is always collision (used for walls mostly)
            if (id > 0)
            {
                return Tile.Collision;
            }
        
            return Tile.Walkable;
        }
    }
}