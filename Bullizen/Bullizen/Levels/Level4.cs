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
    class Level4
    {
        public Ball TennisBall;
        public Panel Panel, JumpButton, EndBlock;
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
            JumpButton = new Panel(newcontent.Load<Texture2D>("GameObjText/jumpbutton"), new Vector2(50, 600));

            EndBlock = new Panel(newcontent.Load<Texture2D>("GameObjText/endblock"), new Vector2(1100, 110));

            background = newcontent.Load<Texture2D>("LevelText/level4");
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
            else if (TennisBall.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, JumpButton.PanelRectangle, JumpButton.PanelTextureData))
            {
                TennisBall.BallVelocity *= 2f;
                TennisBall.Update(gameTime);
            }
            else if (EndBlock.IntersectsPixel(TennisBall.BallRectangle, TennisBall.BallTextureData, EndBlock.PanelRectangle, EndBlock.PanelTextureData))
            {
                TennisBall.Update(gameTime);
                finished = true;
            }
            else TennisBall.Update(gameTime);

            if (JumpButton.IntersectsPixel(cursorRectangle, cursorTextureData, JumpButton.PanelRectangle, JumpButton.PanelTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !TennisBall.started)
            {
                float x, y;
                x = JumpButton.PanelPosition.X - mouse.X;
                y = JumpButton.PanelPosition.Y - mouse.Y;
                JumpButton.PanelPosition.X = Mouse.GetState().X + x;
                JumpButton.PanelPosition.Y = Mouse.GetState().Y + y;
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
                MessageBox.Show("Фиолетовый ускоритель поможет мячику забраться\n" +
                    "выше если его правильно использовать.\n", "Подсказка #4", MessageBoxButtons.OK);
            }
            Panel.Update(gameTime);
            JumpButton.Update(gameTime);

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
            JumpButton.Draw(spriteBatch);
            EndBlock.Draw(spriteBatch);

            gui.Draw(spriteBatch);
            spriteBatch.Draw(cursorTexture, cursorRectangle, Color.White);

        }
    }
}
