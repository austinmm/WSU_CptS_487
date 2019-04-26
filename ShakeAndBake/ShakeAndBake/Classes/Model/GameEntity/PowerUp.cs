using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Controller.Collision;
using ShakeAndBake.Extras.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeAndBake.Model.GameEntity
{
    public abstract class PowerUp : GameObject
    {
        protected Path path;
        public Path Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        protected PathType pathType;
        public PathType PathType
        {
            get { return this.pathType; }
            set { this.pathType = value; }
        }

        protected string texture;
        public string Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        protected int pointsNeeded;
        public int PointsNeeded
        {
            get { return this.pointsNeeded; }
            set { this.pointsNeeded = value; }
        }

        public override Texture2D Sprite
        {
            get
            {
                if (this.sprite == null)
                {
                    this.sprite = ShakeAndBakeGame.GetTexture(this.texture);
                    this.hitBoxRadius = sprite.Width / 2;
                }
                return this.sprite;
            }
            set
            {
                this.sprite = value;
                this.hitBoxRadius = sprite.Width / 2;
            }
        }

        protected bool powerUpReady;
        public bool PowerUpReady
        {
            get { return this.powerUpReady; }
            set { this.powerUpReady = value; }
        }

        protected bool powerUpActivated;
        public bool PowerUpActivated
        {
            get { return this.powerUpActivated; }
            set { this.powerUpActivated = value; }
        }

        public PowerUp(Path path, string texture)
        {
            this.path = path;
            this.texture = texture;
            this.Sprite = ShakeAndBakeGame.GetTexture(this.texture);
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            if (!this.isInWindow()) { this.powerUpReady = false; }
            if (!this.powerUpReady) { return; }
            cb.UdateObjectPositionWithFunction(this, () =>
            {
                position = this.path.NextPoint();
                if (this.BoundsContains(Player.Instance) || Player.Instance.BoundsContains(this))
                {
                    this.PlaySoundEffect();
                    this.powerUpActivated = true;
                    this.Reset();
                }
                return true;
            });
            //cb.UdateObjectPositionWithFunction(this, () => { position = this.path.NextPoint(); return true; });
        }

        protected abstract void PlaySoundEffect();

        public override void Draw(SpriteBatch spriteBatch) {
            if (this.powerUpReady)
            {
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }

        public virtual void AttemptActivation(int points)
        {
            if(points >= this.pointsNeeded)
            {
                this.powerUpReady = true;
            }
        }

        protected virtual void InitPowerUp()
        {
            this.powerUpReady = false;
            this.path.Reset();
        }

        public virtual void Reset()
        {
            this.powerUpReady = false;
            this.path.Reset();
            this.position = this.path.CurrentPosition(); ;
        }

        public void UpdatePowerUpPoints(int points)
        {
            this.pointsNeeded += points;
        }
    }
}