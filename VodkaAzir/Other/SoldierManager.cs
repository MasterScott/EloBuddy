using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace VodkaXinZhao.Other
{
    class SoldierManager
    {
        public static List<Soldier> activeSoldiers { get; }

        static SoldierManager()
        {
            activeSoldiers = new List<Soldier>();
            GameObject.OnCreate += delegate(GameObject sender, EventArgs args) { Chat.Print("Created soldier, Name: {0}, NetworkId: {1}", sender.Name, sender.NetworkId+""); };
            GameObject.OnCreate += delegate (GameObject sender, EventArgs args) { Chat.Print("Destroyed soldier, Name: {0}, NetworkId: {1}", sender.Name, sender.NetworkId + ""); };
        }
        
        public static void Initialize()
        {

        }

        public int GetNumberOfSoldiers()
        {
            return activeSoldiers.Count;
        }

        /// <summary>
        ///  Returns the soldier closeset to the target
        /// </summary>
        public Soldier GetNearestSoldier(Obj_AI_Base obj)
        {
            if (activeSoldiers.Count == 0)
            {
                return null;
            }
            return activeSoldiers.OrderBy(s => s.Distance(obj)).First();
        }

        /// <summary>
        ///  Returns the soldier fartheset from the target
        /// </summary>
        public Soldier GetFarthestSoldier(Obj_AI_Base obj)
        {
            if (activeSoldiers.Count == 0)
            {
                return null;
            }
            return activeSoldiers.OrderByDescending(s => s.Distance(obj)).First();
        }
        
        /// <summary>
        ///  Returns true, if added soldier, false otherwise.
        /// </summary>
        public bool AddSoldier(Soldier s)
        {
            var contained = activeSoldiers.Contains(s);
            activeSoldiers.Add(s);
            return contained && activeSoldiers.Contains(s);
        }
        
        /// <summary>
        ///  Return true, if removed soldier, false if it the list didn't contain him or couldn't remove him.
        /// </summary>
        public bool RemoveSoldier(Soldier s)
        {
            var contained = activeSoldiers.Contains(s);
            activeSoldiers.Remove(s);
            return contained && !activeSoldiers.Contains(s);
        }
        
        /// <summary>
        ///  Return true, if the list contains the specified soldier.
        /// </summary>
        public bool Contains(Soldier s)
        {
            return activeSoldiers.Contains(s);
        }
    }
}
