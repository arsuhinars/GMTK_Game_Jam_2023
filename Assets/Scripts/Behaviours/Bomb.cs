using UnityEngine;
using GMTK_2023.Scriptables;
using GMTK_2023.Managers;

namespace GMTK_2023.Behaviours
{
    public class Bomb : LevelItem
    {
        [SerializeField] private BombSettings m_settings;

        public void Throw(Vector3 origin, Vector3 direction)
        {
            Rigidbody.position = origin;
            Rigidbody.velocity = direction.normalized * m_settings.throwSpeed;
        }

        private void FixedUpdate()
        {
            if (Rigidbody.position.y <= LevelManager.Instance.WaterLevelY)
            {
                HandleExplosion();
            }
        }

        private void HandleExplosion()
        {
            var colliders = Physics.OverlapSphere(Rigidbody.position, m_settings.explosionRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag(m_settings.fishTag))
                {
                    colliders[i].GetComponent<ISpawnable>().Kill();
                }
            }

            Kill();
        }
    }
}
