using System;
using System.Collections.Generic;
using System.Linq;

namespace Chroma.Physics
{
    public class CollisionManager
    {
        private static HashSet<Collider> _colliders;

        static CollisionManager()
        {
            _colliders = new HashSet<Collider>();
        }

        public static event EventHandler<CollisionEventArgs> CollisionDetected;

        public static void RegisterCollider(Collider collider)
        {
            if (!_colliders.Contains(collider))
                _colliders.Add(collider);
        }

        public static void UnregisterCollider(Collider collider)
        {
            if (_colliders.Contains(collider))
                _colliders.Remove(collider);
        }

        public static void Update(float delta)
        {
            if (_colliders.Count <= 1)
                return;

            for (var i = 0; i < _colliders.Count; i++)
            {
                for (var j = 0; j < _colliders.Count; j++)
                {
                    if (i == j)
                        continue;

                    var first = _colliders.ElementAt(i);
                    var second = _colliders.ElementAt(j);

                    if (!first.Enabled || !second.Enabled)
                        continue;

                    var result = first.Collide(second);

                    if (result.Occured)
                    {
                        if (!first.CollidingWith.Contains(second))
                        {
                            first.CollidingWith.Add(second);
                            first.Entity.OnCollisionEnter(second.Entity);
                        }

                        if (!second.CollidingWith.Contains(first))
                        {
                            second.CollidingWith.Add(first);
                            second.Entity.OnCollisionEnter(first.Entity);
                        }

                        CollisionDetected?.Invoke(null, new CollisionEventArgs(first, second));
                    }
                    else
                    {
                        if (first.CollidingWith.Contains(second))
                        {
                            first.CollidingWith.Remove(second);
                            first.Entity.OnCollisionExit(second.Entity);
                        }

                        if (second.CollidingWith.Contains(first))
                        {
                            second.CollidingWith.Remove(first);
                            second.Entity.OnCollisionExit(first.Entity);
                        }
                    }
                }
            }
        }
    }
}