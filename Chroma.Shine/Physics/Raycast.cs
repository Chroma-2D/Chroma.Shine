using System.Numerics;

namespace Chroma.Physics
{
    public static class Raycast
    {
        public static bool Cast(Vector2 origin, Vector2 direction, out RaycastHit raycastHit, float maxDistance = 500f)
        {
            for (int i = 0; i < maxDistance; i++)
            {
                Vector2 currentpos = origin + (direction * (i + 1));
                foreach (Collider collider in CollisionManager._colliders)
                {
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
                        
                    }
                }
            }

            raycastHit = new RaycastHit { Posiition = Vector2.Zero};
            return false;
        }
    }
}