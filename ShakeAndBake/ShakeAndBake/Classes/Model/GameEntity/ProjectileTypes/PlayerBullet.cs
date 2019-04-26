using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Controller.Collision;

namespace ShakeAndBake.Model.GameEntity
{
    public class PlayerBullet : Projectile
    {
        public PlayerBullet(Path path, string texture) : base(path, texture) { }

        public override Projectile Clone()
        {
            PlayerBullet copy = new PlayerBullet(this.path, this.texture);
            copy.Acceleration = this.acceleration;
            copy.HitDamage = this.HitDamage;
            copy.Mass = this.mass;
            copy.PathType = this.pathType;
            copy.Sprite = this.sprite;
            copy.Texture = this.texture;
            copy.Velocity = this.velocity;
            copy.position = this.position;
            copy.Path = this.path;
            copy.IsBouncy = this.isBouncy;
            copy.Density = this.density;
            return copy;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }
        
        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);
            cb.HandlePlayerBulletCollisions(this);
        }
    }
}
