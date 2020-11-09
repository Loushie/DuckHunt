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
        //private SoundEffectInstance effect;

        public Target()
        {
            random = new Random();
            offset = Vector2.Zero;
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];

            sprites[0] = content.Load<Texture2D>("Target");
            sprites[1] = content.Load<Texture2D>("Target1");
            sprites[2] = content.Load<Texture2D>("Target2");
            sprites[3] = content.Load<Texture2D>("Target3");

            //effect = content.Load<SoundEffect>("SFX_Powerup_01").CreateInstance();

            Respawn();

        }

        public override void Update(GameTime gametime)
        {
            Move(gametime);

            if (position.X > GameWorld.Screensize.X)
            {
                Respawn();
            }
        }

        public void Respawn()
        {
            int index = random.Next(0, 4);
            sprite = sprites[index];

            velocity = new Vector2(1, 0);
            speed = random.Next(10, 100);
            position.Y = random.Next(30, 150);
            position.X = 0;

            //effect.Play();

        }

        public override void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                GameWorld.Destroy(other);
                Respawn();
            }
        }
    }

}