using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Behaviours
{
    public class PowerupForceIncrease : MonoBehaviour
    {
        GameObject forceField;
        bool powerUpEnable=false;

        bool isEnabled=false;

        float forceMultiplier=2.0f;

        float timer=0.0f;
        float timeActive=5f;
        // Start is called before the first frame update
        void Start()
        {
            forceField=GameObject.Find("ForceField");
        }

        // Update is called once per frame
        void Update()
        {
            if(powerUpEnable)
            {
                timer+=Time.deltaTime;
                if(!isEnabled)
                {
                    forceField.GetComponent<ForceField>().setForceMultiplier(2.0f);
                    isEnabled=true;
                }
                if(timer>timeActive)
                {
                    forceField.GetComponent<ForceField>().setForceMultiplier(1.0f);
                    isEnabled=false;
                    powerUpEnable=false;
                    Destroy(transform.gameObject);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            powerUpEnable=true;
        }
    }
}

