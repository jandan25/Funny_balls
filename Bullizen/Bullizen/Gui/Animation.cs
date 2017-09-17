using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullizen
{
    class Animation
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 originalPosition;
        private Vector2 position;
        private Vector2 velocity;

        private int frameHeight;
        private int frameWidth;
        private int currentFrame;
        private float timer;
        private float interval = 100; //interval smeny kadrov

        public Animation(Texture2D newTexture, Vector2 newPosition, int newFrameHeith, int newFrameWidth)
        {
            texture = newTexture;
            position = newPosition;
            frameHeight = newFrameHeith;
            frameWidth = newFrameWidth;
        }

        public void Update(GameTime gametime)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, 0,frameWidth,frameHeight);
            originalPosition = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Right(gametime);
                velocity.X = 3; // skorost ball
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Left(gametime);
                velocity.X = -3;
            }
            else
            {
                velocity = Vector2.Zero;
            }

            

        }

        public void Right(GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.TotalSeconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 3)
                    currentFrame = 0;
            }
        }

        public void Left(GameTime gametime)
        {
            timer += (float)gametime.ElapsedGameTime.TotalSeconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 7 || currentFrame < 4)
                    currentFrame = 4;
            }
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, rectangle, Color.White, 0f, originalPosition, 1f, SpriteEffects.None, 0);
        }
    }

}
