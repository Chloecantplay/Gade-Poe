using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadeTask4
{
    [Serializable]
    public class ResourceBuilding : Building
    {

        private int rType;

        public int ResourceType
        {
            get { return rType; }
            set { rType = value; }
        }

        private int rGen;

        public int ResourcesGenerated
        {
            get { return rGen; }
            set { rGen = value; }
        }

        private int rPerRound;

        public int ResourcesPerRound
        {
            get { return rPerRound; }
            set { rPerRound = value; }
        }

        private int rLeft;

        public int ResourcesLeft
        {
            get { return rLeft; }
            set { rLeft = value; }
        }

        Random rand = new Random();

        public bool IsDead { get; set; }

        public int xPos
        {
            get { return base.xpos; }
            set { base.xpos = value; }
        }
        public int yPos
        {
            get { return base.ypos; }
            set { base.ypos = value; }
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

        public int maxHealth
        {
            get { return base.maxHp; }
        }
        public int Hp
        {
            get { return base.hp; }
            set { base.hp = value; }
        }
        public ResourceBuilding(int x, int y, int hp, int team, string symbol, int retype, int regen, int reperround, int releft)
        {
            xPos = x;
            yPos = y;
            base.maxHp = hp;
            base.Team = team;
            base.symbol = symbol;
            ResourceType = retype;
            ResourcesGenerated = regen;
            ResourcesPerRound = reperround;
            ResourcesLeft = releft;
        }

        public override void Destroyed()
        {
            symbol = "X";
            IsDead = true;
        }

        public override string Info()
        {
            string temp = "";
            temp += "Resource building";
            temp += "{" + base.symbol + "}";
            temp += "(" + xpos + "," + ypos + ")";
            temp += "Resource: " + ResourceType + ", \n";
            temp += "Resorces Made: " + ResourcesGenerated + ", " + "Per Round :" + ResourcesPerRound + ", \n";
            temp += "Resources Left: " + ResourcesLeft + ", \n";
            temp += (IsDead ? " This building is destroyed\n" : " This building is fully operational\n");
            return temp;
        }

        public int GenerateResources()
        {
            if (symbol != "X")   //Checks if building has been destroyed
            {
                if (ResourcesLeft > ResourcesPerRound)      //Checks if it has enough resources
                {
                    ResourcesGenerated += ResourcesPerRound;
                    ResourcesLeft -= ResourcesPerRound;
                }
            }
            int resources = ResourcesGenerated;
            return resources;
        }
        public override void BeingAttacked(Unit attacker)
        {

            if (attacker is MeleeUnit)
            {
                base.hp = base.hp - ((MeleeUnit)attacker).attack;
            }
            else if (attacker is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)attacker;
                base.hp = base.hp - (ru.attack - ru.attackRange);
            }

            if (base.hp <= 0)
            {
                Destroyed(); //it does the big deaded
            }
        }
    }
}

