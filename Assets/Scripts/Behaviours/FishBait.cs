using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK_2023.Scriptables;

namespace GMTK_2023.Behaviours
{
    public class FishBait : MonoBehaviour
    {
        [SerializeField] FishBaitSettings fishBaitSettings;
        float m_baitTimer=0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_baitTimer+=Time.deltaTime;

            if(m_baitTimer>fishBaitSettings.m_baitTimeCountdown)
            {
                fishBaitSettings.m_baitEnabled=!fishBaitSettings.m_baitEnabled;
                m_baitTimer=0;
            }
        }

        public bool isBaitEnabled()
        {
            return fishBaitSettings.m_baitEnabled;
        }
    }
}

