using Xamarin.Forms;

namespace Odin.Maths
{
    public class Segment
    {
        public Segment(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;

            Distance = MathHelper.Distance(p1, p2);
            Angle = MathHelper.Angle(P1, P2);
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;

            Normal = new SVector();
        }

        public Point P1 { get; }
        public Point P2 { get; }

        public double Distance { get; }
        public double Angle { get; }
        public SVector Normal { get; }
    }
}
