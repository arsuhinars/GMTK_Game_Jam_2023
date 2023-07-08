using GMTK_2023.Managers;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(MeshFilter))]
    public class WaterMeshGenerator : PoolItem
    {
        public Vector2Int TilePos { get => m_tilePos; set => m_tilePos = value; }
        public float Size => m_size;

        [SerializeField] private float m_size = 1f;
        [SerializeField] private int m_cellsCount = 10;
        private Vector2Int m_tilePos;
        private MeshFilter m_meshFilter;

        public void GenerateMesh()
        {
            int verticesCount = (m_cellsCount + 1) * (m_cellsCount + 1);

            var vertices = new Vector3[verticesCount];
            var triangles = new int[m_cellsCount * m_cellsCount * 2 * 3];

            int GetVertexIndex(int x, int y) => x + y * (m_cellsCount + 1);

            float posFactor = m_size / m_cellsCount;
            for (int x = 0; x < m_cellsCount + 1; ++x)
            {
                for (int y = 0; y < m_cellsCount + 1; ++y)
                {
                    int idx = GetVertexIndex(x, y);
                    vertices[idx] = new Vector3(x * posFactor, 0f, y * posFactor);
                }
            }

            for (int x = 0; x < m_cellsCount; ++x)
            {
                for (int y = 0; y < m_cellsCount; ++y)
                {
                    int idx = (x + y * m_cellsCount) * 6;
                    // left top triangle
                    triangles[idx] = GetVertexIndex(x, y + 1);
                    triangles[idx + 1] = GetVertexIndex(x + 1, y);
                    triangles[idx + 2] = GetVertexIndex(x, y);
                    // right bottom one
                    triangles[idx + 3] = GetVertexIndex(x, y + 1);
                    triangles[idx + 4] = GetVertexIndex(x + 1, y + 1);
                    triangles[idx + 5] = GetVertexIndex(x + 1, y);
                }
            }

            var mesh = new Mesh()
            {
                vertices = vertices,
                triangles = triangles
            };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            m_meshFilter.mesh = mesh;
        }

        private void Awake()
        {
            m_meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            GenerateMesh();
        }

        private void Update()
        {
            if (!IsActiveInPool)
            {
                return;
            }

            var manager = WaterManager.Instance;

            if (!manager.TileBounds.Contains(m_tilePos))
            {
                manager.RemoveMesh(m_tilePos);
            }
        }

        public override void OnGet() { }

        public override void OnRelease() { }
    }
}
