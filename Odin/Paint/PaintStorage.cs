using System.Collections.Generic;
using SkiaSharp;

namespace Odin.Paint
{
    public class PaintStorage
    {
        private readonly IDictionary<string, SKPaint> _storedPaints = new Dictionary<string, SKPaint>();

        public void DeclarePaint(string paintName, SKPaint paint)
        {
            if (_storedPaints.ContainsKey(paintName))
            {
                _storedPaints[paintName] = paint;
            }
            else
            {
                _storedPaints.Add(new KeyValuePair<string, SKPaint>(paintName, paint));
            }
        }

        public SKPaint GetPaint(string paintName)
        {
            return _storedPaints[paintName];
        }

    }
}
