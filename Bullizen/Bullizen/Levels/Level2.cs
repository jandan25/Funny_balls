using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bullizen
{
    class Level2
    {
        public Ball TennisBall;
        public Panel Panel, Panel1, Panel2, EndBlock;
        private GUI gui;

        Texture2D cursorTexture;
        Texture2D background;
        Rectangle cursorRectangle;
        Color[] cursorTextureData;
        public bool finished = false;
        MouseState mouse;

        public void LoadContent(ContentManager newcontent)
        {
            gui = new GUI();
            gui.LoadContent(newcontent);

            // Create a new SpriteBatch, which can be used to draw textures.
            TennisBall = new Ball(newcontent.Load<Texture2D>("GameObjText/Tennisball"), new Vector2(300, 30), 0.8f);

            Panel = new Panel(newcontent.Load<Texture2D>("GameObjText/plane"), new Vector2(100, 100));
            Panel1 = new Panel(newcontent.Load<Texture2D>("GameObjText/revplane"), new Vector2(50, 600));
            Panel2 = new Panel(newcontent.Load<Texture2D>("GameObjText/plane"), new Vector2(100, 350));

            EndBlock = new Panel(newcontent.Load<Texture2D>("GameObjText/endblock"), new Vector2(600, 660));

            background = newcontent.Load<Texture2D>("LevelText/level2");
            cursorTexture = newcontent.Load<Texture2D>("GameObjText/star");
            cursorTextureData = new Color[cursorTexture.Width * cursorTexture.Height];
            cursorTexture.GetData(cursorTextureData);
        }
        public void Update(GameTime gameTime)
        {
            if (TennisBall.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, Panel.PanelRectangle, Panel.PanelTextureData))
                TennisBall.Update(gameTime);
            else if (TennisBall.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, Panel1.PanelRectangle, Panel1.PanelTextureData))
                TennisBall.Update(gameTime);
            else if (TennisBall.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, Panel2.PanelRectangle, Panel2.PanelTextureData))
                TennisBall.Update(gameTime);
            else if (EndBlock.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, EndBlock.PanelRectangle, EndBlock.PanelTextureData))
            {
                TennisBall.Update(gameTime);
                finished = true;
            }
            else TennisBall.Update(gameTime);

            if (Panel1.IntersectsPixel(cursorRectangle, cursorTextureData, Panel1.PanelRectangle, Panel1.PanelTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !TennisBall.started)
            {
                float x, y;
                x = Panel1.PanelPosition.X - mouse.X;
                y = Panel1.PanelPosition.Y - mouse.Y;
                Panel1.PanelPosition.X = Mouse.GetState().X + x;
                Panel1.PanelPosition.Y = Mouse.GetState().Y + y;
            }

            if (gui.Start.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Start.ButtonRectangle, gui.Start.ButtonTextureData)
            && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                TennisBall.started = true;
            }
            if (gui.Stop.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Stop.ButtonRectangle, gui.Stop.ButtonTextureData)
                && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                TennisBall.started = false;
            }
            if (gui.Hint.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Hint.HRectangle, gui.Hint.HTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && TennisBall.started == false)
            {
                MessageBox.Show("Чтобы довести мяч до финиша используй\n" +
                    "отскок мяча от блоков. Есть только один\n" +
                    "путь до финиша!\n", "Подсказка #2", MessageBoxButtons.OK);
            }
            gui.Update(gameTime);
            Panel.Update(gameTime);
            Panel1.Update(gameTime);
            Panel2.Update(gameTime);          
            EndBlock.Update(gameTime);


            mouse = Mouse.GetState();
            cursorRectangle = new Rectangle(mouse.X - (cursorTexture.Width / 2),
               mouse.Y - (cursorTexture.Height / 2), cursorTexture.Width, cursorTexture.Height);
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
           
            TennisBall.Draw(spriteBatch);
            Panel.Draw(spriteBatch);
            Panel1.Draw(spriteBatch);
            Panel2.Draw(spriteBatch);

            EndBlock.Draw(spriteBatch);
            gui.Draw(spriteBatch);
            spriteBatch.Draw(cursorTexture, cursorRectangle, Color.White);
       
        }
    }
}