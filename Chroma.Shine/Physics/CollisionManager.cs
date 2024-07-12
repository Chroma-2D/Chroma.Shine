using System;
using System.Collections.Generic;
using System.Linq;

namespace Chroma.Physics
{
    public class CollisionManager
    {
        internal static HashSet<Collider> _colliders;

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

            foreach(Collider first in _colliders)
            {
                first.UpdateCollisions((args) => { CollisionDetected?.Invoke(null, args);});
            }
        }
    }
}