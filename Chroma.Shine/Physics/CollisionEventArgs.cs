namespace Chroma.Physics
{
    public class CollisionEventArgs
    {
        public Collider First { get; }
        public Collider Second { get; }

        internal CollisionEventArgs(Collider first, Collider second)
        {
            First = first;
            Second = second;
        }
    }
}