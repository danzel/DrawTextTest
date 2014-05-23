// // /***************************************************************************/
// // Datum:        01:35, 20.05.2014
// // Project:      WindowsGameXNA/WindowsGameXNA/Game1.cs
// // Copyright (c) Insert Information Technologies GmbH
// // /***************************************************************************/

#region

using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace DrawTextTest
{
    /// <summary>
    ///     This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private FrameRateCounter fpsCounter;
        private double debugDownTime;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }


        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            // FPS-Counter laden
            this.fpsCounter = new FrameRateCounter(this)
            {
                DebugModus = true
            };
            this.fpsCounter.LoadContent();
        }

        private int drawFrameCount, updateFrameCount;
        private StringBuilder log = new StringBuilder();

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            log.AppendFormat("Update {0} {1}, ", updateFrameCount, gameTime.IsRunningSlowly);
            log.AppendLine();
            updateFrameCount++;

            debugDownTime += gameTime.ElapsedGameTime.TotalSeconds;

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || debugDownTime > 10)
            {
                this.fpsCounter.OutputTotalTime();
                File.WriteAllText("out.txt", log.ToString());
                this.Exit();
            }
            // UPS-Counter updaten
            this.fpsCounter.StartUpdateTimer();

            // TODO: Add your update logic here

            // UPS-Counter updaten
            this.fpsCounter.EndUpdateTimer(gameTime);

            if (updateFrameCount == 105)
                Thread.Sleep(60);
            if (updateFrameCount == 205 || updateFrameCount == 206)
                Thread.Sleep(120);

            base.Update(gameTime);
        }


        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            log.AppendFormat("Draw {0} {1}, ", drawFrameCount, gameTime.IsRunningSlowly);
            log.AppendLine();
            drawFrameCount++;


            if (drawFrameCount == 150)
                Thread.Sleep(60);
            if (drawFrameCount == 260 || drawFrameCount == 261)
                Thread.Sleep(120);

            // FPS-Updaten
            this.fpsCounter.StartDrawTimer();

            // Clearen
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            this.spriteBatch.DrawString(this.fpsCounter.spriteFont, "abcdefghijklmnopqrstuvwxyz1234567890 some words here on the end test amazing testing letters to the edge of the screen", new Vector2(0, 40), Color.Black);
            this.spriteBatch.End();

            // Draw
            this.fpsCounter.EndDrawTimer(gameTime);

            base.Draw(gameTime);
        }
    }
}