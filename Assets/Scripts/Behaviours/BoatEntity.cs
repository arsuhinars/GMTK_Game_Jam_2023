using GMTK_2023.Managers;
using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    public class BoatEntity : LevelItem
    {
        [SerializeField] private BoatSettings m_settings;
        [SerializeField] private GameObject m_fishBaitObject;
        [SerializeField] private GameObject m_bombObject;

        private FishBait m_fishBait;
        private Bomb m_bomb;
        private float m_actionTimer;

        public override void OnGet()
        {
            m_actionTimer = 0f;
        }

        public override void OnRelease() { }

        protected override void Awake()
        {
            base.Awake();

            m_fishBait = m_fishBaitObject.GetComponent<FishBait>();
            m_bomb = m_bombObject.GetComponent<Bomb>();

            m_fishBaitObject.SetActive(false);
            m_bombObject.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();

            if (!GameManager.Instance.IsStarted
                || !IsAlive
                || m_fishBait.IsAlive
                || m_bomb.IsAlive)
            {
                return;
            }

            m_actionTimer += Time.deltaTime;
            if (m_actionTimer < m_settings.cooldownTime)
            {
                return;
            }
            m_actionTimer = 0f;

            if (Random.Range(0f, 1f) < m_settings.bombSpawnChance)
            {
                SpawnBomb();
            }
            else
            {
                SpawnBait();
            }
        }

        private void SpawnBomb()
        {
            m_bomb.Spawn();
            m_bomb.Throw(
                transform.position + m_settings.throwOffset,
                GenerateThrowDirection()
            );
        }

        private void SpawnBait()
        {
            m_fishBait.Spawn();
            m_fishBait.Throw(
                transform.position + m_settings.throwOffset,
                GenerateThrowDirection()
            );
        }

        private Vector3 GenerateThrowDirection()
        {
            var angle = Random.Range(0f, 2f * Mathf.PI);
            float k = Mathf.Sin(m_settings.throwAngle * Mathf.Deg2Rad);

            return new Vector3(
                Mathf.Cos(angle) * k,
                Mathf.Sin(m_settings.throwAngle * Mathf.Deg2Rad),
                Mathf.Sin(angle) * k
            );
        }
    }
}
