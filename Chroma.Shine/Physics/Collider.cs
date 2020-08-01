using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Chroma.Physics
{
    public abstract class Collider
    {
        internal List<Collider> CollidingWith { get; }

        public bool Enabled { get; set; }
        public string Tag { get; set; }

        public Vector2 Position { get; set; }
        public Entity Entity { get; set; }

        public Collider(Entity entity, bool startEnabled = true, bool autoRegister = true)
        {
            CollidingWith = new List<Collider>();
            
            Entity = entity;
            Enabled = startEnabled;

            if (autoRegister)
                CollisionManager.RegisterCollider(this);
        }

        public void Destroy()
        {
            for (var i = 0; i < CollidingWith.Count; i++)
            {
                CollidingWith[i].CollidingWith.Remove(this);
                CollidingWith[i].Entity.OnCollisionExit(Entity);
            }
            
            CollidingWith.Clear();
            CollisionManager.UnregisterCollider(this);
        }

        public List<Collider> GetCurrentCollisions()
            => new List<Collider>(CollidingWith);

        public abstract CollisionResult Collide(Collider other);
    }
}