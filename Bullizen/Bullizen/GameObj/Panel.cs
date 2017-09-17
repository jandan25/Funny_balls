using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullizen
{
    class Panel
    {
        public Texture2D PanelTexture;
        public Rectangle PanelRectangle;
        public Vector2 PanelPosition;
        public Color[] PanelTextureData;

        public Panel(Texture2D newPanelTexture, Vector2 newPanelPosition)
        {
            PanelTexture = newPanelTexture;
            PanelPosition = newPanelPosition;

            PanelTextureData = new Color[PanelTexture.Width * PanelTexture.Height];
            PanelTexture.GetData(PanelTextureData);

        }

        public void Update(GameTime gameTime)
        {
            PanelRectangle = new Rectangle ((int)PanelPosition.X,(int)PanelPosition.Y, PanelTexture.Width, PanelTexture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PanelTexture,PanelRectangle,Color.White);
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
                        return true;
                }
            return false;
        }
    }
}
