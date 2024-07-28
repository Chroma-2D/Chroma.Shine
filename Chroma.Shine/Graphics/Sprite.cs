using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using Chroma.MemoryManagement;

namespace Chroma.Graphics
{
    public class Sprite : DisposableResource
    {
        protected Texture Texture { get; }

        public Vector2 Position;
        public Vector2 Scale = Vector2.One;
        public Vector2 Origin { get; set; }
        public Vector2 Shearing { get; set; }
        public Size Size { get; }

        public float Rotation;

        public Sprite(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file path provided does not exist.");

            Texture = new Texture(filePath);
            Size = new Size(Texture.Width, Texture.Height);
        }

        public Sprite(Texture texture)
        {
            if (texture.Disposed)
                throw new ArgumentException("Texture you're trying to use was already disposed.", nameof(texture));

            Texture = texture;
            Size = new Size(texture.Width, texture.Height);
        }

        public virtual void Draw(RenderContext context)
        {
            if (Shearing != Vector2.Zero)
            {
                RenderTransform.Push();
                RenderTransform.Shear(Shearing);
            }

            context.DrawTexture(
                Texture, 
                Position, 
                Scale,
                Origin,
                Rotation
             );

            if (Shearing != Vector2.Zero)
            {
                RenderTransform.Shear(Shearing);
            }
        }

        protected override void FreeManagedResources()
        {
            Texture.Dispose();
        }
    }
}