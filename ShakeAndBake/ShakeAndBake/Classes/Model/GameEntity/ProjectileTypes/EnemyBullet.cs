using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Collections.Generic;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Controller.Collision;
using System;

namespace ShakeAndBake.Model.GameEntity
{
    public class EnemyBullet : Projectile
    {
        public EnemyBullet(Path path, string texture) : base(path, texture) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }

        public override Projectile Clone()
        {
            EnemyBullet copy = new EnemyBullet(this.path, this.texture);
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

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);
            cb.HandleEnemyBulletCollisions(this);
        }
    }
}