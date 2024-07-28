using System.Numerics;
using Chroma;
using Chroma.ContentManagement;
using Chroma.ContentManagement.FileSystem;
using Chroma.Graphics;
using Chroma.Graphics.TextRendering.TrueType;

namespace Animation
{
    public class GameCore : Game
    {
        private SpriteSheet _spriteSheet;
        private SpriteSheetAnimation _spriteSheetAnimation; 
            
        public GameCore() : base(new(false, false))
        {
            
        }
        
        protected override IContentProvider InitializeContentPipeline()
        {
            return new FileSystemContentProvider(
                Path.Combine(AppContext.BaseDirectory, "../../../../_common")
            );
        }

        protected override void Initialize(IContentProvider content)
        {
            _spriteSheet = new SpriteSheet(content.Load<Texture>("Textures/Spritesheet.png"), 128, 128);
            _spriteSheet.Position = new Vector2(30, 40);
            _spriteSheetAnimation = new SpriteSheetAnimation(_spriteSheet, 1, 24, 0.5f);
            _spriteSheetAnimation.Repeat = true;
            _spriteSheetAnimation.Play();
        }

        protected override void Update(float delta)
        {
            _spriteSheetAnimation.Update(delta);
        }

        protected override void Draw(RenderContext context)
        {
            _spriteSheetAnimation.Draw(context);
            context.DrawString(TrueTypeFont.Default, $"FPS: {Chroma.Diagnostics.PerformanceCounter.FPS}\nDelta: {Chroma.Diagnostics.PerformanceCounter.Delta}", 0, 0, Color.White);
        }
    }
}