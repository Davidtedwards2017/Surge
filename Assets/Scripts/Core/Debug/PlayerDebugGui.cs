//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using Surge.Actors;
using Surge.Controllers;

namespace Surge.Core.Debug
{
    public class PlayerDebugGui
    {

        private bool bPlayerMassSet = false;
        private bool bPlayerDragSet = false;
        private bool bPlayerTurningRateSet = false;
        private bool bPlayerMaxSpeedSet = false;
        private bool bPlayerThrustAmtSet = false;
        
        private float m_Player_Mass;
        private float m_Player_Drag;
        private float m_Player_TurningRate;
        private float m_Player_MaxSpeed;
        private float m_Player_ThrustAmt;
        
        public float Player_Mass
        { 
            get
            {
                if(!bPlayerMassSet)
                {
                    m_Player_Mass = GameInfo.PlayerCtrl.PlayerPawn.RB.mass;
                    bPlayerMassSet = true;
                }
                return m_Player_Mass;
            }
            set{ m_Player_Mass = value; }
        }
        public float Player_Drag
        { 
            get
            {
                if(!bPlayerDragSet)
                {
                    m_Player_Mass = GameInfo.PlayerCtrl.PlayerPawn.RB.drag;
                    bPlayerDragSet = true;
                }
                return m_Player_Drag;
            }
            set{ m_Player_Drag = value; }
        }
        public float Player_TurningRate
        { 
            get
            {
                if(!bPlayerTurningRateSet)
                {
                    m_Player_TurningRate = GameInfo.PlayerCtrl.PlayerPawn.TurningRate;
                    bPlayerTurningRateSet = true;
                }
                return m_Player_TurningRate;
            }
            set{ m_Player_ThrustAmt = value; }
        }
        public float Player_MaxSpeed
        { 
            get
            {
                if(!bPlayerMaxSpeedSet)
                {
                    m_Player_MaxSpeed = GameInfo.PlayerCtrl.PlayerPawn.MaxSpeed;
                    bPlayerMaxSpeedSet = true;
                }
                return m_Player_MaxSpeed;
            }
            set{ m_Player_MaxSpeed = value; }
        }
        public float Player_ThrustAmt
        { 
            get
            {
                if(!bPlayerThrustAmtSet)
                {
                    m_Player_ThrustAmt = GameInfo.PlayerCtrl.PlayerPawn.ThrustAmt;
                    bPlayerThrustAmtSet = true;
                }
                return m_Player_ThrustAmt;
            }
            set{ m_Player_MaxSpeed = value; }
        }

        public PlayerDebugGui()
        {
        }

        internal void UpdatePlayerDebugOverlay()
        {
            PlayerPawn Player = GameInfo.PlayerCtrl.PlayerPawn;
            
            if(Player == null || Player.RB == null)
                return;
            
            int numberOfRows = 6;

            GUI.BeginGroup(new Rect(25,50, DebugVarables.GROUP_WIDTH, numberOfRows * DebugVarables.LINE_HEIGHT));
            GUI.Label(new Rect(0,0, DebugVarables.GROUP_WIDTH, DebugVarables.LINE_HEIGHT),"Player Controls");

            //sliders
            m_Player_Mass = DebugGui.CreateSliderRow(1, "Mass", Player_Mass, 0.1f, 10);
            Player.RB.drag = DebugGui.CreateSliderRow(2, "Drag", Player.RB.drag, 0, 10);
            Player.TurningRate = DebugGui.CreateSliderRow(3, "TurningRate", Player.TurningRate, 0, 20);
            Player.MaxSpeed = DebugGui.CreateSliderRow(4, "MaxSpeed", Player.MaxSpeed, 0, 50);
            Player.ThrustAmt = DebugGui.CreateSliderRow(5, "ThrustAmt", Player.ThrustAmt, 0, 500);
            
            GUI.EndGroup();
            
            Player.RB.mass = m_Player_Mass;
        }

    }
}

