using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHunt
{
    class Crosshair : GameObject
    {
        private Vector2 spawnOffset;
        private Texture2D laser;
        private bool canFire;
        private int fireTrigger;

        public Crosshair()
        {
            canFire = true;
            fireTrigger = 0;
            color = Color.White;

        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("1Cursor");
            }

            sprite = sprites[0];
            //collision box Crosshair
            this.position = new Vector2(GameWorld.GetScreensize().X / 2, GameWorld.GetScreensize().Y - sprite.Height / 2);
            this.origin = new Vector2(sprite.Height / 2, sprite.Width / 2);
            this.offset.X = (-sprite.Width / 2);
            this.offset.Y = -sprite.Height / 2;

            laser = content.Load<Texture2D>("laserGreen03");
        }

        public override void Update(GameTime gametime)
        {
            MouseState mouseState = Mouse.GetState();
            position.X = mouseState.X;
            position.Y = mouseState.Y;

            HandleInput();
            Move(gametime);
            Animate(gametime);
        }




        public void HandleInput()
        {
            velocity = Vector2.Zero;
            MouseState state = Mouse.GetState();

            //shootfunction

            if (state.LeftButton == ButtonState.Pressed & canFire)
            {
               canFire = false;
                GameWorld.Instantiate(new Bullet(laser, new Vector2(position.X + spawnOffset.X, position.Y + spawnOffset.Y)));
            }
            //Firespeed cooldown
            if (!canFire && fireTrigger < 25)
            {
                fireTrigger++;
            }
            else
            {
                canFire = true;
                fireTrigger = 0;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

        }

        public override void OnCollision(GameObject other)
        {
            if (other is Target)
            {
                
                
            }
        }

    }
}
