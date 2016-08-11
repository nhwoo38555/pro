using UnityEngine;
using System.Collections;
using Assets.Scripts.World;
using Assets.Scripts.User;
using Assets.Scripts.UI;
namespace Assets.Scripts.Game
{
    public sealed class GameManager
    {
        private GameManager() { }
        public void HandleOnEnterGame()
        {
            _SpawnPlayer();
            _SpawnMonster();

        }
        public void HandleOnExitGame()
        {

        }
        public void UpdatePre()
        {
            if(true == Input.GetKey(KeyCode.Escape))
            {
                if(UIManager.Instance.currentActiveLayerType == UILayerType.GAMESTAGE)
                {
                    UIManager.Instance.DeactivateUiLayer(UILayerType.GAMESTAGE);
                    UIManager.Instance.ActivateUILayer(UILayerType.GAME_PAUSE);
                }
                else if(UIManager.Instance.currentActiveLayerType == UILayerType.GAME_PAUSE){
                    UIManager.Instance.DeactivateUiLayer(UILayerType.GAME_PAUSE);
                    UIManager.Instance.ActivateUILayer(UILayerType.GAMESTAGE); 

                }
            }
            UserManager.Instance.UpdateCustomPre();
        }
        public void Update()
        {
            UserManager.Instance.UpdateCustom();
        }
        public void UpdatePost()
        {

        }

        private void _SpawnPlayer()
        {
            if(null!=WorldManager.Instance.rootComponet && null!= WorldManager.Instance.rootComponet.playerSpawn)
            {
                GameObject playerGo = WorldManager.Instance.rootComponet.playerSpawn.Spawn();
                UserManager.Instance.SetUserGameObject(playerGo);
                UserManager.Instance.SetPlayerCamera(WorldManager.Instance.rootComponet.playerCamera);
            }
                   
                    }
        private void _SpawnMonster()
        {
            if(null!=WorldManager.Instance.rootComponet && null != WorldManager.Instance.rootComponet.monsterSpawnList)
            {
                foreach(WCMonsterSpawn monsterSpawn in WorldManager.Instance.rootComponet.monsterSpawnList)
                {
                    if(null!= monsterSpawn)
                    {
                        monsterSpawn.MonsterSpawn();
                    }
                }
            }
        }
        #region SINGLETON
        private static GameManager _instance = null;
        public static GameManager Instance { get { if (null != _instance) { return _instance; } else { return (_instance = new GameManager()); } } }
        #endregion
    }
}