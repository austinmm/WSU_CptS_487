using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.GameEntity;
using System.Collections.Generic;

namespace ShakeAndBake.Model.Factories.PowerUpFactory
{
    public abstract class PowerUpAbstractFactory
    {
        protected string jsonObject;
        protected System.IO.StreamReader reader;
        protected PathAbstractFactory factory;

        public abstract PowerUp Create();
        protected virtual void InitReader(string basename)
        {
            string dirname = "../../../../JSON/PowerUpTypes/";
            this.reader = new System.IO.StreamReader(dirname + basename);
            this.jsonObject = this.reader.ReadToEnd();
        }

        protected abstract PowerUp JsonToPowerUp();
    }

    public class BombPowerUpFactory : PowerUpAbstractFactory
    {
        public BombPowerUpFactory()
        {
            base.InitReader("Bomb.json");
        }
        protected override PowerUp JsonToPowerUp()
        {
            return JsonConvert.DeserializeObject<BombPowerUp>(this.jsonObject);
        }

        public override PowerUp Create()
        {
            PowerUp powerUp = this.JsonToPowerUp();
            this.factory = PathFactoryProducer.ProduceFactory(powerUp.PathType);
            powerUp.Position = new Vector2(10, 150);
            Path path = this.factory.Create(powerUp.Position, new Vector2(1, 0), (float)powerUp.Velocity);
            powerUp.Path = path;
            return powerUp;
        }
    }
}
