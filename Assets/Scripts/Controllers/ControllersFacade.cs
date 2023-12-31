﻿//using Assets.Scripts.Managers;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    public class ControllersFacade : MonoBehaviour
    {
        public static ControllersFacade Instance { get; private set; } = null;

        public CameraController CameraController => m_camera;
        public PlayerController PlayerController => m_player;
        //public WaterManager WaterManager => m_waterManager;
        public UIController UIController => m_ui;

        private CameraController m_camera;
        private PlayerController m_player;
        private UIController m_ui;
        //private WaterManager m_waterManager;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }

            m_camera = FindObjectOfType<CameraController>();
            m_player = FindObjectOfType<PlayerController>();
            m_ui = FindObjectOfType<UIController>();
            //m_waterManager = FindObjectOfType<WaterManager>();
        }
    }
}
