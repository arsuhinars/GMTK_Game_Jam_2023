using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(MeshFilter))]
    public class WaterMeshGenerator : MonoBehaviour
    {
        [SerializeField] private float size = 1f;
        [SerializeField] private int cellsCount = 10;

        private MeshFilter m_meshFilter;

        public void GenerateMesh()
        {
            int verticesCount = (cellsCount + 1) * (cellsCount + 1);
            
            var vertices = new Vector3[verticesCount];
            var triangles = new int[cellsCount * cellsCount * 2 * 3];

            int GetVertexIndex(int x, int y) => x + y * (cellsCount + 1);

            float posFactor = size / cellsCount;
            for (int x = 0; x < cellsCount + 1; ++x)
            {
                for (int y = 0; y < cellsCount + 1; ++y)
                {
                    int idx = GetVertexIndex(x, y);
                    vertices[idx] = new Vector3(x * posFactor, 0f, y * posFactor);
                }
            }
            
            for (int x = 0; x < cellsCount; ++x)
            {
                for (int y = 0; y < cellsCount; ++y)
                {
                    int idx = (x + y * cellsCount) * 6;
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
    }
}
