using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullizen
{
    class GUI
    {
        public Bttn Start, Stop;
        public Hint Hint;
        public void LoadContent(ContentManager newcontent)
        {
            Hint = new Hint(newcontent.Load<Texture2D>("GuiText/Hint"), new Vector2(1180, 10));
            Start = new Bttn(newcontent.Load<Texture2D>("GuiText/start"), new Vector2(1000, 650));
            Stop = new Bttn(newcontent.Load<Texture2D>("GuiText/stop"), new Vector2(1140, 650));
        }
        public void Update(GameTime gameTime)
        {
            Hint.Update(gameTime);
            Start.Update(gameTime);
            Stop.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Hint.HTexture, Hint.HRectangle, Color.White);
            spriteBatch.Draw(Start.ButtonTexture, Start.ButtonRectangle, Color.White);
            spriteBatch.Draw(Stop.ButtonTexture, Stop.ButtonRectangle, Color.White);
        }
    }
}
