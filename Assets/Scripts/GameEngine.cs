using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GadeTask4
{
    public class GameEngine : MonoBehaviour
    {
        Map map;
        System.Random r = new System.Random();

        public GameObject MeleeT1;
        public GameObject RangedT1;
        public GameObject DeathT1;
        public GameObject MeleeT2;
        public GameObject RangedT2;
        public GameObject DeathT2;
        public GameObject WizzardTeam;
        public GameObject WizzardDeath;
        public GameObject FactoryT1;
        public GameObject FactoryT2;
        public GameObject ResourceT1;
        public GameObject ResourceT2;
        public Text Info;

        private int round;
        int resources = 0;
        int UnitCost = 10;

        // Start is called before the first frame update
        void Start()
        {

            map = new Map(10, 20, 20);
            map.Generate();
            display();
            action();
            round = 1;


        }

        // Update is called once per frame
        void Update()
        {
            display();
            Remove();
            action();
            round++;

        }
        public void display()
        {
            foreach (Building bg in map.Buildings)
            {
                if (bg is FactoryBuilding)
                {
                    FactoryBuilding f = (FactoryBuilding)bg;
                    if (f.team == 0)
                    {
                        if (f.IsDead == false)
                        {
                            GameObject obj = Instantiate(FactoryT1, new Vector2(f.xPos, f.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                        else
                        {
                            GameObject obj = Instantiate(DeathT1, new Vector2(f.xPos, f.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                    }
                    else
                    {
                        if (f.IsDead == false)
                        {
                            GameObject obj = Instantiate(FactoryT2, new Vector2(f.xPos, f.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                        else
                        {
                            GameObject obj = Instantiate(DeathT2, new Vector2(f.xPos, f.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                    }
                }
                else if (bg is ResourceBuilding)
                {
                    ResourceBuilding r = (ResourceBuilding)bg;
                    if (r.team == 0)
                    {
                        if (r.IsDead == false)
                        {
                            GameObject obj = Instantiate(ResourceT1, new Vector2(r.xPos, r.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                        else
                        {
                            GameObject obj = Instantiate(DeathT1, new Vector2(r.xPos, r.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                    }
                    else
                    {
                        if (r.IsDead == false)
                        {
                            GameObject obj = Instantiate(ResourceT2, new Vector2(r.xPos, r.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                        else
                        {
                            GameObject obj = Instantiate(DeathT2, new Vector2(r.xPos, r.yPos), Quaternion.identity);
                            obj.transform.parent = transform;
                        }
                    }
                }
                foreach (Unit u in map.Units)
                {
                    if (u is MeleeUnit)
                    {
                        MeleeUnit m = (MeleeUnit)u;
                        if (m.team == 0)
                        {
                            if (m.IsDead == false)
                            {

                                GameObject obj = Instantiate(MeleeT1, new Vector2(m.xPos, m.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                            else
                            {
                                GameObject obj = Instantiate(DeathT1, new Vector2(m.xPos, m.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                        }
                        else
                        {
                            if (m.IsDead == false)
                            {
                                GameObject obj = Instantiate(MeleeT2, new Vector2(m.xPos, m.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                            else
                            {
                                GameObject obj = Instantiate(DeathT2, new Vector2(m.xPos, m.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                        }
                    }
                    else if (u is RangedUnit)
                    {
                        RangedUnit ru = (RangedUnit)u;
                        if (ru.team == 0)
                        {
                            if (ru.IsDead == false)
                            {

                                GameObject obj = Instantiate(RangedT1, new Vector2(ru.xPos, ru.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                            else
                            {
                                GameObject obj = Instantiate(DeathT1, new Vector2(ru.xPos, ru.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                        }
                        else
                        {
                            if (ru.IsDead == false)
                            {
                                GameObject obj = Instantiate(RangedT2, new Vector2(ru.xPos, ru.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                            else
                            {
                                GameObject obj = Instantiate(DeathT2, new Vector2(ru.xPos, ru.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                        }
                    }
                    else if (u is WizzardUnit)
                    {
                        WizzardUnit w = (WizzardUnit)u;

                        if (w.team == 0)
                        {
                            if (w.IsDead == false)
                            {

                                GameObject obj = Instantiate(WizzardTeam, new Vector2(w.xPos, w.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                            else
                            {
                                GameObject obj = Instantiate(WizzardDeath, new Vector2(w.xPos, w.yPos), Quaternion.identity);
                                obj.transform.parent = transform;
                            }
                        }

                    }

                }
            }
        }
        public void Remove()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        public void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("Map.bat", FileMode.Create, FileAccess.Write, FileShare.None);

            using (fileStream)
            {
                binaryFormatter.Serialize(fileStream, map);


            }
        }

        public void Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("Map.bat", FileMode.Open, FileAccess.Read, FileShare.None);

            using (fileStream)
            {
                map = (Map)formatter.Deserialize(fileStream);


            }
        }

        public void action()
        {
            foreach (Building b in map.Buildings)
            {
                if (b is FactoryBuilding)
                {
                    FactoryBuilding f = (FactoryBuilding)b;
                    if (f.Production_Speed % round == 0 && resources >= UnitCost)
                    {
                        map.Units.Add(f.SpawnUnit());
                    }
                }
                if (b is ResourceBuilding)
                {
                    ResourceBuilding rs = (ResourceBuilding)b;
                    resources = rs.GenerateResources();
                }
            }
            for (int i = 0; i < map.Units.Count; i++)
            {
                if (map.Units[i] is MeleeUnit)
                {
                    MeleeUnit mu = (MeleeUnit)map.Units[i];
                    if (mu.health <= mu.maxHealth * 0.25) // Running Away
                    {
                        mu.Move(r.Next(0, 4));
                    }
                    else
                    {
                        (Unit closest, int distanceTo) = mu.EnemyDistance(map.Units);
                        (Building closestbuilding, int distance) = mu.ClosestBuilding(map.Buildings);
                        if (distanceTo < distance)  //Checks if Unit is closer than building
                        {
                            //Check In Range
                            if (distanceTo <= mu.attackRange)
                            {
                                mu.isAttack = true;
                                mu.Combat(closest);
                                if (mu.IsDead == true)   //Returns resources if unit is dead
                                {
                                    foreach (Building bg in map.Buildings)
                                    {
                                        if (bg is ResourceBuilding)
                                        {
                                            ResourceBuilding rb = (ResourceBuilding)bg;
                                            if (mu.team == rb.team)
                                            {
                                                rb.ResourcesGenerated++;
                                            }
                                        }
                                    }
                                }
                            }
                            else //Move Towards
                            {
                                if (closest is MeleeUnit)
                                {
                                    MeleeUnit closestMu = (MeleeUnit)closest;
                                    if (mu.xPos > closestMu.xPos) //North
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.xPos < closestMu.xPos) //South
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.yPos > closestMu.yPos) //West
                                    {
                                        mu.Move(3);
                                    }
                                    else if (mu.yPos < closestMu.yPos) //East
                                    {
                                        mu.Move(1);
                                    }
                                }
                                else if (closest is RangedUnit)
                                {
                                    RangedUnit closestRu = (RangedUnit)closest;
                                    if (mu.xPos > closestRu.xPos) //North
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.xPos < closestRu.xPos) //South
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.yPos > closestRu.yPos) //West
                                    {
                                        mu.Move(3);
                                    }
                                    else if (mu.yPos < closestRu.yPos) //East
                                    {
                                        mu.Move(1);
                                    }
                                }
                                if (closest is WizzardUnit)
                                {
                                    WizzardUnit closestwu = (WizzardUnit)closest;
                                    if (mu.xPos > closestwu.xPos) //North
                                    {
                                        mu.Move(0);
                                    }
                                    else if (mu.xPos < closestwu.xPos) //South
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.yPos > closestwu.yPos) //West
                                    {
                                        mu.Move(3);
                                    }
                                    else if (mu.yPos < closestwu.yPos) //East
                                    {
                                        mu.Move(1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Check In Range
                            if (distanceTo <= mu.attackRange)
                            {
                                mu.isAttack = true;
                                mu.Combat(closest);
                            }
                            else //Move Towards
                            {
                                if (closestbuilding is FactoryBuilding)
                                {
                                    FactoryBuilding closestfb = (FactoryBuilding)closestbuilding;
                                    if (mu.xPos > closestfb.xPos) //North
                                    {
                                        mu.Move(1);
                                    }
                                    else if (mu.xPos < closestfb.xPos) //South
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.yPos > closestfb.yPos) //West
                                    {
                                        mu.Move(3);
                                    }
                                    else if (mu.yPos < closestfb.yPos) //East
                                    {
                                        mu.Move(4);
                                    }
                                }
                                else if (closestbuilding is ResourceBuilding)
                                {
                                    ResourceBuilding closestrb = (ResourceBuilding)closestbuilding;
                                    if (mu.xPos > closestrb.xPos) //North
                                    {
                                        mu.Move(1);
                                    }
                                    else if (mu.xPos < closestrb.xPos) //South
                                    {
                                        mu.Move(2);
                                    }
                                    else if (mu.yPos > closestrb.yPos) //West
                                    {
                                        mu.Move(3);
                                    }
                                    else if (mu.yPos < closestrb.yPos) //East
                                    {
                                        mu.Move(4);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (map.Units[i] is RangedUnit)
                {
                    RangedUnit ru = (RangedUnit)map.Units[i];
                    if (ru.health <= ru.maxHealth * 0.25) // Running Away is commented out to make for a more interesting battle - and some insta-deaths
                    {
                        ru.Move(r.Next(0, 4));
                    }
                    else
                    {
                        (Unit closest, int distanceTo) = ru.EnemyDistance(map.Units);
                        (Building closestbuilding, int distance) = ru.ClosestBuilding(map.Buildings);
                        if (distanceTo < distance)  //Checks if Unit is closer than building
                        {
                            //Check In Range
                            if (distanceTo <= ru.attackRange)
                            {
                                ru.isAttack = true;
                                ru.Combat(closest);
                                if (ru.IsDead == true)   //Returns resources if unit is dead
                                {
                                    foreach (Building bg in map.Buildings)
                                    {
                                        if (bg is ResourceBuilding)
                                        {
                                            ResourceBuilding rb = (ResourceBuilding)bg;
                                            if (ru.team == rb.team)
                                            {
                                                rb.ResourcesGenerated++;
                                            }
                                        }
                                    }
                                }
                            }
                            else //Move Towards
                            {
                                if (closest is MeleeUnit)
                                {
                                    MeleeUnit closestMu = (MeleeUnit)closest;
                                    if (ru.xPos > closestMu.xPos) //North
                                    {
                                        ru.Move(0);
                                    }
                                    else if (ru.xPos < closestMu.xPos) //South
                                    {
                                        ru.Move(2);
                                    }
                                    else if (ru.yPos > closestMu.yPos) //West
                                    {
                                        ru.Move(3);
                                    }
                                    else if (ru.yPos < closestMu.yPos) //East
                                    {
                                        ru.Move(1);
                                    }
                                }
                                else if (closest is RangedUnit)
                                {
                                    RangedUnit closestRu = (RangedUnit)closest;
                                    if (ru.xPos > closestRu.xPos) //North
                                    {
                                        ru.Move(0);
                                    }
                                    else if (ru.xPos < closestRu.xPos) //South
                                    {
                                        ru.Move(2);
                                    }
                                    else if (ru.yPos > closestRu.yPos) //West
                                    {
                                        ru.Move(3);
                                    }
                                    else if (ru.yPos < closestRu.yPos) //East
                                    {
                                        ru.Move(1);
                                    }
                                }
                                if (closest is WizzardUnit)
                                {
                                    WizzardUnit closestMu = (WizzardUnit)closest;
                                    if (ru.xPos > closestMu.xPos) //North
                                    {
                                        ru.Move(0);
                                    }
                                    else if (ru.xPos < closestMu.xPos) //South
                                    {
                                        ru.Move(2);
                                    }
                                    else if (ru.yPos > closestMu.yPos) //West
                                    {
                                        ru.Move(3);
                                    }
                                    else if (ru.yPos < closestMu.yPos) //East
                                    {
                                        ru.Move(1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Check In Range
                            if (distanceTo <= ru.attackRange)
                            {
                                ru.isAttack = true;
                                ru.Combat(closest);
                            }
                            else //Move Towards
                            {
                                if (closestbuilding is FactoryBuilding)
                                {
                                    FactoryBuilding closestfb = (FactoryBuilding)closestbuilding;
                                    if (ru.xPos > closestfb.xPos) //North
                                    {
                                        ru.Move(1);
                                    }
                                    else if (ru.xPos < closestfb.xPos) //South
                                    {
                                        ru.Move(2);
                                    }
                                    else if (ru.yPos > closestfb.yPos) //West
                                    {
                                        ru.Move(3);
                                    }
                                    else if (ru.yPos < closestfb.yPos) //East
                                    {
                                        ru.Move(4);
                                    }
                                }
                                else if (closestbuilding is ResourceBuilding)
                                {
                                    ResourceBuilding closestrb = (ResourceBuilding)closestbuilding;
                                    if (ru.xPos > closestrb.xPos) //North
                                    {
                                        ru.Move(1);
                                    }
                                    else if (ru.xPos < closestrb.xPos) //South
                                    {
                                        ru.Move(2);
                                    }
                                    else if (ru.yPos > closestrb.yPos) //West
                                    {
                                        ru.Move(3);
                                    }
                                    else if (ru.yPos < closestrb.yPos) //East
                                    {
                                        ru.Move(4);
                                    }
                                }
                            }
                        }

                    }

                }
                else if (map.Units[i] is WizzardUnit)
                {
                    WizzardUnit wu = (WizzardUnit)map.Units[i];
                    if (wu.health <= wu.maxHealth * 0.50) // Wizzards will run away when on half health or less
                    {
                        wu.Move(r.Next(0, 4));
                    }
                    else
                    {
                        (Unit closest, int distanceTo) = wu.EnemyDistance(map.Units);

                        //Check In Range
                        if (distanceTo <= wu.attackRange)
                        {
                            wu.isAttack = true;
                            wu.Combat(closest);
                        }
                        else //Move Towards
                        {
                            if (closest is MeleeUnit)
                            {
                                MeleeUnit closestMu = (MeleeUnit)closest;
                                if (wu.xPos > closestMu.xPos) //North
                                {
                                    wu.Move(0);
                                }
                                else if (wu.xPos < closestMu.xPos) //South
                                {
                                    wu.Move(2);
                                }
                                else if (wu.yPos > closestMu.yPos) //West
                                {
                                    wu.Move(3);
                                }
                                else if (wu.yPos < closestMu.yPos) //East
                                {
                                    wu.Move(1);
                                }
                            }
                            else if (closest is RangedUnit)
                            {
                                RangedUnit closestRu = (RangedUnit)closest;
                                if (wu.xPos > closestRu.xPos) //North
                                {
                                    wu.Move(0);
                                }
                                else if (wu.xPos < closestRu.xPos) //South
                                {
                                    wu.Move(2);
                                }
                                else if (wu.yPos > closestRu.yPos) //West
                                {
                                    wu.Move(3);
                                }
                                else if (wu.yPos < closestRu.yPos) //East
                                {
                                    wu.Move(1);
                                }
                            }

                        }

                    }

                }
            }
            round++;
        }

        public void InfoClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit)
            {
                float mouseX = hit.collider.transform.position.x;
                float mouseY = hit.collider.transform.position.y;

                foreach (Unit u in map.Units)
                {
                    if (u is MeleeUnit)
                    {
                        MeleeUnit mu = (MeleeUnit)u;
                        if (mu.xPos == mouseX && mu.yPos == mouseY)
                        {
                            Info.text = mu.Info();
                        }
                    }
                    else if (u is RangedUnit)
                    {
                        RangedUnit ru = (RangedUnit)u;
                        if (ru.xPos == mouseX && ru.yPos == mouseY)
                        {
                            Info.text = ru.Info();
                        }
                    }
                    else if (u is WizzardUnit)
                    {
                        WizzardUnit wu = (WizzardUnit)u;
                        if (wu.xPos == mouseX && wu.yPos == mouseY)
                        {
                            Info.text = wu.Info();
                        }
                    }
                }

                foreach (Building bg in map.Buildings)
                {
                    if (bg is FactoryBuilding)
                    {
                        FactoryBuilding fb = (FactoryBuilding)bg;
                        if (fb.xPos == mouseX && fb.yPos == mouseY)
                        {
                            Info.text = fb.Info();
                        }
                    }
                    else if (bg is ResourceBuilding)
                    {
                        ResourceBuilding rb = (ResourceBuilding)bg;
                        if (rb.xPos == mouseX && rb.yPos == mouseY)
                        {
                            Info.text = rb.Info();
                        }
                    }
                }
            }

        }
    }
}
