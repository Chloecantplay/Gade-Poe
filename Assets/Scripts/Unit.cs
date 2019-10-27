using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadeTask4
{
    [Serializable]
    public abstract class Unit
    {
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int maxHealth;
        protected int Speed;
        protected int Attack;
        protected int attackRange;
        protected int Team;
        protected string symbol;
        protected bool IsAttack;
        protected Building spawn;

        public abstract void Move(int dir);
        public abstract void Combat(Unit attacker);
        public abstract bool RangeCheck(Unit other);
        public abstract (Unit, int) EnemyDistance(List<Unit> units);
        public abstract (Building, int) ClosestBuilding(List<Building> buildings);
        public abstract void Death();
        public abstract string Info();
    }
}
