using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DuckHunt
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        private Texture2D sprite;
        private Texture2D backgroundTexture;
        private List<GameObject> gameObjects;
        private static List<GameObject> newObjects;
        private static List<GameObject> deleteObjects;
        private Vector2 distance;
        public Vector2 spritePosition;
        private float rotation;
        public Rectangle spriteRectangle;
        public int score;
        public Vector2 scorePosition;
        private SpriteFont scoreFont;
        
        private static Vector2 screensize;
        
        private Texture2D collisionTexture;
        

        //gives us the size of the screen to use for other methods
        public static Vector2 GetScreensize()
        {
            return screensize;
        }


        public static Vector2 Screensize { get; internal set; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            screensize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            //sets score to 0 when game is started
            score = 0;
            scorePosition.X = 10;
            scorePosition.Y = 10;

            gameObjects = new List<GameObject>();
            newObjects = new List<GameObject>();
            deleteObjects = new List<GameObject>();
            gameObjects.Add(new Target());
            gameObjects.Add(new Crosshair());


            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {

            backgroundTexture = Content.Load<Texture2D>("2dField");
            //loading a font to use for the score
            scoreFont = Content.Load<SpriteFont>("scoreFont");
            //loading the correct sprite and giving it a position
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sprite = Content.Load<Texture2D>("Riffle");
            spritePosition = new Vector2(350, 500);

            

            

            // TODO: use this.Content to load your game content here


            collisionTexture = Content.Load<Texture2D>("CollisionTexture");

            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(this.Content);
            }
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //taking the location of the mouse
            MouseState mouse = Mouse.GetState();

            //using the location of the mouse to rotate the sprite
            distance.X = mouse.X - spritePosition.X;
            distance.Y = mouse.Y - spritePosition.Y;

            rotation = (float)Math.Atan2(distance.Y, distance.X);

            // TODO: Add your update logic here

            //lets us use the keyboard and mouse as inputs
            MouseState mouseState = Mouse.GetState();
            KeyboardState state = Keyboard.GetState();

            gameObjects.AddRange(newObjects);
            newObjects.Clear();

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);
                }
            }

            foreach (GameObject go in deleteObjects)
            {
                gameObjects.Remove(go);
                score+= 10;
            }
            deleteObjects.Clear();

            if (state.IsKeyDown(Keys.Enter) && score > 1)
            {
                Initialize();
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// this is the part that draws all the elements on the screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //draws the background in a seperate spritebatch to make sure it is the first thing to me drawn
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);

#if DEBUG
                DrawCollisionBox(go);
#endif
            }

            spriteBatch.Draw(sprite, spritePosition, null, Color.White, rotation, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, "Score: " + score.ToString(), scorePosition, Color.White);
            

            spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void DrawCollisionBox(GameObject go)
        {

            Rectangle topLine = new Rectangle(go.Collision.X, go.Collision.Y, go.Collision.Width, 1);
            Rectangle bottomLine = new Rectangle(go.Collision.X, go.Collision.Y + go.Collision.Height, go.Collision.Width, 1);
            Rectangle rightLine = new Rectangle(go.Collision.X + go.Collision.Width, go.Collision.Y, 1, go.Collision.Height);
            Rectangle leftLine = new Rectangle(go.Collision.X, go.Collision.Y, 1, go.Collision.Height);

            spriteBatch.Draw(collisionTexture, topLine, Color.Red);
            spriteBatch.Draw(collisionTexture, bottomLine, Color.Red);
            spriteBatch.Draw(collisionTexture, rightLine, Color.Red);
            spriteBatch.Draw(collisionTexture, leftLine, Color.Red);
        }

        //creates a gameobject
        public static void Instantiate(GameObject go)
        {
            newObjects.Add(go);
        }

        //deletes a gameobject
        public static void Destroy(GameObject go)
        {
            deleteObjects.Add(go);
        }
    }
}
