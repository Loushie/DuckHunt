using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DuckHunt
{
    class Target : GameObject
    {
        private Random random;
        private bool isHovered;
        private bool isClicked;

        public Target()
        {
            random = new Random();
            offset = Vector2.Zero;
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            sprites[0] = content.Load<Texture2D>("Target");
            

            Respawn();

        }

        public override void Update(GameTime gametime)
        {
            Move(gametime);

            if (position.Y > GameWorld.Screensize.Y)
            {
                Respawn();
            }
        }

        public void Respawn()
        {
            int index = random.Next(0, 1);
            sprite = sprites[index];

            velocity = new Vector2(0, 1);
            speed = random.Next(10, 10);
            //position.X = random.Next(0, (int)GameWorld.Screensize.X - sprite.Width);
            position.X = random.Next(0, 10);
            position.Y = 30;


        }

        public override void OnCollision(GameObject Cursor)
        {

        }
        
            
        
    }

}