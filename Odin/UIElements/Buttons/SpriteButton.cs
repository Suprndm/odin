﻿using Odin.Sprites;

namespace Odin.UIElements.Buttons
{
    public class SpriteButton : SimpleButton
    {
        public Sprite _sprite { get; set; }

        public SpriteButton( string name, float x, float y, float width, float height) : base( x, y, height, width)
        {
            _sprite = new Sprite( name, 0, 0, width, height);
            AddChild(_sprite);
        }

        public override void Render()
        {
            
        }
    }
}
