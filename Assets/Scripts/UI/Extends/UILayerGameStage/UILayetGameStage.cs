using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.UI
{
    class UILayerGameStage : UILayerAbstract
    {
        protected override void _OnActivate()
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;

            if (null == this._eventHandler)
            {
                this._eventHandler = base.layerCanvas.GetComponentInChildren<UILayerGameStageHandler>();

            }
            if(null!= this._eventHandler)
            {
               this._eventHandler.HandleOnActivateLayer();
            }
        }
        protected override void _OnDeActivate()
        {
            if (null != this._eventHandler)
            {
                this._eventHandler.HandleOnDeActivateLayer();
            }
        }
        UILayerGameStageHandler _eventHandler = null;
        public override UILayerType uiLayerType
        {
            get
            {
                return UILayerType.GAMESTAGE;
            }
        }
    }
}
