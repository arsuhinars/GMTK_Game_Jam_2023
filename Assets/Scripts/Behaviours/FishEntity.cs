using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        float wanderm_timer=5f;

        float m_nearestDistance=100f;

        FishState currentState=FishState.Wandering;
    
        GameObject m_nearestBoat;

        private Transform m_target;
        private NavMeshAgent m_agent;
        private float m_timer;

        //private Rigidbody rb;

        void OnEnable() 
        {
            m_agent = GetComponent<NavMeshAgent> ();
            m_timer = wanderm_timer;
            //rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            m_timer += Time.deltaTime;
 
            if (m_timer >= wanderm_timer) 
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
            // Vector3 velocity = rb.velocity;

            // if(velocity!=Vector3.zero)
            // {
            //     transform.forward=velocity;
            // }
        }

        public void Wandering()
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            m_agent.SetDestination(newPos);
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
            m_agent.SetDestination(m_nearestBoat.transform.position);
            m_timer=0;
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;
    
            randDirection += origin;
    
            NavMeshHit navHit;
    
            NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
    
            return navHit.position;
        }

        public void Kill()
        {
            Destroy(transform.parent.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag=="Boat")
            {
                Kill();
            }
        }
    }
}
