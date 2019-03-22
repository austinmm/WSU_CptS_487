using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using ShakeAndBake.Classes.Model.Factories.PathFactory;


namespace ShakeAndBake.Model.GameEntity
{
    public class MidBoss : Enemy
    {
        //Boss Enemy Constructor
        public MidBoss() : base()
        {
            fireRate = Util.randDouble(500, 600);
            this.ProjectileTypes = new System.Collections.Generic.List<ProjectileType>();
            this.ProjectileTypes.Add(ProjectileType.BossWaveProjectile);
            sprite = ShakeAndBakeGame.GetTexture("circle");
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
