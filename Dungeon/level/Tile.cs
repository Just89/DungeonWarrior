using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeon.level
{
    public enum Tile
    {
        //Setting up the things that require a certain action or collision
        Walkable, 
        Collision,
        Door,
        Chest,
        Stair,
    }
}
