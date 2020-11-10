using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DuckHunt
{
    class Target : GameObject
    {
        public int score;
        private Random random;
        public int dead;
        public int outOfBounds;
        public SoundEffectInstance death;
        
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

            sprites[0] = content.Load<Texture2D>("TargetGreen");
            sprites[1] = content.Load<Texture2D>("TargetBlue");
            sprites[2] = content.Load<Texture2D>("TargetRed");
            sprites[3] = content.Load<Texture2D>("TargetOrange");

            death = content.Load<SoundEffect>("DeathMoan").CreateInstance();
            death.Play();

            Respawn();

        }

        public override void Update(GameTime gametime)
        {
            Move(gametime);

            if (position.X > GameWorld.GetScreensize().X)
            {
                outOfBounds += 1;
                Respawn();
            }
        }

        public void Respawn()
        {
            int index = random.Next(0, 4);
            sprite = sprites[index];

            velocity = new Vector2(1, 0);
            //random speed from left to right
            speed = random.Next(100, 500);
            //spawn positions on y axis
            position.Y = random.Next(30, 250);
            position.X = 0;

        }

        

        public override void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                score++;
                GameWorld.Destroy(other);
                dead += 1;
                death.Play();
                Respawn();
            }
        }

        



    }

}