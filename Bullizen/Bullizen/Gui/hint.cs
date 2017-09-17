using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullizen
{
    class Hint
    {
        public Texture2D HTexture;
        public Rectangle HRectangle;
        public Vector2 HPosition;
        public Color[] HTextureData;
        public bool touched;

    public Hint (Texture2D newHTexture, Vector2 newHPosition)
        {
            HTexture = newHTexture;
            HPosition = newHPosition;

            HTextureData = new Color[HTexture.Width * HTexture.Height];
            HTexture.GetData(HTextureData);

        }
 public void Update(GameTime gameTime)
        {
            HRectangle = new Rectangle ((int)HPosition.X,(int)HPosition.Y, HTexture.Width, HTexture.Height);
        }
public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(HTexture,HRectangle,Color.White);
        }
public bool IntersectsPixel(Rectangle rect1, Color[] data1,
                                     Rectangle rect2, Color[] data2)
        {
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
                        return touched = true;
                }
            return touched = false;
        }
    }
}
