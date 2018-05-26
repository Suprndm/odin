﻿namespace Odin.VisualEffects
{
    public class Particule
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float F { get; set; }
        public float VX { get; set; }
        public float VY { get; set; }

        public Particule(float x, float y)
        {
            X = x;
            Y = y;
        }

        public virtual void Update()
        {
            
        }
    }
}
