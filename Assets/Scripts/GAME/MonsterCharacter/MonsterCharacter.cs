using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;
using Assets.Scripts.User;
namespace Assets.Scripts.Game
{
    public class MonsterCharacter : MonoBehaviour
    {
        private readonly string ANIMATOR_PARAM_IDLE = "Idle";
        private readonly string ANIMATOR_PARAM_Die = "Die";
        private readonly string ANIMATOR_PARAM_Attack = "Attack";
        void Awake()
        {
            this._animator = base.GetComponentInChildren<Animator>();
            if (null != this._animator)
            {
                this._animator.SetTrigger(ANIMATOR_PARAM_IDLE);
            }
            this._navMeshAgent = base.GetComponentInChildren<NavMeshAgent>();
        }
        void Update()
        {
            _RandomMove();
            _CheckAttack();
            _CheckAttack();
            
        }
        public void Die()
        {
            if (null != this._animator)
            {
                this._animator.SetTrigger(ANIMATOR_PARAM_Die);
            }
            if (null != this._destoryParticleObject)
            {
                ParticleSystem particle = GameObject.Instantiate<ParticleSystem>(this._destoryParticleObject);
                if(null != particle)
                {
                    particle.transform.position = base.transform.position;
                    particle.transform.rotation = base.transform.rotation;
                    particle.Play();
                    AutoDestroy particleAutoDestroy = base.gameObject.AddComponent<AutoDestroy>();
                    if(null != particleAutoDestroy)
                    {
                        particleAutoDestroy.StartAutoDestroy(3f);
                    }
                }
            }
            AutoDestroy enemyAutoDestroy = base.gameObject.AddComponent<AutoDestroy>();
            if(null != enemyAutoDestroy)
            {
                enemyAutoDestroy.StartAutoDestroy(3f);
            }
        }
       private void _RandomMove()
        {
            if(this._nextMoveStartTime <= Time.realtimeSinceStartup)
            {
                Vector3 destPosition = base.transform.position;
                destPosition.x = destPosition.x + UnityEngine.Random.Range(-3f, 3f);
                destPosition.y = destPosition.y + UnityEngine.Random.Range(-3f, 3f);
                if( null != this._navMeshAgent)
                {
                    this._navMeshAgent.SetDestination(destPosition);
                }
                this._nextMoveStartTime = Time.realtimeSinceStartup + UnityEngine.Random.Range(3f, 5f);
            }
        }
        private void _CheckAttack()
        {
            if(this._lastAttackTime + 10f <= Time.realtimeSinceStartup)
            {
                if(Vector3.Distance(UserManager.Instance.userGo.transform.position,base.transform.position) <= 2.5f)
                {
                    if( null != this._animator)
                    {
                        this._animator.SetTrigger(ANIMATOR_PARAM_Attack);
                        Vector3 lookForward = UserManager.Instance.userGo.transform.position - base.transform.position;
                        base.transform.rotation = Quaternion.LookRotation(lookForward.normalized);
                    }
                    this._lastAttackTime = Time.realtimeSinceStartup;
                }
            }
        }
        public void HandlePlayAttack()
        {

            HealthUpdate();
            //UserManager.Instance.health -= 10f;
            //UserManager.Instance.Slider.value = UserManager.Instance.health;
            Debug.Log("Attack");
        }
        public void HealthUpdate()
        {
           //UserManager.Instance._HealthUpdate();
            UserManager.Instance._HealthUpdateImage();
        }
        [SerializeField]
        private ParticleSystem _destoryParticleObject = null;
        private Animator _animator = null;
        private NavMeshAgent _navMeshAgent = null;
        private float _nextMoveStartTime = 0f;
        private float _lastAttackTime = 0f;
        private float healthMinus = 10f;
    }
}
