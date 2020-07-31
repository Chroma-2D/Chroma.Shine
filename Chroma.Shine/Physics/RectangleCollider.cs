using System;
using System.Drawing;
using System.Numerics;

namespace Chroma.Physics
{
    public class RectangleCollider : Collider
    {
        public Size Size;

        public RectangleCollider(Entity entity, int width, int height) : base(entity)
        {
            Size = new Size(width, height);
        }

        public override CollisionResult Collide(Collider other)
        {
            if (other is RectangleCollider rc)
            {
                if (Position.X < rc.Position.X + rc.Size.Width &&
                    Position.X + Size.Width > rc.Position.X &&
                    Position.Y < rc.Position.Y + rc.Size.Height &&
                    Position.Y + Size.Height > rc.Position.Y)
                {
                    var thisCenter = new Vector2(
                        Position.X + Size.Width / 2f,
                        Position.Y + Size.Height / 2f
                    );

                    var otherCenter = new Vector2(
                        rc.Position.X + rc.Size.Width / 2f,
                        rc.Position.Y + rc.Size.Height / 2f
                    );

                    var dx = rc.Position.X - Position.X;
                    if (thisCenter.X < otherCenter.X)
                    {
                        dx -= Size.Width;
                    }
                    else
                    {
                        dx += rc.Size.Width;
                    }

                    var dy = rc.Position.Y - Position.Y;
                    if (thisCenter.Y < otherCenter.Y)
                    {
                        dy -= Size.Height;
                    }
                    else
                    {
                        dy += rc.Size.Height;
                    }

                    if (MathF.Abs(dx) < MathF.Abs(dy))
                        return new CollisionResult(true);

                    return new CollisionResult(true);
                }

                return new CollisionResult(false);
            }
            else if (other is CircleCollider cc)
            {
                return new CollisionResult(
                    CircleCollider.OverlapsWithRectangle(cc.Position, cc.Radius, Position, Size)
                );
            }

            return new CollisionResult(false);
        }
    }
}