using System;
using System.Numerics;
using Chroma.Graphics;
using Chroma.Physics;

namespace Chroma
{
    public class Entity
    {
        private Color _colliderDebugColor = Color.White;
        
        public Vector2 Position;
        public Sprite Sprite;

        public Collider Collider { get; private set; }

        public void AttachCollider(Collider collider)
        {
            if (Collider != null)
                throw new InvalidOperationException("Collider was already attached to this entity.");

            Collider = collider;
            Collider.Position = Position;
        }

        public void DetachCollider()
        {
            if (Collider == null)
                throw new InvalidOperationException("This entity has no collider.");

            Collider.Destroy();
            Collider = null;
        }

        public virtual void Update(float delta)
        {
            if (Collider != null)
                Collider.Position = Position;
        }

        public virtual void Draw(RenderContext context)
        {
            if (Collider != null)
            {
                if (Collider is RectangleCollider rc)
                {
                    context.Rectangle(
                        ShapeMode.Stroke,
                        rc.Position,
                        rc.Size,
                        _colliderDebugColor
                    );
                }
                else if (Collider is CircleCollider cc)
                {
                    context.Circle(ShapeMode.Stroke,
                        cc.Position,
                        cc.Radius,
                        _colliderDebugColor
                    );
                }
            }
        }

        public virtual void OnCollisionEnter(Entity e)
        {
            if (e.Collider.Tag == "_e1")
                e.Collider.Destroy();
            
            _colliderDebugColor = Color.Red;
        }

        public virtual void OnCollisionExit(Entity e)
        {
            if (Collider.GetCurrentCollisions().Count == 0)
                _colliderDebugColor = Color.White;
        }
    }
}