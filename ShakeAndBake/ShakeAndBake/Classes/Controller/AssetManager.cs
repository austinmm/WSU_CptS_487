using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.Controller
{
    // Central store for textures, fonts, and sounds.
    public class AssetManager
    {
        private Dictionary<string, Texture2D> textures;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture2D>();
        }
        
        public void StoreTexture(string name, Texture2D texture)
        {
            textures[name] = texture;
        }

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }
    }
}