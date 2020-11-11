using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DuckHunt
{
    class Target : GameObject
    {
        private Random random;
        public int outOfBounds;
        public int Speed;
        public SoundEffectInstance death;

        public Target()
        {
            random = new Random();
            offset = Vector2.Zero;
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {

            sprites = new Texture2D[5];
            //The 4 sprites + gameover button sprite (sprites[4])
            sprites[0] = content.Load<Texture2D>("TargetGreen");
            sprites[1] = content.Load<Texture2D>("TargetBlue");
            sprites[2] = content.Load<Texture2D>("TargetRed");
            sprites[3] = content.Load<Texture2D>("TargetOrange");
            sprites[4] = content.Load<Texture2D>("RestartButton");

            death = content.Load<SoundEffect>("DeathMoan").CreateInstance();

            Respawn();

        }

        public override void Update(GameTime gametime)
        {
            Move(gametime);

            if (position.X > GameWorld.GetScreensize().X)
            {
                outOfBounds ++;
                //Target respawns until it reaches 5 targets that went out of bounds
                if (outOfBounds < 5)
                {
                    Respawn();
                }
                else
                {
                    Respawn();
                }
            }
        }

        public void Respawn()
        {
            if (outOfBounds < 5)
            {
                int index = random.Next(0, 4);
                sprite = sprites[index];
                //random speed from left to right
                speed = random.Next(50, 300) + Speed;
                //spawn positions on y axis
                position.Y = random.Next(30, 350);
                position.X = 0;
            }
            //if >5 targets go out of bounds, the game ends
            else
            {
                sprite = sprites[4];
                speed = 0;
                position.Y = 200;
                position.X = 150;
            }
            //Makes the sprite move from right to left on the x-axis
            velocity = new Vector2(1, 0);
        }

        public override void OnCollision(GameObject other)
        {
            //Target gets destroyed if it collides with bullet
            if (other is Bullet)
            {
                GameWorld.Destroy(other);
                death.Play();
                // adds 10 to speed if Target is destroyed by Bullet
                Speed += 10;
                //Target respawns if hit by Bullet
                Respawn();
            }
        }

    }

}