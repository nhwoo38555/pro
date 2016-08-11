using UnityEngine;
using System;
using Assets.Scripts.World;
using Assets.Scripts.Game;
using UnityEngine.UI;

namespace Assets.Scripts.User
{
    public sealed class UserManager 
    {
        private const float PLAYER_HEIGHT = 2.0f;
        private const float TURN_SPEED = 100.0f;
        private UserManager() { }
        public void SetPlayerCamera(Camera camera_)
        {
            if (null != camera_)
            {
                if (null != this.userGo)
                {
                    camera_.transform.SetParent(this.userGo.transform);
                    camera_.transform.localRotation = Quaternion.identity;
                    camera_.transform.localPosition = new Vector3(0f, PLAYER_HEIGHT, 0f);
                }

                this.playerCamera = camera_;
               
            }
        }
        public void SetUserGameObject(GameObject UserGameObject_)
        {
            this.userGo = UserGameObject_;
            if(null!= this.userGo)
            {
                this._navMeshAgent = this.userGo.GetComponentInChildren<NavMeshAgent>();
                //여기에 컴포넌트 넣고 
                //this.healthbar = this.userGo.GetComponentInChildren<Slider>();
                //this.Imagebar = this.userGo.GetComponentInChildren<Image>();
               // this.bgImage = this.userGo.GetComponent<Image>();
                // this.JoyImage = this.userGo.GetComponentInChildren<RawImage>();
            }
        }
        public void UpdateCustomPre()
        {
            _UpdateMoveInput();
        }
        public void UpdateCustom()
        {
            _UpdateRotation();
            _UpdateMove();
            _RayCasting();
            
        }
        private void _UpdateMoveInput()
        {
            
            this._moveDirection = Vector3.zero;
            
            if (true == Input.GetKey(KeyCode.W)){
                this._moveDirection += new Vector3(0f, 0f, 1f);
            }
            if (true == Input.GetKey(KeyCode.S))
            {
                this._moveDirection += new Vector3(0f, 0f, -1f);
            }
            if (true == Input.GetKey(KeyCode.D))
            {
                this._moveDirection += new Vector3(1f, 0f, 0f);
            }
            if (true == Input.GetKey(KeyCode.A))
            {
                this._moveDirection += new Vector3(-1f, 0f, 0f);
            }

            this._axisDeltaX = Input.GetAxis("Mouse X");
            this._axisDeltaY = Input.GetAxis("Mouse Y");
            
            if(true == Input.GetMouseButtonDown(0))
            {
                this._fired = true;
            }
            

        }

        private void _UpdateRotation()
        {
            if(this._axisDeltaX !=0f && this.userGo)
            {
                this.userGo.transform.Rotate(Vector3.up, this._axisDeltaX);
            }
            if(this._axisDeltaY !=0f && this.userGo)
            {
                this.playerCamera.transform.Rotate(Vector3.right, -this._axisDeltaY);
            }
        }

        private void _UpdateMove()
        {
            if(null != this._navMeshAgent && this._moveDirection != Vector3.zero)
            {
                Vector3 forword = this._navMeshAgent.transform.TransformDirection(this._moveDirection);
                Vector3 moveOffset = forword.normalized * this._navMeshAgent.speed * Time.deltaTime;
                this._navMeshAgent.Move(moveOffset);
                
            }
        }
        private void _RayCasting()
        {
            if(true == this._fired && null!= this.playerCamera)
            {
                RaycastHit hit;
                Ray ray = new Ray(this.playerCamera.transform.position, this.playerCamera.transform.forward);
                int MonsterLayerMask = 1 << 20;
                if(true == Physics.Raycast(ray, out hit, MonsterLayerMask))
                {
                    if (null != hit.collider)
                    {
                        MonsterCharacter monsterCharacter = hit.collider.GetComponentInChildren<MonsterCharacter>();
                        if(null!= monsterCharacter)
                        {
                            monsterCharacter.Die();
                        }
                    }
                }
                this._fired = false;
            }
        }
        /*public void _HealthUpdate()   
        {
            
            this.health = health - 0.1f;
            this.healthbar.value = health;

        }*/
        public void _HealthUpdateImage()
        {
            this.cur_Health -= 10f;
            this.calc_Health = this.cur_Health / this.max_Health;
            _SetHealthImage(calc_Health);
        }
        private void _SetHealthImage(float myhealth)
        {
            if (myhealth >= 0)
            {
                Imagebar.transform.localScale = new Vector3(myhealth, Imagebar.transform.localScale.y, Imagebar.transform.localScale.z);
            }
        }
        public string userID { get; set; }
        public GameObject userGo { get; private set; }
        public Camera playerCamera { get; private set; }
        private NavMeshAgent _navMeshAgent = null;
        private Vector3 _moveDirection = Vector3.zero;
        private float _axisDeltaX = 0f;
        private float _axisDeltaY = 0f;
        private bool _fired = false;
        private WCMonsterSpawn monsterSpawn;
        //private Slider healthbar { get; set; }
        //private float health = 1f; //silder health 
        private float max_Health = 100f;
        private float cur_Health = 100f;
        private float calc_Health;
        private Image Imagebar { get; set; }
       // private Image bgImage { get; set; }
       // private RawImage JoyImage { get; set; }
        #region
        private static UserManager _instance = null;
        public static UserManager Instance { get { if (null != _instance) { return _instance; } else { return (_instance = new UserManager()); } } }
        #endregion

    }

}