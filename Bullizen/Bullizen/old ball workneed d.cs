//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Bullizen
//{
//    class Ball
//    {
//        public Texture2D BallTexture;
//        public Rectangle BallRectangle;

//        public Vector2 BallPosition;
//        public Vector2 BallVelocity;
//        public Vector2 BallSpeedUp;

//        public Vector2 oldBallPosition;
//        public Vector2 oldBallVelocity;

//        public bool BallAlreadyJump;
//        private float bounce, gi;
//        private float count;

//        public Color[] BallTextureData;
//        public bool touched;
//        public bool forward;
//        public bool started = false;
//        float seconds = 0;

//        public Ball(Texture2D newBallTexture, Vector2 newBallPosition, float newbounce)
//        {
//            BallTexture = newBallTexture;
//            BallPosition = newBallPosition;

//            oldBallPosition = BallPosition;
//            oldBallVelocity = BallVelocity;

//            bounce = newbounce;
//            count = bounce * 10;
//            BallAlreadyJump = false;

//            count = 0;
//            started = false;
//            gi = 2;

//            BallSpeedUp.Y = 1;

//            BallTextureData = new Color[BallTexture.Width * BallTexture.Height];
//            BallTexture.GetData(BallTextureData);

//        }

//        public void Update(GameTime gameTime)
//        {
//            if (!started)
//            {
//                BallPosition = oldBallPosition;
//                BallVelocity = oldBallVelocity;
//            }
//            if (touched)
//            {
//                if (forward)
//                {
//                    //if (count > 0)
//                    //{
//                    count *= 1.105f;
//                    count--;
//                    BallVelocity.X *= bounce / 1.2f;
//                    BallVelocity.Y *= -bounce / 1.2f;

//                    //}
//                    //else BallVelocity.Y = 0;
//                }
//                else
//                {
//                    //if (count > 0)
//                    //{
//                    count *= 1.105f;
//                    count--;
//                    BallVelocity.X *= -bounce / 1.2f;
//                    BallVelocity.Y *= -bounce / 1.2f;

//                    //}
//                    //else BallVelocity.Y = 0;
//                }
//            }
//            else if (started)
//                BallAlreadyJump = true;
//            if (BallAlreadyJump == false && forward)
//                BallVelocity.X = -BallVelocity.Y;
//            // BallVelocity.X += 0.20f * 1;
//            if (BallAlreadyJump == false && forward == false)
//                BallVelocity.X = BallVelocity.Y;
//            if (BallAlreadyJump == true)
//                BallVelocity.Y += 0.10f * 1;
//            if (BallPosition.Y + BallTexture.Height >= 710)
//            {
//                BallPosition.Y -= 10f;
//                BallVelocity.Y *= -bounce / 1.2f;
//            }

//            if (BallPosition.X + BallTexture.Width >= 1200)
//            {
//                BallPosition.X -= 10f;
//                BallVelocity.X *= -bounce / 1.2f;
//            }

//            BallPosition += BallVelocity;
//            BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);

//            /*
//             BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);
//            if (started && BallSpeedUp.Y >= 1)
//            {
//                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
//                BallVelocity.Y = BallVelocity.Y + gi * seconds;
//                BallPosition.Y += BallVelocity.Y + gi * (seconds * seconds) / 2;
//                BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);
//                if (BallVelocity.X < 0)
//                {
//                    BallVelocity.X = (BallVelocity.Y + gi * seconds) * (float)Math.Sin(25);
//                    BallPosition.X += BallVelocity.Y * seconds + (gi * seconds * seconds / 2) * seconds * (float)Math.Cos(25);
//                }
//            }
//            if (touched && forward && BallSpeedUp.Y >= 0)
//            {
//                BallVelocity.X = (-BallVelocity.Y + gi * seconds) * (float)Math.Sin(25);
//                BallPosition.X += (BallVelocity.Y + gi * seconds / 2 * seconds * (float)Math.Cos(25);

