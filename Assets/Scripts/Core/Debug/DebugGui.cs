using UnityEngine;
using System.Collections;
using Surge.Core;
using Surge.Controllers;
using Surge.Actors;

namespace Surge.Core.Debug
{
  
    internal static class DebugVarables
    {
        public const float LINE_HEIGHT = 20;
        public const float SLIDER_WIDTH = 100;
        public const float LABEL_WIDTH = 140;
        public const float VARIABLE_WIDTH = 100;
        public const float GROUP_WIDTH = SLIDER_WIDTH + LABEL_WIDTH + VARIABLE_WIDTH;
        public const float SLIDER_X = LABEL_WIDTH;
        public const float VARIABLE_X = SLIDER_X + SLIDER_WIDTH;
    }

    public class DebugGui: MonoBehaviour {

        private LineRenderer Line;
        private PlayerDebugGui PlayerDbgGui = new PlayerDebugGui();


        // Use this for initialization
    	void Start () {
            Line = gameObject.AddComponent<LineRenderer>();
            Line.SetVertexCount(2);
            Line.SetWidth(0.5f,0.5f);

            GameInfo.GameStateCtrl.GameLoadedEvent += onGameLoaded;
    	}
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}

        void OnGUI () 
        {
            UpdateSliders();
        }

        public void UpdateSliders()
        {
            CameraDebug();
            PlayerDbgGui.UpdatePlayerDebugOverlay();

            if(GameInfo.CurrentPlatform == Platform.COMPUTER)
                MouseDirectionDebug();
            //hSliderValue = GUI.HorizontalSlider(new Rect(25, 225, 100, 30), hSliderValue, 0.0F, 10.0F);
        }

        void CameraDebug()
        {
            CameraController CameraCtrl = GameInfo.CameraCtrl;
            int numberOfRows = 5;

            GUI.BeginGroup(new Rect(25,200, DebugVarables.GROUP_WIDTH, numberOfRows*DebugVarables.LINE_HEIGHT));
                GUI.Label(new Rect(0,0, DebugVarables.GROUP_WIDTH, DebugVarables.LINE_HEIGHT),"Camera Controls");

                //sliders
                CameraCtrl.CurrentCameraYOffset = CreateSliderRow(1,"CurrentCameraYOffset",CameraCtrl.CurrentCameraYOffset,0,100);
                CameraCtrl.VelocityOffsetScale = CreateSliderRow(2, "VelocityOffsetScale", CameraCtrl.VelocityOffsetScale, 0, 1);
                CameraCtrl.MaxOffset = CreateSliderRow(3, "MaxOffset", CameraCtrl.MaxOffset, 0, 50);
                CameraCtrl.EdgeDistance = CreateSliderRow(4, "EdgeDistance", CameraCtrl.EdgeDistance, 0, 50);

            GUI.EndGroup();
        }

        void MouseDirectionDebug()
        {
            if(GameInfo.PlayerCtrl.PlayerPawn == null)
                return;

            InputController inputCtrl = GameInfo.InputCtrl;

            Line.SetPosition(0, GameInfo.PlayerCtrl.PlayerPawn.transform.position);
            Line.SetPosition(1, inputCtrl.MouseHitWorldLocation);
        }

        internal static float CreateSliderRow(int row, string label, float value, float minValue, float maxValue)
        {
            GUI.BeginGroup(new Rect(0,row * DebugVarables.LINE_HEIGHT, DebugVarables.GROUP_WIDTH, DebugVarables.LINE_HEIGHT));
                GUI.Label(new Rect(0,0, DebugVarables.LABEL_WIDTH, DebugVarables.LINE_HEIGHT),label);
                value = GUI.HorizontalSlider( new Rect(DebugVarables.SLIDER_X, 0, DebugVarables.SLIDER_WIDTH, DebugVarables.LINE_HEIGHT),value, minValue, maxValue);
                GUI.Label(new Rect(DebugVarables.VARIABLE_X, 0, DebugVarables.VARIABLE_WIDTH, DebugVarables.LINE_HEIGHT),value.ToString());
            GUI.EndGroup();

            return value;
        }

        #region Events and Notifications
        
        void onGameLoaded()
        {

        }
        
        #endregion  
    }
}
