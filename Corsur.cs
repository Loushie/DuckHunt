using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace DuckHunt
{
    
    class Cursor : GameObject
    {
        private bool isHovered;
        private bool isClicked;

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject other)
        {
            throw new NotImplementedException();
        }

        public void shoot()
        {

            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var rectangle = new Rectangle(mousePoint.X, mousePoint.Y, this.sprite.Width, this.sprite.Height);

            if (rectangle.Contains(mousePoint))
            {
                if ((isHovered = true) && (isClicked = mouseState.LeftButton == ButtonState.Pressed))
                {
                    
                }
                
                
            }
            else
            {
                isHovered = false;
                isClicked = false;
            }

        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }
    }

}
