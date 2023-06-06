using System.Numerics;
using Chroma;
using Chroma.ContentManagement;
using Chroma.ContentManagement.FileSystem;
using Chroma.Graphics;

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

        protected override void LoadContent()
        {
            _spriteSheet = new SpriteSheet(Content.Load<Texture>("Textures/Spritesheet.png"), 128, 128);
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
        }
    }
}