using System;
using System.Drawing;
using System.Numerics;

namespace Chroma.Physics
{
    public class CircleCollider : Collider
    {
        public float Radius;

        public CircleCollider(Entity entity, float radius) : base(entity)
        {
            Radius = radius;
        }

        public override CollisionResult Collide(Collider other)
        {
            if (other is RectangleCollider rc)
                return new CollisionResult(OverlapsWithRectangle(Position, Radius, rc.Position, rc.Size));
            else if (other is CircleCollider cc)
                return new CollisionResult(OverlapsWithCircle(Position, Radius, cc.Position, cc.Radius));

            return new CollisionResult(false);
        }
        
        internal static bool OverlapsWithCircle(Vector2 c1, float r1, Vector2 c2, float r2)
        {
            var distance = MathF.Pow(c1.X - c2.X, 2) +
                           MathF.Pow(c1.Y - c2.Y, 2);

            return distance <= MathF.Pow(r1 + r2, 2);
        }

        internal static bool OverlapsWithRectangle(Vector2 cp, float cr, Vector2 rp, Size rs)
        {
            var circleDistanceX = MathF.Abs(
                cp.X - rp.X - rs.Width / 2f
            );
            
            var circleDistanceY = MathF.Abs(
                cp.Y - rp.Y - rs.Height / 2f
            );

            if (circleDistanceX > (rs.Width / 2f + cr) ||
                circleDistanceY > (rs.Height / 2f + cr))
            {
                return false;
            }
            else if (circleDistanceX <= rs.Width / 2f ||
                     circleDistanceY <= rs.Height / 2f)
            {
                return true;
            }

            return (MathF.Pow(circleDistanceX - rs.Width / 2f, 2) +
                    MathF.Pow(circleDistanceY - rs.Height / 2f, 2)) <= MathF.Pow(cr, 2);
        }
    }
}