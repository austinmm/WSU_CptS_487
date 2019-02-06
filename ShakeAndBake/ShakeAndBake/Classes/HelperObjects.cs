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
        static public Path EasyPath = new Path();
        static public Path MediumPath = new Path();
        static public Path HardPath = new Path();
        static public Path BossPath = new Path();
        static EnemyPaths()
        {
            //Read in the different paths from file.
        }
    }
    
    public static class ProjectilePaths
    {
        static public Path EasyPath = new Path();
        static public Path MediumPath = new Path();
        static public Path HardPath = new Path();
        static public Path BossPath = new Path();
        static ProjectilePaths()
        {
            //Read in the different paths from file.
        }
    }
    public enum ProjectileTypes { Bullet, Rocket, FireBall }
}
