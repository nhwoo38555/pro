using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.UI
{
    class UILayerGamePause : UILayerAbstract
    {
        protected override void _OnActivate()
        {
            UnityEngine.Cursor.visible = true;
        }
        public override UILayerType uiLayerType
        {
            get
            {
                return UILayerType.GAME_PAUSE;
            }
        }
    }
} 
