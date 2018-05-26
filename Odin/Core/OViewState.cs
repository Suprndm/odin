using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core
{
    public class OViewState
    {
        public OViewState()
        {
            VisualTreeDepth = 1;
            ZIndex = 1;
            IsVisible = true;
            Opacity = 1;
            IsEnabled = true;
            Scale = 1;
        }

        public float Scale { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool IsEnabled { get; set; }
        public float Opacity { get; set; }
        public decimal VisualTreeDepth { get; set; }
        public decimal ZIndex { get; set; }
        public bool IsVisible { get; set; }
    }
}
