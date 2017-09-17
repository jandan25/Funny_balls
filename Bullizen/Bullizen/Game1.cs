using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Bullizen.MenuSystem;
using Bullizen.Levels;
using System.Windows.Forms;

namespace Bullizen
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //cursordds
        // добавить остановку после улета правее или левее, посмотреть ошибку склейки
       
        Level1 lvl_1;
        Level2 lvl_2;
        Level3 lvl_3;
        Level4 lvl_4;
        Level5 lvl_5;
        Menu menu,submenu;
        bool firstlaunch = true;
        GameState gameState;
        Texture2D background;
       // Animation sprite; //animation
        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            lvl_1 = new Level1();
            lvl_2 = new Level2();
            lvl_3 = new Level3();
            lvl_4 = new Level4();
            lvl_5 = new Level5();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menu = new Menu();
            Bullizen.MenuSystem.MenuItem newGame = new Bullizen.MenuSystem.MenuItem("Начать игру");
            Bullizen.MenuSystem.MenuItem exitGame = new Bullizen.MenuSystem.MenuItem("Выйти");
            newGame.Click += newGame_Click;
            exitGame.Click += exitGame_Click;
            menu.items.Add(newGame);
            menu.items.Add(exitGame);

            submenu = new Menu();
            Bullizen.MenuSystem.MenuItem Exp1 = new Bullizen.MenuSystem.MenuItem("Эксперимент_1");
            Bullizen.MenuSystem.MenuItem Exp2 = new Bullizen.MenuSystem.MenuItem("Эксперимент_2");
            Bullizen.MenuSystem.MenuItem Exp3 = new Bullizen.MenuSystem.MenuItem("Эксперимент_3");
            Bullizen.MenuSystem.MenuItem Exp4 = new Bullizen.MenuSystem.MenuItem("Эксперимент_4");
            Bullizen.MenuSystem.MenuItem Exp5 = new Bullizen.MenuSystem.MenuItem("Эксперимент_5");
            Bullizen.MenuSystem.MenuItem Back = new Bullizen.MenuSystem.MenuItem("Назад");

            Exp1.Click += Exp1_Click;
            Exp2.Click += Exp2_Click;
            Exp3.Click += Exp3_Click;
            Exp4.Click += Exp4_Click;
            Exp5.Click += Exp5_Click;
            Back.Click += Back_Click;

            submenu.items.Add(Exp1);
            submenu.items.Add(Exp2);
            submenu.items.Add(Exp3);
            submenu.items.Add(Exp4);
            submenu.items.Add(Exp5);
            submenu.items.Add(Back);

            gameState = GameState.Menu;

           // sprite = new Animation(Content.Load<Texture2D>(""), new Vector2(100,100), 47,44); //animation
            base.Initialize();
        }

        void Back_Click(object sender, EventArgs e)
        {
            gameState = GameState.Menu;
        }

        void Exp5_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            lvl_1.finished = true;
            lvl_2.finished = true;
            lvl_3.finished = true;
            lvl_4.finished = true;
        }

        void Exp4_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            lvl_1.finished = true;
            lvl_2.finished = true;
            lvl_3.finished = true;
        }

        void Exp3_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            lvl_1.finished = true;
            lvl_2.finished = true;
        }

        void Exp2_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            lvl_1.finished = true;
        }

        void Exp1_Click(object sender, EventArgs e)
        {
            gameState = GameState.Game;
            menu.items[1].Active = true;
        }

        void exitGame_Click(object sender, EventArgs e)
        {
            this.Exit();
        }

        void newGame_Click(object sender, EventArgs e)
        {
            gameState = GameState.submenu;
            lvl_1.LoadContent(Content);
            lvl_2.LoadContent(Content);
            lvl_3.LoadContent(Content);
            lvl_4.LoadContent(Content);
            lvl_5.LoadContent(Content);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent(Content);
            submenu.LoadContent(Content);
            background = Content.Load<Texture2D>("LevelText/endgame");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (gameState == GameState.Menu)
                menu.Update();
            else if (gameState == GameState.submenu)
                submenu.Update();
            else if (gameState == GameState.Game)
                UpdateGameLogic(gameTime);
            base.Update(gameTime);
        }

        private void UpdateGameLogic(GameTime gameTime)
        {
            if (firstlaunch)
            {
                MessageBox.Show("Приветствуем тебя в игре Веселые мячики, в которой \n" +
                    "тебе предстоит решить задачи которые представлены в игре!\n" +
                    "Игроку необходимо довести мяч до блока Finish.\n\nStart" +
                    "- начало эксперимента;\nStop - остановка;\nTip - подсказка по уровню.\n\nУдачной игры!", "Приветствуем тебя!", MessageBoxButtons.OK);
                firstlaunch = false;
            }

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                gameState = GameState.Menu;
                lvl_1.finished = false;
                lvl_2.finished = false;
                lvl_3.finished = false;
                lvl_4.finished = false;
                lvl_5.finished = false;

            }
            if (!lvl_1.finished)
                lvl_1.Update(gameTime);
            else if (!lvl_2.finished)
                lvl_2.Update(gameTime);
            else if (!lvl_3.finished)
                lvl_3.Update(gameTime);
            else if (!lvl_4.finished)
                lvl_4.Update(gameTime);
            else if (!lvl_5.finished)
                lvl_5.Update(gameTime);
           // sprite.Update(gameTime); //animation
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.Game)
                DrawGame();
            else if (gameState == GameState.submenu)
                submenu.Draw(spriteBatch);
            else
                menu.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        private void DrawGame()
        {
            spriteBatch.Begin();
            if (!lvl_1.finished)
                lvl_1.Draw(spriteBatch, graphics);
            else if (!lvl_2.finished)
                lvl_2.Draw(spriteBatch, graphics);
            else if (!lvl_3.finished)
                lvl_3.Draw(spriteBatch, graphics);
            else if (!lvl_4.finished)
                lvl_4.Draw(spriteBatch, graphics);
            else if (!lvl_5.finished)
                lvl_5.Draw(spriteBatch, graphics);
            else
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
           // sprite.Draw(spriteBatch); // animation
            spriteBatch.End();
        }
    }
    enum GameState
    {
        Game,
        Menu,
        submenu
    }
}
