using System.Numerics;
using Chroma.Diagnostics.Logging;
using Chroma.Graphics;
using Chroma.Input.EventArgs;
using Chroma.Physics;

namespace Chroma.ShineExample
{
    internal class GameCore : Game
    {
        private Log Log { get; } = LogManager.GetForCurrentAssembly();

        private Entity _e1;
        private Entity _e2;
        private Entity _e3;
        private Entity _e4;

        internal GameCore()
            : base(false)
        {
            _e1 = new Entity {Position = new Vector2(120, 120)};
            _e1.AttachCollider(new RectangleCollider(_e1, 32, 32) {Tag = "_e1"});

            _e2 = new Entity {Position = new Vector2(240, 120)};
            _e2.AttachCollider(new RectangleCollider(_e2, 32, 32) {Tag = "_e2"});

            _e3 = new Entity {Position = Vector2.Zero};
            _e3.AttachCollider(new CircleCollider(_e3, 32) {Tag = "_e3"});

            _e4 = new Entity {Position = new Vector2(256)};
            _e4.AttachCollider(new CircleCollider(_e4, 48)
            {
                Position = new Vector2(256),
                Tag = "_e4"
            });

            CollisionManager.CollisionDetected += (sender, e) =>
            {
                Log.Info($"Collision: {e.First.Tag} <-> {e.Second.Tag}");
            };
        }

        protected override void Update(float delta)
        {
            _e1.Update(delta);
            _e2.Update(delta);
            _e3.Update(delta);
            _e4.Update(delta);
            
            CollisionManager.Update(delta);
        }

        protected override void Draw(RenderContext context)
        {
            _e1.Draw(context);
            _e2.Draw(context);
            _e3.Draw(context);
            _e4.Draw(context);
        }

        protected override void MouseMoved(MouseMoveEventArgs e)
        {
            _e3.Position = e.Position;
        }
    }
}