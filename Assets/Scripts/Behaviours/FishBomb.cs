using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK_2023.Scriptables;


namespace GMTK_2023.Behaviours
{
    public class FishBomb : MonoBehaviour
    {
        float m_bombTimer=0f;
        [SerializeField] FishBombSettings fishBombSettings;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_bombTimer+=Time.deltaTime;
            if(m_bombTimer>fishBombSettings.m_bombTimeCountdown)
            {
                GameObject bomb=Instantiate(fishBombSettings.bombPrefab,transform.position+new Vector3(0f,1.5f,2f), transform.rotation);
                //bomb.GetComponent<Rigidbody>().useGravity=false;
                bomb.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, fishBombSettings.m_launchVelocity,fishBombSettings.m_launchVelocity));
                m_bombTimer=0f;
            }
        }
    }
}