//                BallSpeedUp.Y = BallVelocity.Y / seconds;
//                BallVelocity.Y = -BallVelocity.Y - gi / seconds;
//                BallPosition.Y += BallVelocity.Y - gi * (seconds * seconds) / 2;
//                touched = false;
//                BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);
//            }
            

//            if (BallPosition.Y + BallTexture.Height >= 710 && BallSpeedUp.Y >= 0)
//            {
//                BallSpeedUp.Y = BallVelocity.Y / seconds;
//                BallVelocity.Y = -BallVelocity.Y - gi / seconds; 
//                BallPosition.Y += BallVelocity.Y - gi * (seconds * seconds) / 2; // 1 seconds за увеличенный прыжок
//            }

//            if (BallPosition.X + BallTexture.Width >= 1200)
//            {
//                BallPosition.X -= 10f;
//                BallVelocity.X *= -bounce / 1.2f;
//            }

//            if (!started)
//            {
//                BallPosition = oldBallPosition;
//                BallVelocity = oldBallVelocity;
//                seconds = 0;
//                BallSpeedUp.Y = 1;
//                touched = false;
//                forward = false;
//            }
//             */
//            /*
//             * if (touched)
//            {
//                if (forward && BallSpeedUp.Y >= 0)
//                {

//                    BallVelocity.X = (BallVelocity.Y + gi * seconds) * (float)Math.Sin(25);
//                    BallPosition.X += BallVelocity.Y * seconds + (gi * seconds * seconds / 2) * seconds * (float)Math.Cos(30);

//                    BallSpeedUp.Y = BallVelocity.Y / seconds;
//                    BallVelocity.Y = -BallVelocity.Y - gi / seconds;
//                    BallPosition.Y += BallVelocity.Y - gi * (seconds * seconds) / 2;

                   
//                    /*ввести в гугле движение тела при отскоке 1 ссылка*

//                    //BallVelocity.X = BallVelocity.X + (float)Math.Sin((double)30) * seconds;
//                    //BallPosition.X = BallPosition.X + BallVelocity.X * seconds + ((float)Math.Sin((double)30) * (seconds * seconds / 2));
//                    ////if (count > 0)
//                    ////{
//                    //count *= 1.105f;
//                    //count--;
//                    //BallVelocity.X *= bounce / 1.2f;
//                    //BallVelocity.Y *= -bounce / 1.2f;

//                    //}
//                    //else BallVelocity.Y = 0;
//                }
//                else
//                {
//                    //if (count > 0)
//                    //{
                    

//                    //}
//                    //else BallVelocity.Y = 0;
//                }
//            }
//            if  (started && BallSpeedUp.Y >= 1)
//            {
//                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
//                BallVelocity.Y = BallVelocity.Y + gi * seconds;
//                BallPosition.Y += BallVelocity.Y + gi * (seconds * seconds) / 2;
//                if (BallVelocity.X > 0)
//                {
//                    BallVelocity.X = (BallVelocity.Y + 9.8f * seconds) * (float)Math.Sin(30);
//                    BallPosition.X += BallVelocity.Y * seconds + (9.8f * seconds * seconds / 2) * seconds * (float)Math.Cos(30);
//                }
//            }

//            if (BallPosition.Y + BallTexture.Height >= 710 && BallSpeedUp.Y >= 0)
//            {
//                BallSpeedUp.Y = BallVelocity.Y / seconds;
//                BallVelocity.Y = -BallVelocity.Y - gi / seconds; 
//                BallPosition.Y += BallVelocity.Y - gi * (seconds * seconds) / 2; // 1 seconds за увеличенный прыжок
//            }

//            if (BallPosition.X + BallTexture.Width >= 1200)
//            {
//                BallPosition.X -= 10f;
//                BallVelocity.X *= -bounce / 1.2f;
//            }

