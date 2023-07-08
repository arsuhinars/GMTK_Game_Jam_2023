using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK_2023.Scriptables;
namespace GMTK_2023.Behaviours
{

    public class Bomb : MonoBehaviour
    {
        [SerializeField] BombSettings bombSettings;
        void OnTriggerEnter(Collider other)
        {
            //Destroy(transform.gameObject);
            if(other.gameObject.tag=="Boat")
            return;

            if(other.gameObject.tag=="Water")
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, bombSettings.radius);
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
