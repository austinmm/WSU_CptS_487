using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace GameClasses
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.7.2
    //https://dotnetcodr.com/2015/05/29/getting-notified-when-collection-changes-with-observablecollection-in-c-net/
    public enum GameBoardConfigs { Stage1, Stage2, Stage3 }
    public static class EnemyPaths
    {
        static public List<Vector2> EasyPath = new List<Vector2>();
        static public List<Vector2> MediumPath = new List<Vector2>();
        static public List<Vector2> HardPath = new List<Vector2>();
        static public List<Vector2> BossPath = new List<Vector2>();
        static EnemyPaths()
        {
            //Read in the different paths from file.
        }
    }
    public static class ProjectilePaths
    {
        static public List<Vector2> EasyPath = new List<Vector2>();
        static public List<Vector2> MediumPath = new List<Vector2>();
        static public List<Vector2> HardPath = new List<Vector2>();
        static public List<Vector2> BossPath = new List<Vector2>();
        static ProjectilePaths()
        {
            //Read in the different paths from file.
        }
    }
    public enum ProjectileTypes { Bullet, Rocket, FireBall }
}
