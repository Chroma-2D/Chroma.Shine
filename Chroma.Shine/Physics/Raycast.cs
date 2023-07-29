using System;
using System.Linq;
using System.Numerics;

namespace Chroma.Physics
{
    public static class Raycast
    {
        public static bool Cast(Vector2 origin, Vector2 direction, out RaycastHit raycastHit, float maxDistance = 500f, string[] skipTags = null)
        {
            if (skipTags == null)
                skipTags = new String[] { };
            Vector2 currentpos = origin;
            for (int i = 0; i < maxDistance; i++)
            {
                currentpos += direction;
                foreach (Collider collider in CollisionManager._colliders)
                {
                    if (skipTags.Contains(collider.Tag)) continue;
                    if (collider is RectangleCollider rc)
                    {
                        if (currentpos.X >= rc.Position.X &&
                            currentpos.X <= rc.Position.X + rc.Size.Width &&
                            currentpos.Y >= rc.Position.Y &&
                            currentpos.Y <= rc.Position.Y + rc.Size.Height)
                        {
                            raycastHit = new RaycastHit {Posiition = currentpos, Collider = rc};
                            return true;
                        }
                    }
                    else if (collider is CircleCollider cc)
                    {
                        //internal static bool OverlapsWithRectangle(Vector2 cp, float cr, Vector2 rp, Size rs)
                        //return new CollisionResult(OverlapsWithRectangle(Position, Radius, rc.Position, rc.Size)
                        var circleDistanceX = MathF.Abs(
                            cc.Position.X - currentpos.X - 0.5f
                        );
            
                        var circleDistanceY = MathF.Abs(
                            cc.Position.Y - currentpos.Y - 0.5f
                        );

                        if (circleDistanceX > (0.5f + cc.Radius) ||
                            circleDistanceY > (0.5f + cc.Radius))
                        {
                            continue;
                        }
                        else if (circleDistanceX <= 0.5f ||
                                 circleDistanceY <= 0.5f)
                        {
                            raycastHit = new RaycastHit { Posiition = currentpos, Collider = cc};
                            return true;
                        }

                        if ((MathF.Pow(circleDistanceX - 0.5f, 2) +
                             MathF.Pow(circleDistanceY - 0.5f, 2)) <= MathF.Pow(cc.Radius, 2))
                        {
                            raycastHit = new RaycastHit { Posiition = currentpos, Collider = cc };
                            return true;
                        }
                    }
                }
            }

            raycastHit = new RaycastHit { Posiition = Vector2.Zero};
            return false;
        }
    }
}