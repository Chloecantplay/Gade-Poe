using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadeTask4
{
    [Serializable]
    public class MeleeUnit : Unit
    {
        public bool IsDead { get; set; }

        public int xPos
        {
            get { return base.xPos; }
            set { base.xPos = value; }
        }
        public int yPos
        {
            get { return base.yPos; }
            set { base.yPos = value; }
        }
        public int health
        {
            get { return base.health; }
            set { base.health = value; }
        }
        public int speed
        {
            get { return base.Speed; }
            set { base.Speed = value; }
        }
        public int attack
        {
            get { return base.Attack; }
            set { base.Attack = value; }
        }

        public int attackRange
        {
            get { return base.attackRange; }
            set { base.attackRange = value; }
        }

        public int team
        {
            get { return base.Team; }
            set { base.Team = value; }
        }

        public string symbol
        {
            get { return base.symbol; }
            set { base.symbol = value; }
        }

        public bool isAttack
        {
            get { return base.IsAttack; }
            set { base.IsAttack = value; }
        }
        public Building Spawn
        {
            get { return base.spawn; }
        }
        public int maxHealth
        {
            get { return base.maxHealth; }
        }

        public MeleeUnit(int x, int y, int health, int speed, int attack, int team, string symbol, Building spawn)
        {
            xPos = x;
            yPos = y;
            base.maxHealth = health;
            Speed = speed;
            base.Attack = attack;
            attackRange = 1;
            base.Team = team;
            base.symbol = symbol;
            base.health = health;
            base.spawn = spawn;
        }

        int speedCheck = 1;

        public override void Combat(Unit attacker)
        {

            if (attacker is MeleeUnit)
            {
                base.health = base.health - ((MeleeUnit)attacker).Attack;
            }
            if (attacker is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)attacker;
                base.health = base.health - (ru.attack - ru.attackRange);
            }
            else if (attacker is WizzardUnit)
            {
                WizzardUnit WizardUnit = (WizzardUnit)attacker;

                if (WizardUnit.xPos - 1 == xPos && WizardUnit.yPos + 1 == yPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.yPos + 1 == yPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.yPos + 1 == yPos && WizardUnit.xPos + 1 == xPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.xPos + 1 == xPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.xPos + 1 == xPos && WizardUnit.yPos - 1 == yPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.yPos - 1 == yPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.xPos - 1 == xPos && WizardUnit.yPos - 1 == yPos)
                {
                    health = health - WizardUnit.attack;
                }
                if (WizardUnit.xPos - 1 == xPos)
                {
                    health = health - WizardUnit.attack;
                }
            }
            if (base.health <= 0)
            {
                Death(); //it does the big deaded
            }
        }

        public override void Death()
        {
            symbol = "X";
            IsDead = true;
        }

        public override (Unit, int) EnemyDistance(List<Unit> units)
        {
            int shortest = 100;
            Unit closest = this;

            foreach (Unit u in units)
            {
                if (u is MeleeUnit && u != this)
                {
                    MeleeUnit otherMu = (MeleeUnit)u;
                    int distance = Math.Abs(this.xPos - otherMu.xPos)
                               + Math.Abs(this.yPos - otherMu.yPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherMu;
                    }
                }
                else if (u is RangedUnit && u != this)
                {
                    RangedUnit otherRu = (RangedUnit)u;
                    int distance = Math.Abs(this.xPos - otherRu.xPos)
                               + Math.Abs(this.yPos - otherRu.yPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherRu;
                    }
                }
                else if (u is WizzardUnit && u != this)
                {
                    WizzardUnit otherWu = (WizzardUnit)u;
                    int distance = Math.Abs(this.xPos - otherWu.xPos)
                               + Math.Abs(this.yPos - otherWu.yPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherWu;
                    }
                }

            }
            return (closest, shortest);
        }

        public override void Move(int dir)
        {
            switch (dir)
            {
                case 0: yPos--; break;//North
                case 1: xPos++; break;//East
                case 2: yPos++; break;//South
                case 3: xPos--; break;//West
                default: break;
            }
        }
        public override (Building, int) ClosestBuilding(List<Building> buildings)
        {
            int shortest = 100;
            Building closest = Spawn;
            //Closest Building and Distance                    
            foreach (Building b in buildings)
            {
                if (b is FactoryBuilding)
                {
                    FactoryBuilding fb = (FactoryBuilding)b;
                    int distance = Math.Abs(this.xPos - fb.xPos)
                               + Math.Abs(this.yPos - fb.yPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = fb;
                    }
                }
                else if (b is ResourceBuilding)
                {
                    ResourceBuilding rb = (ResourceBuilding)b;
                    int distance = Math.Abs(this.xPos - rb.xPos)
                               + Math.Abs(this.yPos - rb.yPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = rb;
                    }
                }

            }
            return (closest, shortest);
        }
        public override bool RangeCheck(Unit other)
        {
            int distance = 0;
            int oX = 0;
            int oY = 0;
            if (other is MeleeUnit)
            {
                oX = ((MeleeUnit)other).xPos;
                oY = ((MeleeUnit)other).yPos;
            }
            else if (other is RangedUnit)
            {
                oX = ((RangedUnit)other).xPos;
                oY = ((RangedUnit)other).yPos;
            }
            else if (other is WizzardUnit)
            {
                oX = ((WizzardUnit)other).xPos;
                oY = ((WizzardUnit)other).yPos;
            }
            distance = Math.Abs(xPos - oX) + Math.Abs(yPos - oY);
            if (distance <= attackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string Info()
        {
            string temp = "";
            temp += "Melee Unit: \n";
            temp += "{" + base.symbol + "}\n";
            temp += "(" + base.xPos + "," + base.yPos + ")";
            temp += "Health: " + base.health + "\n    Attack: " + Attack + "\n    Range: " + base.attackRange + "\n   Speed " + Speed + "\n";
            temp += " \n    Current Health:" + health;
            temp += (IsDead ? " This unit is dead" : "This unit is still alive...somehow");
            return temp;
        }

        public int Distance(Unit u)
        {
            int Distance;
            int ySqr;
            int xSqr;

            if (u.GetType() == typeof(MeleeUnit))
            {
                xSqr = (int)Math.Pow(((MeleeUnit)u).xPos - xPos, 2);
                ySqr = (int)Math.Pow(((MeleeUnit)u).yPos - yPos, 2);

                Distance = (int)Math.Sqrt(xSqr + ySqr);

                return Distance;
            }
            else if (u.GetType() == typeof(RangedUnit))
            {
                xSqr = (int)Math.Pow(((RangedUnit)u).xPos - xPos, 2);
                ySqr = (int)Math.Pow(((RangedUnit)u).yPos - yPos, 2);

                Distance = (int)Math.Sqrt(xSqr + ySqr);

                return Distance;
            }
            else if (u.GetType() == typeof(WizzardUnit))
            {
                xSqr = (int)Math.Pow(((WizzardUnit)u).xPos - xPos, 2);
                ySqr = (int)Math.Pow(((WizzardUnit)u).yPos - yPos, 2);

                Distance = (int)Math.Sqrt(xSqr + ySqr);

                return Distance;
            }
            else
            {
                return 0;
            }
        }



    }
}
