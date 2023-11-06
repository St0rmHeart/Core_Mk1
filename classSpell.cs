using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// Заклинание
    /// </summary>
    public class Spell : BasicDevEntity
    {
        public Spell(string name) : base(name) { }
        protected int manaCostFire;
        protected int manaCostEarth;
        protected int manaCostAir;
        protected int manaCostWater;
    }
}
