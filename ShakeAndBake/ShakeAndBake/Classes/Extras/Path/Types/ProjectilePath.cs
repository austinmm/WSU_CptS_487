using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ShakeAndBake.Extras;

namespace ShakeAndBake.Extras.Paths
{
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
}