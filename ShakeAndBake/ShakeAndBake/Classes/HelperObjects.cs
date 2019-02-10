using Microsoft.Xna.Framework;

namespace GameClasses
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.7.2
    //https://dotnetcodr.com/2015/05/29/getting-notified-when-collection-changes-with-observablecollection-in-c-net/
    public enum GameBoardConfigs { Phase1, Phase2, Phase3, Phase4 }
    
    public static class EnemyPaths
    {
        static public Path DefaultPath(Vector2 origin, Vector2 direction)
        {
            return new StraightPath(origin, direction, 1);
        }

        static EnemyPaths()
        {
            //Read in the different paths from file.
        }
    }
    
    public static class ProjectilePaths
    {
        static public Path DefaultPath(Vector2 origin, Vector2 direction)
        {
            return new StraightPath(origin, direction, 1);
        }

        static ProjectilePaths()
        {
            //Read in the different paths from file.
        }
    }
    public enum ProjectileTypes { Bullet, Rocket, FireBall }
}
