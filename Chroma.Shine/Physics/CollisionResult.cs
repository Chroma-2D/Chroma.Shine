namespace Chroma.Physics
{
    public class CollisionResult
    {
        public bool Occured { get; }

        internal CollisionResult(bool occured)
        {
            Occured = occured;
        }
    }
}