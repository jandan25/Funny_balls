using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bullizen.Levels
{
    class Level3
    {
        public Ball TennisBall;
        public Panel Panel, Button, Block, EndBlock;
        private GUI gui;

        Texture2D cursorTexture;
        Texture2D background;
        Rectangle cursorRectangle;
        Color[] cursorTextureData;
        public bool finished = false;
        MouseState mouse;

        public void LoadContent(ContentManager newcontent)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            TennisBall = new Ball(newcontent.Load<Texture2D>("GameObjText/Tennisball"), new Vector2(100, 30), 0.8f);

            Panel = new Panel(newcontent.Load<Texture2D>("GameObjText/plane"), new Vector2(50, 300));
            Button = new Panel(newcontent.Load<Texture2D>("GameObjText/button"), new Vector2(50, 600));
            Block = new Panel(newcontent.Load<Texture2D>("GameObjText/vplane"), new Vector2(1050, 420));


            EndBlock = new Panel(newcontent.Load<Texture2D>("GameObjText/endblock"), new Vector2(1100, 550));

            background = newcontent.Load<Texture2D>("LevelText/level3");
            cursorTexture = newcontent.Load<Texture2D>("GameObjText/star");
            cursorTextureData = new Color[cursorTexture.Width * cursorTexture.Height];

            cursorTexture.GetData(cursorTextureData);
            gui = new GUI();
            gui.LoadContent(newcontent);
        }
        public void Update(GameTime gameTime)
        {
            //сначала с чем сталкиваемся потом что сталкиваем
            if (TennisBall.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, Panel.PanelRectangle, Panel.PanelTextureData))
                TennisBall.Update(gameTime);
            else if (TennisBall.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, Button.PanelRectangle, Button.PanelTextureData))
            {
                TennisBall.Update(gameTime);
                Block.PanelPosition.Y -= 1000; 
            }
            else if (TennisBall.IntersectsPixel(Block.PanelRectangle, Block.PanelTextureData, TennisBall.BallRectangle, TennisBall.BallTextureData))
                TennisBall.Update(gameTime);
            else if (EndBlock.IntersectsPixel(EndBlock.PanelRectangle, EndBlock.PanelTextureData, TennisBall.BallRectangle, TennisBall.BallTextureData))
            {
                TennisBall.Update(gameTime);
                finished = true;
            }
            else TennisBall.Update(gameTime);

            if (Button.IntersectsPixel(Button.PanelRectangle, Button.PanelTextureData, cursorRectangle, cursorTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !TennisBall.started)
            {
                float x, y;
                x = Button.PanelPosition.X - mouse.X;
                y = Button.PanelPosition.Y - mouse.Y;
                Button.PanelPosition.X = Mouse.GetState().X + x;
                Button.PanelPosition.Y = Mouse.GetState().Y + y;
            }
            if (gui.Start.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Start.ButtonRectangle, gui.Start.ButtonTextureData)
            && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                TennisBall.started = true;
            }
            if (gui.Stop.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Stop.ButtonRectangle, gui.Stop.ButtonTextureData)
                && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed || !TennisBall.started)
            {
                TennisBall.started = false;
                Block.PanelPosition.Y = 420;
            }
            if (gui.Hint.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Hint.HRectangle, gui.Hint.HTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && TennisBall.started == false)
            {
                MessageBox.Show("Используй оранжевую кнопку чтобы\n" +
                    "убрать вертикальный блок и довести мяч до финиша.\n", "Подсказка #3", MessageBoxButtons.OK);
            }
            Panel.Update(gameTime);
            Button.Update(gameTime);
            Block.Update(gameTime);

            EndBlock.Update(gameTime);

            gui.Update(gameTime);
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
            Button.Draw(spriteBatch);
            Block.Draw(spriteBatch);
            EndBlock.Draw(spriteBatch);
            gui.Draw(spriteBatch);
            spriteBatch.Draw(cursorTexture, cursorRectangle, Color.White);
        }
    }
}
