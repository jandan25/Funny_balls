using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bullizen
{
    class Level1 
    {
        public Ball SoccerBall;
        private Panel Panel, EndBlock;
        private GUI gui;

        Texture2D cursorTexture;
        Texture2D background;
        Rectangle cursorRectangle;
        Color[] cursorTextureData;
        public bool finished = false;
        MouseState mouse;
        //Animation sprite; //animation
        public void LoadContent(ContentManager newcontent)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            gui = new GUI();
            gui.LoadContent(newcontent);
            SoccerBall = new Ball(newcontent.Load<Texture2D>("GameObjText/soccerball"), new Vector2(70, 30), 0.8f);
            Panel = new Panel(newcontent.Load<Texture2D>("GameObjText/plane"), new Vector2(50, 600));

            EndBlock = new Panel(newcontent.Load<Texture2D>("GameObjText/endblock"), new Vector2(1100, 500));


            cursorTexture = newcontent.Load<Texture2D>("GameObjText/star");
            background = newcontent.Load<Texture2D>("LevelText/level1");
            cursorTextureData = new Color[cursorTexture.Width * cursorTexture.Height];
            cursorTexture.GetData(cursorTextureData);
            //sprite = new Animation(newcontent.Load<Texture2D>("GameObjText/bowlingball"), new Vector2(600, 600), 47, 44);
        }
        public void Update(GameTime gameTime)
        {
            if (SoccerBall.IntersectsPixel(SoccerBall.BallRectangle, SoccerBall.BallTextureData, Panel.PanelRectangle, Panel.PanelTextureData))
                SoccerBall.Update(gameTime);
            else if (EndBlock.IntersectsPixel(SoccerBall.BallRectangle, SoccerBall.BallTextureData, EndBlock.PanelRectangle, EndBlock.PanelTextureData))
            {
                SoccerBall.Update(gameTime);
                finished = true;
            }
            else SoccerBall.Update(gameTime);
            if (Panel.IntersectsPixel(cursorRectangle, cursorTextureData, Panel.PanelRectangle, Panel.PanelTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !SoccerBall.started)
            {
                float x, y;
                x = Panel.PanelPosition.X - mouse.X;
                y = Panel.PanelPosition.Y - mouse.Y;
                Panel.PanelPosition.X = Mouse.GetState().X + x;
                Panel.PanelPosition.Y = Mouse.GetState().Y + y;
            }
            if (gui.Start.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Start.ButtonRectangle, gui.Start.ButtonTextureData)
             && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                SoccerBall.started = true;
            }
            if (gui.Stop.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Stop.ButtonRectangle, gui.Stop.ButtonTextureData)
                && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                SoccerBall.started = false;
            }
            if (gui.Hint.IntersectsPixel(cursorRectangle, cursorTextureData, gui.Hint.HRectangle, gui.Hint.HTextureData)
                && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && SoccerBall.started == false)
            {
                MessageBox.Show("Используй наклонный блок для направления мяча.\n" +
                    "Пол также можно использовать для прохождения уровня!", "Подсказка #1", MessageBoxButtons.OK);
            }
            Panel.Update(gameTime);
            EndBlock.Update(gameTime);
            gui.Update(gameTime);

            //sprite.Update(gameTime);

            cursorRectangle = new Rectangle(mouse.X - (cursorTexture.Width / 2),
                mouse.Y - (cursorTexture.Height / 2), cursorTexture.Width, cursorTexture.Height);
            mouse = Mouse.GetState();
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
         
            SoccerBall.Draw(spriteBatch);
            Panel.Draw(spriteBatch);
            EndBlock.Draw(spriteBatch);
            gui.Draw(spriteBatch);

            //sprite.Draw(spriteBatch);

            spriteBatch.Draw(cursorTexture, cursorRectangle, Color.White);
        }
    }
}
