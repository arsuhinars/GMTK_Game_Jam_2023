using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Behaviours
{
    public class FishBait : MonoBehaviour
    {
        float m_baitTimeCountdown=5f;
        bool m_baitEnabled=false;
        float m_baitTimer=0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_baitTimer+=Time.deltaTime;

            if(m_baitTimer>m_baitTimeCountdown)
            {
                m_baitEnabled=!m_baitEnabled;
                m_baitTimer=0;
            }
        }

        public bool isBaitEnabled()
        {
            return m_baitEnabled;
        }
    }
}

