using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.Controller
{
    // Central store for textures, fonts, and sounds.
    public class AssetManager
    {
        private ContentManager manager;
        private Dictionary<string, Texture2D> textures;

        public AssetManager(ContentManager manager)
        {
            this.manager = manager;
            textures = new Dictionary<string, Texture2D>();
        }

        public void LoadAll() {
            LoadAndStore("circle");
            LoadAndStore("player");
            LoadAndStore("player_bullet");
            LoadAndStore("enemey_bullet");
        }

        public void LoadAndStore(string name) {
            Console.WriteLine(name);
            StoreTexture(name, manager.Load<Texture2D>(name));
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