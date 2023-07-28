using System.Numerics;
using Chroma;
using Chroma.Graphics;
using Chroma.Input;
using Chroma.Physics;

namespace Raycasting
{
    internal class GameCore : Game
    {
        private Entity _e1;
        private Vector2 _mousePos = Vector2.One;
        private Color _rayOriginColor = Color.Red;

        internal GameCore()
            : base(new(false, false))
        {
            _e1 = new Entity { Position = new Vector2(120, 120) };
            _e1.AttachCollider(new RectangleCollider(_e1, 32, 32) { Tag = "_e1" });
        }

        protected override void Update(float delta)
        {
            _e1.Update(delta);
        }

        protected override void FixedUpdate(float fixedDelta)
        {
            CollisionManager.Update(fixedDelta);
            RaycastHit hit;
            if (Raycast.Cast(new Vector2(300, 350), Vector2.Normalize(_mousePos), out hit))
            {
                Console.WriteLine("Hit!");
            }
        }

        protected override void Draw(RenderContext context)
        {
            _e1.Draw(context);
            context.Rectangle(ShapeMode.Fill, 300, 350, 5, 5, _rayOriginColor);
            context.Line(new Vector2(300, 350), _mousePos, Color.Red);
        }
        
        protected override void MouseMoved(MouseMoveEventArgs e)
        {
            _mousePos = e.Position;
        }
    }
}