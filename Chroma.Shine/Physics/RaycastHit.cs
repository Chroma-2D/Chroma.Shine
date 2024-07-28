using System.Numerics;

namespace Chroma.Physics
{
    public class RaycastHit
    {
        public Vector2 Position { get; internal init; }
        public Collider Collider { get; internal init; }
    }
}