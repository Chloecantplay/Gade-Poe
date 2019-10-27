using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadeTask4
{
    [Serializable]
    public abstract class Building
    {

        protected int xpos;
        protected int ypos;
        protected int hp;
        protected int maxHp;
        protected int Team;
        protected string symbol;

        public abstract void Destroyed();
        public abstract string Info();
        public abstract void BeingAttacked(Unit attacker);
    }
}
