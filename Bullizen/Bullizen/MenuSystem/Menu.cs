using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Bullizen.MenuSystem;

namespace Bullizen
{
    public class Menu
    {
        public List<MenuItem> items { get; set; }
        private SpriteFont font;
        private Texture2D background;

        private int currentItem;
        private KeyboardState oldState;

        public Menu()
        {
            items = new List<MenuItem>();
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyUp(Keys.Enter) && oldState.IsKeyDown(Keys.Enter))
                items[currentItem].OnClick();

            int delta = 0;

            if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                delta = -1;
            if (state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                delta = 1;

            currentItem += delta;
            bool ok = false;
            while (!ok)
            {
                if (currentItem < 0)
                    currentItem = items.Count - 1;
                else if (currentItem > items.Count - 1)
                    currentItem = 0;
                else if (items[currentItem].Active == false)
                    currentItem += delta;
                else ok = true;
            }

            oldState = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            int y = 350;
            foreach (MenuItem item in items)
            {
                Color color = Color.White;
                if (item.Active == false)
                    color = Color.Gray;
                if (item == items[currentItem])
                    color = Color.Blue;
                spriteBatch.DrawString(font, item.Name, new Vector2(1000, y), color);
                y += 60;
            }


            spriteBatch.End();
        }

        public void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("LevelText/menu");
            font = Content.Load<SpriteFont>("LevelText/MenuFont");
        }

    }
}