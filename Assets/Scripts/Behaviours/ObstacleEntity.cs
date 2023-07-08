using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    public class ObstacleEntity : LevelItem
    {
        [SerializeField] private ObstacleSettings m_settings;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(m_settings.fishTag))
            {
                other.GetComponent<ISpawnable>().Kill();
            }
        }
    }
}
