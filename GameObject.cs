using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHunt
{
    public abstract class GameObject
    {
        protected Vector2 position;
        protected Texture2D[] sprites;
        protected Texture2D sprite;
        protected float fps;
        protected Vector2 origin;
        protected float speed;
        protected Vector2 velocity;
        protected Vector2 offset;
        protected Color color;

        public Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)(position.X + offset.X),
                       (int)(position.Y + offset.Y),
                       sprite.Width,
                       sprite.Height
                   );
            }
        }


        public abstract void LoadContent(ContentManager content);


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0, origin, 1, SpriteEffects.None, 1f);
            
        }

        public abstract void Update(GameTime gametime);

        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += ((velocity * speed) * deltaTime);
        }

        public abstract void OnCollision(GameObject other);

        public void CheckCollision(GameObject other)
        {
            if (Collision.Intersects(other.Collision))
            {
                OnCollision(other);
            }
        }

    }
}
