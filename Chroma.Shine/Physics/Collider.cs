using System;
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
        public Entity Entity { get; }

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

        public void UpdateCollisions(Action<CollisionEventArgs> collisionDetected = null)
        {
            foreach (Collider second in CollisionManager._colliders)
            {
                if (this == second)
                    continue;

                if (!Enabled || !second.Enabled)
                    continue;

                var result = Collide(second);

                if (result.Occured)
                {
                    if (!CollidingWith.Contains(second))
                    {
                        CollidingWith.Add(second);
                        Entity.OnCollisionEnter(second.Entity);
                    }

                    if (!second.CollidingWith.Contains(this))
                    {
                        second.CollidingWith.Add(this);
                        second.Entity.OnCollisionEnter(Entity);
                    }

                    if (collisionDetected != null)
                        collisionDetected(new CollisionEventArgs(this, second));
                }
                else
                {
                    if (CollidingWith.Contains(second))
                    {
                        CollidingWith.Remove(second);
                        Entity.OnCollisionExit(second.Entity);
                    }

                    if (second.CollidingWith.Contains(this))
                    {
                        second.CollidingWith.Remove(this);
                        second.Entity.OnCollisionExit(this.Entity);
                    }
                }
            }
        }
    }
}