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
    public class Boss : Enemy
    {
        //Boss Enemy Constructor
        public Boss() : base()
        {
            this.path = EnemyPaths.BossPath;
        }
        public override void Update()
        {
            base.Update();
            //Move Enemy to new position in its set path
        }
    }
}
