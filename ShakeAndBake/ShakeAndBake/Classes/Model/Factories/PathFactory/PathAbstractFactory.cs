using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    abstract class PathAbstractFactory
    {
        public abstract Path Create(Vector2 origin);
    }
}
