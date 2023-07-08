using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    public class Bomb : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {
            //Destroy(transform.gameObject);
            if(other.gameObject.tag=="Boat")
            return;

            if(other.gameObject.tag=="Water")
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8f);
                foreach (var hitCollider in hitColliders)
                {
                    if(hitCollider.gameObject.tag=="Fish")
                    {
                        hitCollider.GetComponent<FishEntity>().Kill();
                    }
                }
            }
        }
    }
}
