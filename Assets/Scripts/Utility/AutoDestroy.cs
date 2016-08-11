using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utility
{
    class AutoDestroy : MonoBehaviour
    {
        public void StartAutoDestroy(float time_)
        {
            base.StartCoroutine(_CoroutineAutoDestroy(time_));
        }
        private IEnumerator _CoroutineAutoDestroy(float time_)
        {
            yield return new WaitForSeconds(time_);
            GameObject.Destroy(base.gameObject);
        }
    }
}
