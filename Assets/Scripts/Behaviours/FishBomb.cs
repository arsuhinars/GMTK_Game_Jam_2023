using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Behaviours
{
    public class FishBomb : MonoBehaviour
    {
        float m_bombTimeCountdown=5f;
        float m_bombTimer=0f;

        float m_launchVelocity=100f;

        [SerializeField] GameObject bombPrefab;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_bombTimer+=Time.deltaTime;
            if(m_bombTimer>m_bombTimeCountdown)
            {
                GameObject bomb=Instantiate(bombPrefab,transform.position+new Vector3(0f,1.5f,2f), transform.rotation);
                //bomb.GetComponent<Rigidbody>().useGravity=false;
                bomb.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, m_launchVelocity,m_launchVelocity));
                m_bombTimer=0f;
            }
        }
    }
}