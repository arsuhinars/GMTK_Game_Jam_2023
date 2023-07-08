using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    enum FishState
    {
        Wandering,
        TowardsBait
    };

    public class FishEntity : MonoBehaviour
    {
        float wanderRadius=10f;
        float wanderTimer=5f;

        float m_nearestDistance=100f;

        float m_speed=4f;
        float m_rotationSpeed=4f;

        [SerializeField] GameObject[] pointOfInterests;

        FishState currentState=FishState.Wandering;
    
        GameObject m_nearestBoat;

        private Transform m_target;
        private float m_timer;

        private Rigidbody rb;

        void OnEnable() 
        {
            m_timer = wanderTimer;
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            m_timer += Time.deltaTime;
 
            if (m_timer >= wanderTimer) 
            {
                int chance=Random.Range(0,10);
                switch(chance)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    currentState=FishState.TowardsBait;
                    break;

                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    currentState=FishState.Wandering;
                    break;
                }

                switch(currentState)
                {
                    case FishState.Wandering:
                    Wandering();
                    break;

                    case FishState.TowardsBait:
                    TowardsBait();
                    break;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, m_target.position, m_speed*Time.deltaTime);
            var desiredRotation=Quaternion.LookRotation(m_target.position);
            transform.rotation=Quaternion.Slerp(transform.rotation,desiredRotation,Time.deltaTime*m_rotationSpeed);
            Vector3 velocity = rb.velocity;
        }

        public void Wandering()
        {
            int randomPoint=Random.Range(0,pointOfInterests.Length);
            m_target=pointOfInterests[randomPoint].transform;
            m_timer = 0;
        }

        public void TowardsBait()
        {
            GameObject[] boats=GameObject.FindGameObjectsWithTag("Boat");
            for(int i=0;i<boats.Length;i++)
            {
                float distance=Vector3.Distance(transform.position,boats[i].transform.position);
                if(distance<m_nearestDistance)
                {
                    m_nearestBoat=boats[i];
                    m_nearestDistance=distance;
                }
            }
            m_target=m_nearestBoat.transform;
            m_timer=0;
        }
        public void Kill()
        {
            Destroy(transform.parent.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag=="Boat"&&currentState==FishState.TowardsBait)
            {
                Kill();
            }
        }
    }
}
