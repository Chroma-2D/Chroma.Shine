using System.Numerics;
using Chroma;
using Chroma.Diagnostics.Logging;
using Chroma.Graphics;
using Chroma.Input;
using Chroma.Physics;

namespace Raycasting
{
    internal class GameCore : Game
    {
        private Log Log { get; } = LogManager.GetForCurrentAssembly();
        
        private Entity _e1, _e2, _e3, _e4;
        private Vector2 _mousePos = Vector2.One;
        private Color _rayOriginColor = Color.Red;

        internal GameCore()
            : base(new(false, false))
        {
            _e1 = new Entity { Position = new Vector2(120, 120) };
            _e1.AttachCollider(new RectangleCollider(_e1, 32, 32) { Tag = "_e1" });

            _e2 = new Entity { Position = new Vector2(220, 120) };
            _e2.AttachCollider(new RectangleCollider(_e1, 32, 32) { Tag = "_e2" });

            _e3 = new Entity { Position = new Vector2(320, 120) };
            _e3.AttachCollider(new CircleCollider(_e3, 32) { Tag = "_e3" });

            _e4 = new Entity { Position = new Vector2(420, 120) };
            _e4.AttachCollider(new RectangleCollider(_e1, 32, 32) { Tag = "_e4" });
        }


        protected override void Update(float delta)
        {
            _e1.Update(delta);
            _e2.Update(delta);
            _e3.Update(delta);
            _e4.Update(delta);
        }

        protected override void FixedUpdate(float fixedDelta)
        {
            CollisionManager.Update(fixedDelta);
            RaycastHit hit;
            if (Raycast.Cast(new Vector2(300, 350), Vector2.Normalize(_mousePos - new Vector2(300, 350)), out hit, Vector2.Distance(_mousePos, new Vector2(300, 350)), new []{"_e4"}))
            {
                Log.Info($"Hit {hit.Collider.Tag} at {hit.Position.X}, {hit.Position.Y}");
                _rayOriginColor = Color.Green;
            }
            else
            {
                _rayOriginColor = Color.Red;
            }
        }

        protected override void Draw(RenderContext context)
        {
            _e1.Draw(context);
            _e2.Draw(context);
            _e3.Draw(context);
            _e4.Draw(context);
            context.Rectangle(ShapeMode.Fill, 300, 350, 5, 5, _rayOriginColor);
            context.Line(new Vector2(300, 350), _mousePos, Color.Red);
        }
        
        protected override void MouseMoved(MouseMoveEventArgs e)
        {
            _mousePos = e.Position;
        }
    }
}