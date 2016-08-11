using UnityEngine;
using System.Collections;

namespace Assets.Scripts.World
{

    public class WCMonsterSpawn : MonoBehaviour
    {
        public GameObject MonsterSpawn()
        {
            if (null != this._MonsterObject)
            {
                return GameObject.Instantiate(this._MonsterObject, base.transform.position, base.transform.rotation) as GameObject;
            }
            return null;
        }

        [SerializeField]
        private UnityEngine.Object _MonsterObject = null;
    }


}