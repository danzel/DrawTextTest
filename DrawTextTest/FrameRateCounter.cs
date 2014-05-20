// /***************************************************************************/
// Datum:        17:41, 19.12.2013
// Project:      PathFindingSample/FinalDarkness - Performance/FrameRateCounter.cs
// Copyright (c) Kai Cissarek
// /***************************************************************************/
#region

using System;
using DrawTextTest.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace DrawTextTest
{
    /// <summary>
    /// </summary>
    public class FrameRateCounter
    {
        /// <summary>
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// </summary>
        private SpriteFont spriteFont;

        /// <summary>
        /// </summary>
        private readonly Game game;

        /// <summary>
        /// </summary>
        private int frameRate;

        /// <summary>
        /// </summary>
        private int frameCounter;

        /// <summary>
        /// </summary>
        private TimeSpan elapsedTime = TimeSpan.Zero;

        /// <summary>
        /// </summary>
        private readonly TimeSpan oneSec = TimeSpan.FromSeconds(1);

        /// <summary>
        /// </summary>
        private readonly Vector2 fpsPosition = new Vector2(3, 0);

        /// <summary>
        /// </summary>
        private readonly Vector2 updatePosition = new Vector2(60, 0);

        /// <summary>
        /// </summary>
        private readonly Vector2 drawPosition = new Vector2(136, 0);

        /// <summary>
        /// </summary>
        private readonly Vector2 slowPosition = new Vector2(280, 0);

        /// <summary>
        /// </summary>
        private readonly HiPerfTimer drawTimer = new HiPerfTimer();

        /// <summary>
        /// </summary>
        private readonly HiPerfTimer updateTimer = new HiPerfTimer();


        /// <summary>
        /// </summary>
        public bool DebugModus { get; set; }


        /// <summary>
        /// </summary>
        /// <param name="game"></param>
        public FrameRateCounter(Game game)
        {
            this.game = game;
        }


        /// <summary>
        /// </summary>
        public void LoadContent()
        {
            var resxContent = new ResourceContentManager(this.game.Services, Resources.ResourceManager);
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice);
            this.spriteFont = resxContent.Load<SpriteFont>("interfaceFontSmall");
        }


        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateFrameTimer(GameTime gameTime)
        {
            // Debugmodus
            this.elapsedTime += gameTime.ElapsedGameTime;

            if (this.elapsedTime > this.oneSec)
            {
                this.elapsedTime -= this.oneSec;
                this.frameRate = this.frameCounter;
                this.frameCounter = 0;
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        private void DrawTimers(GameTime gameTime)
        {
            // Debugmodus
            this.frameCounter++;

            this.spriteBatch.Begin();

            this.spriteBatch.DrawString(this.spriteFont, "FPS: " + this.frameRate, this.fpsPosition, Color.White);
            this.spriteBatch.DrawString(this.spriteFont, "U " + this.updateTimer.Duration.ToString("0.00000"), this.updatePosition, Color.White);
            this.spriteBatch.DrawString(this.spriteFont, "D " + this.drawTimer.Duration.ToString("0.00000"), this.drawPosition, Color.White);

            // Falls es mal wieder länger dauert, gibts trotzdem kein Snickers
            if (gameTime.IsRunningSlowly)
            {
                this.spriteBatch.DrawString(this.spriteFont, "THE GAME IS RUNNING SLOW !!!", this.slowPosition, Color.White);
            }

            this.spriteBatch.End();
        }


        /// <summary>
        /// </summary>
        public void StartUpdateTimer()
        {
            // Debugmodus
            if (this.DebugModus)
            {
                this.updateTimer.Start();
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public void EndUpdateTimer(GameTime gameTime)
        {
            // Debugmodus
            if (this.DebugModus)
            {
                this.UpdateFrameTimer(gameTime);
                this.updateTimer.Stop();
            }
        }


        /// <summary>
        /// </summary>
        public void StartDrawTimer()
        {
            // Debugmodus
            if (this.DebugModus)
            {
                this.drawTimer.Start();
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public void EndDrawTimer(GameTime gameTime)
        {
            // Debugmodus
            if (this.DebugModus)
            {
                this.drawTimer.Stop();
                this.DrawTimers(gameTime);
            }
        }
    }
}