//            if (!started)
//            {
//                BallPosition = oldBallPosition;
//                BallVelocity = oldBallVelocity;
//                seconds = 0;
//                BallAlreadyJump = false;
//                BallSpeedUp.Y = 1;
//            }
//            BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);*/
//        }
//        /*
//        *   if  (started)
//           {
//               seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
//               if (BallSpeedUp.Y >= 1)
//               {
//                   BallVelocity.Y = BallVelocity.Y + gi * seconds;
//                   BallPosition.Y += BallVelocity.Y + gi * (seconds * seconds) / 2;
//               }
//            }
//           if (touched && BallSpeedUp.Y >= 1)
//           {
//               BallSpeedUp.Y = BallVelocity.Y / seconds;
//               BallVelocity.Y = -BallVelocity.Y - gi / seconds;
//               BallPosition.Y += BallVelocity.Y - gi * (seconds * seconds) / 2;
//               if (forward)
//               {
//                   /*ввести в гугле движение тела при отскоке 1 ссылка

//                   //BallVelocity.X = BallVelocity.X + (float)Math.Sin((double)30) * seconds;
//                   //BallPosition.X = BallPosition.X + BallVelocity.X * seconds + ((float)Math.Sin((double)30) * (seconds * seconds / 2));
//                   ////if (count > 0)
//                   ////{
//                   //count *= 1.105f;
//                   //count--;
//                   //BallVelocity.X *= bounce / 1.2f;
//                   //BallVelocity.Y *= -bounce / 1.2f;

//                   //}
//                   //else BallVelocity.Y = 0;
//               }
//               //else
//               //{
//               //    //if (count > 0)
//               //    //{
//               //    count *= 1.105f;
//               //    count--;
//               //    BallVelocity.X *= -bounce / 1.2f;
//               //    BallVelocity.Y *= -bounce / 1.2f;

//               //    //}
//               //    //else BallVelocity.Y = 0;
//               //}
//           }
//           if (BallPosition.Y + BallTexture.Height >= 710 && BallSpeedUp.Y >= 1)
//           {
//               BallSpeedUp.Y = BallVelocity.Y / seconds;
//               BallVelocity.Y = -BallVelocity.Y - gi / seconds; 
//               BallPosition.Y += BallVelocity.Y - gi * (seconds * seconds) / 2;
//           }

//           if (BallPosition.X + BallTexture.Width >= 1200)
//           {
//               BallPosition.X -= 10f;
//               BallVelocity.X *= -bounce / 1.2f;
//           }

//           if (!started)
//           {
//               BallPosition = oldBallPosition;
//               BallVelocity = oldBallVelocity;
//               seconds = 0;
//               BallAlreadyJump = false;
//               BallSpeedUp.Y = 1;
//           }

//           BallRectangle = new Rectangle((int)BallPosition.X, (int)BallPosition.Y, BallTexture.Width, BallTexture.Height);

//        */
//        public void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.Draw(BallTexture, BallRectangle, Color.White);
//        }
//        public bool IntersectsPixel(Rectangle rect1, Color[] data1,
//                                     Rectangle rect2, Color[] data2)
//        {
//            int top = Math.Max(rect1.Top, rect2.Top);
//            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
//            int left = Math.Max(rect1.Left, rect2.Left);
//            int right = Math.Min(rect1.Right, rect2.Right);

//            for (int y = top; y < bottom; y++)
//                for (int x = left; x < right; x++)
//                {
//                    Color colour1 = data1[(x - rect1.Left) +
//                                            (y - rect1.Top) * rect1.Width];
//                    Color colour2 = data2[(x - rect2.Left) +
//                                            (y - rect2.Top) * rect2.Width];
//                    if (colour1.A != 0 && colour2.A != 0)
//                    {
//                        BallAlreadyJump = false;
//                        Color newcolour1 = data1[(x - 5 - rect1.Left) +
//                                            (y - rect1.Top) * rect1.Width];
//                        if (newcolour1.A != 0 && colour2.A != 0)
//                        {
//                            forward = false;
//                            return touched = true;
//                        }
//                        else
//                        {
//                            forward = true;
//                            return touched = true;
//                        }
//                    }
//                }
//            return touched = false;
//        }
//    }
//}
