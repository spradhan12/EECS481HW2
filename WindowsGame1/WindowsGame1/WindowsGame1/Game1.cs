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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Ball Sprite
        Texture2D ballSprite;
        //Ball Position on Screen
        Vector2 ballPosition = Vector2.Zero;
        //Ball motion on Screen
        Vector2 ballSpeed = new Vector2(150, 150);

        //Federer Sprite
        Texture2D federerSprite;
        //Federer Position on Screen
        Vector2 federerPosition;

        //Nadal Sprite
        Texture2D nadalSprite;
        //Nadal Position on Screen
        Vector2 nadalPosition;

        //Font Sprite
        SpriteFont fontSprite;

        //Scores
        int federerScore = 0;
        int nadalScore = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            base.Initialize();
            IsMouseVisible = true;

            ballPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            federerPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width - federerSprite.Width,
                                          graphics.GraphicsDevice.Viewport.Height / 2 - federerSprite.Height / 2);

            nadalPosition = new Vector2(0, graphics.GraphicsDevice.Viewport.Height / 2 - nadalSprite.Height / 2);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballSprite = Content.Load<Texture2D>("ball");
            federerSprite = Content.Load<Texture2D>("federer");
            nadalSprite = Content.Load<Texture2D>("nadal");
            fontSprite = Content.Load<SpriteFont>("ScoreFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Random hit = new Random();
            //randBinary used whenever a random -1, or 1 is needed
            int randBinary = hit.Next(-2, 1);
            if (randBinary == 0)
                randBinary = 1;
            else if (randBinary == -2)
                randBinary = -1;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ballPosition += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Update federer's position
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up))
                if (federerPosition.Y > 0)
                    federerPosition.Y -= 10;
                else
                    federerPosition.Y = 0;
            else if (keyState.IsKeyDown(Keys.Down))
                if (federerPosition.Y < GraphicsDevice.Viewport.Height - federerSprite.Height)
                    federerPosition.Y += 10;

            //Update nadal's position
            if (keyState.IsKeyDown(Keys.A))
                if (nadalPosition.Y > 0)
                    nadalPosition.Y -= 10;
                else
                    nadalPosition.Y = 0;
            else if (keyState.IsKeyDown(Keys.Z))
                if (nadalPosition.Y < GraphicsDevice.Viewport.Height - nadalSprite.Height)
                    nadalPosition.Y += 10;
       

            int maxX = GraphicsDevice.Viewport.Width - federerSprite.Width;
            int maxY = GraphicsDevice.Viewport.Height - ballSprite.Height;

            //Check for bounce
           // if (ballPosition.X > maxX || ballPosition.X < 0)
             //   ballSpeed.X *= -1;
            if (ballPosition.Y > maxY || ballPosition.Y < 0)
            {
                ballSpeed.Y *= -1;
                if (ballSpeed.Y < 0)
                    ballSpeed.Y -= hit.Next(5, 15);
                else
                    ballSpeed.Y += hit.Next(5, 15);
            }

            else if (ballPosition.X > maxX)
            {
                nadalScore += 1;
                ballPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width/2, graphics.GraphicsDevice.Viewport.Height/2);
                ballSpeed.X = hit.Next(100, 200);
                ballSpeed.X *= randBinary;
                ballSpeed.Y = hit.Next(100, 200);
                ballSpeed.Y *= randBinary;
            }

            else if (ballPosition.X < 0)
            {
                federerScore += 1;
                ballPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
                ballSpeed.X = hit.Next(100, 200);
                ballSpeed.X *= randBinary;
                ballSpeed.Y = hit.Next(100, 200);
                ballSpeed.Y *= randBinary;
            }

            //Check if ball collides with player
            Rectangle ballRect = new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
                                                ballSprite.Width, ballSprite.Height);

            Rectangle federerRect = new Rectangle((int)federerPosition.X, (int)federerPosition.Y,
                                                federerSprite.Width, federerSprite.Height);

            Rectangle nadalRect = new Rectangle((int)nadalPosition.X, (int)nadalPosition.Y,
                                                nadalSprite.Width, nadalSprite.Height);

            if (ballRect.Intersects(federerRect) || ballRect.Intersects(nadalRect))
            {

                ballSpeed.X *= -1;
                ballSpeed.Y *= randBinary;

                if (ballSpeed.X < 0)
                    ballSpeed.X -= 30;
                else
                    ballSpeed.X += 30;

                if (ballSpeed.Y < 0)
                    ballSpeed.Y -= hit.Next(20, 50);
                else
                    ballSpeed.Y += hit.Next(20, 50);

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Green);
            spriteBatch.DrawString(fontSprite, Convert.ToString(federerScore), new Vector2(graphics.GraphicsDevice.Viewport.Width/2 + 50, 20), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(fontSprite, Convert.ToString(nadalScore), new Vector2(graphics.GraphicsDevice.Viewport.Width/2 - 50, 20), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(ballSprite, ballPosition, Color.White);
            spriteBatch.Draw(federerSprite, federerPosition, Color.White);
            spriteBatch.Draw(nadalSprite, nadalPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
