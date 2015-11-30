using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace VodkaXinZhao.Other
{
    class Soldier
    {
        public GameObject gameObject { get; }
        public int createTime { get; set; }
        public int destroyTime { get; set; }
        public int networkId {
            get { return gameObject.NetworkId;  }
        }

        public float Distance(Obj_AI_Base obj)
        {
            return gameObject.Distance(obj);
        }

        public float Distance(Vector3 pos)
        {
            return gameObject.Distance(pos);
        }

        public float DistanceToPlayer()
        {
            return gameObject.Distance(Player.Instance);
        }

        public Soldier(GameObject obj, int createTime)
        {
            this.gameObject = obj;
            this.createTime = createTime;
        }
    }
}
