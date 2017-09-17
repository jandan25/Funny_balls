using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullizen
{
    class Ball
    {
        public Texture2D BallTexture;
        public Rectangle BallRectangle;

        public Vector2 BallPosition;
        public Vector2 BallVelocity;

        public Vector2 oldBallPosition;
        public Vector2 oldBallVelocity;

        public bool BallAlreadyJump;
        private float bounce;
        private float count;

        public Color[] BallTextureData;
        public bool touched = false;
        public bool forward;// = false;
        public bool down;
        public bool started = false;

        public Ball(Texture2D newBallTexture, Vector2 newBallPosition, float newbounce)
        {
            BallTexture = newBallTexture;
            BallPosition = newBallPosition;

            oldBallPosition = BallPosition;
            oldBallVelocity = BallVelocity;

            bounce = newbounce;
            count = bounce * 10;
            BallAlreadyJump = false;

            BallTextureData = new Color[BallTexture.Width * BallTexture.Height];
            BallTexture.GetData(BallTextureData);

        }

        public void Update(GameTime gameTime)
        {
            if (!started)
            {
                forward = false;
                touched = false;
                BallAlreadyJump = false;
                down = false;
                BallPosition = oldBallPosition;
                BallVelocity = oldBallVelocity;
                count = bounce * 10;
            }
            if (touched)
            {
                BallVelocity = BallVelocity / 2;
                if (forward)
                {
                    //if ((BallVelocity.X <= 0.5f) && (BallVelocity.Y <= 0.5f))
                    //    started = false;
                    BallVelocity.X = BallVelocity.Y;
                    BallVelocity.X += bounce;// / 1.2f;
                    BallVelocity.Y += bounce;// / 1.2f;
                    if (!down)
                    {
                        BallVelocity.Y = -BallVelocity.Y;
                    }
                    //BallVelocity.Y = -BallVelocity.Y;
                }
                else
                {
                    //if ((BallVelocity.X <= 0.5f) && (BallVelocity.Y <= 0.5f))
                    //    started = false;
                    if (down)
                    {
                        BallVelocity.Y += bounce;
                        BallVelocity.Y = -BallVelocity.Y;
                    }
                    else
                        BallVelocity.Y += bounce;
                    BallVelocity.X = BallVelocity.Y;
                    BallVelocity.X += bounce; /// 1.2f;
                    //BallVelocity.Y += bounce; /// 1.2f;
                    BallVelocity.X = -BallVelocity.X;
                    if (!down)
                    {
                        BallVelocity.Y = -BallVelocity.Y;
                    }
                }
            }
            else if (started)
                BallAlreadyJump = true;
            //if (BallAlreadyJump == false && forward)
            //    BallVelocity.X = -BallVelocity.Y;
            if (BallAlreadyJump == false && forward == false)
                BallVelocity.X = BallVelocity.Y;
            if (BallAlreadyJump == true)
                BallVelocity.Y += 0.10f * 1; 
            if (down)
            {
                BallVelocity.Y += bounce;
            }
            if (BallPosition.Y + BallTexture.Height >= 710)
            {
                if (BallPosition.X == oldBallPosition.X && count <= 0)
                    started = false;
                BallVelocity.Y *= -bounce / 1.2f;
                BallPosition.Y -= 10f;
                count--;
            }
            if (BallPosition.X <= -60 || BallPosition.X >= 1280)
                started = false;

            BallPosition += BallVelocity;
            BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BallTexture, BallRectangle, Color.White);
        }
        public bool IntersectsPixel(Rectangle rect1, Color[] data1, //rect2 ball rect
                                     Rectangle rect2, Color[] data2)
        {
            int test = rect1.Bottom;
            int test1 = rect1.Left;
            int test2 = rect1.Right;

            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            for (int y = top; y < bottom; y++)
                for (int x = left; x < right; x++)
                {
                    Color colour1 = data1[(x - rect1.Left) +
                                            (y - rect1.Top) * rect1.Width];
                    Color colour2 = data2[(x - rect2.Left) +
                                            (y - rect2.Top) * rect2.Width];
                    if (colour1.A != 0 && colour2.A != 0)
                    {
                        BallAlreadyJump = false;
                        Color newcolour2 = data2[(x - 1 - rect2.Left) +
                                            (y - rect2.Top) * rect2.Width];
                        if (y - 1 - rect2.Top < 0)
                            return touched = true;
                        
                        //null reff
                        Color newcolour3 = data2[(x - rect2.Left) +
                                            (y - 1 - rect2.Top) * rect2.Width];

                         if (newcolour2.A != 0 && colour1.A != 0)
                        {
                            if (newcolour3.A != 0 && colour1.A != 0)
                            {
                                for (int i = rect1.Bottom; i < rect1.Bottom + rect1.Width; i++)
                                {
                                    Color colour4 = data1[(i - rect1.Bottom) * rect1.Width];
                                    if (colour4.A!=0)
                                        down = true;
                                }    
                                forward = true;
                                return touched = true;                             
                            }
                            else
                            {   
                                forward = true;
                                down = false;
                                return touched = true;
                            }
                        }
                        else
                        {
                            if (newcolour3.A != 0 && colour1.A != 0)
                            {
                                for (int i = rect1.Bottom; i < rect1.Bottom + rect1.Width; i++)
                                {
                                    Color colour4 = data1[(i - rect1.Bottom) * rect1.Width];
                                    if (colour4.A != 0)
                                        down = true;
                                }    
                                forward = false;
                                return touched = true;
                            }
                            else
                            {
                                forward = false;
                                down = false;
                                return touched = true;
                            }
                        }
                    }
                }
            return touched = false;
        }
    }
}
