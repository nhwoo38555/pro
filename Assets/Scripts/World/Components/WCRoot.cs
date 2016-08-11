using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Assets.Scripts.World
{
    public class WCRoot : MonoBehaviour
    {
        public void InstantiateWorldGameObject()
        {
               if(null!= this._worldObject && null == this.worldGo)
            {
                this.worldGo = GameObject.Instantiate(this._worldObject) as GameObject;
            }
               if(null!= this.worldGo)
            {
                this.playerSpawn = this.worldGo.GetComponentInChildren<WCPlayerSpawn>();
                this.playerCamera = this.worldGo.GetComponentInChildren<Camera>();
                this.monsterSpawnList = new List<WCMonsterSpawn>();
                this.monsterSpawnList.AddRange(this.worldGo.GetComponentsInChildren<WCMonsterSpawn>());
        
            }
        }
        
        public void TerminateWorldGameObject()
        {
            if (null != this.monsterSpawnList)
            {
                this.monsterSpawnList.Clear();
                this.monsterSpawnList = null;
            }
            if(null!= this.worldGo)
            {
                GameObject.Destroy(this.worldGo);
                this.worldGo=null;
            }
        }

        [SerializeField]
        private UnityEngine.Object _worldObject = null;

        public GameObject worldGo { get; private set; }
        public WCPlayerSpawn playerSpawn { get; private set; }
        public Camera playerCamera { get; private set; }
        public List<WCMonsterSpawn> monsterSpawnList { get; private set; }

    }
}
