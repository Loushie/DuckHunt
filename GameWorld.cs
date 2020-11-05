using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DuckHunt
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Texture2D sprite;
        private Rectangle rectangle;
        private List<GameObject> gameObjects;
        private Vector2 distance;
        public Vector2 spritePosition;
        private Vector2 spriteOrigin;
        private float rotation;
        public Rectangle spriteRectangle;
        private Vector2 spriteVelocity;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sprite = Content.Load<Texture2D>("Riffle");
            spritePosition = new Vector2(350, 500);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            IsMouseVisible = true;

            distance.X = mouse.X - spritePosition.X;
            distance.Y = mouse.Y - spritePosition.Y;

            rotation = (float)Math.Atan2(distance.Y, distance.X);


            /*
             * 
             * Vector2 direction = spritePosition - spriteOrigin;
            direction.Normalize();
            float rotationInRadians = (float)Math.Atan2((double)direction.Y,
                                         (double)direction.X) + MathHelper.PiOver2;


             spriteRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, sprite.Width, sprite.Height);
            spritePosition = spriteVelocity + spritePosition;
            spriteOrigin = new Vector2(spriteRectangle.Width / 2, spriteRectangle.Height / 2);
            */
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(sprite, spritePosition, null, Color.White, rotation, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
