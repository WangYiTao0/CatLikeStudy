using System;
using System.Collections;
using UnityEngine;

namespace Procedural
{

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Grid : MonoBehaviour
    {
        public int XSize, YSize;
        private Vector3[] _vertices;
        private Mesh _mesh;

        private void Awake()
        {
            Generate();
        }

        private void Generate()
        {
            WaitForSeconds wait = new WaitForSeconds(0.05f);

            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _mesh.name = "Procedural Grid";
            //顶点树
            _vertices = new Vector3[(XSize + 1) * (YSize + 1)];
            Vector2[] uv = new Vector2[_vertices.Length];
            Vector4[] tangents = new Vector4[_vertices.Length];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
            
            for (int i = 0, y = 0; y <= YSize; y++)
            {
                for (int x = 0; x <= XSize; x++, i++)
                {
                    //0 0,0 
                    //1 0,1
                    //第二排 第一个 i+1  1，0 
                    _vertices[i] = new Vector3(x, y);
                    uv[i] = new Vector2((float)x / XSize, (float)y / YSize);
                    tangents[i] = tangent;
                }
            }
            _mesh.vertices = _vertices;
            _mesh.uv = uv;
            _mesh.tangents = tangents;
            
            int[] triangles = new int[6 * XSize * YSize];
            // triangles[0] = 0;
            // triangles[1] = triangles[4] = XSize + 1;
            // triangles[2] = triangles[3] =1;
            // triangles[5] = XSize + 2;

            for (int ti = 0, vi = 0, y = 0; y < YSize; y++, vi++)
            {
                for (int x = 0; x < XSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 1] = triangles[ti + 4] = vi + XSize + 1;
                    triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                    triangles[ti + 5] = vi + XSize + 2;
                }
            }
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
        }


        private void OnDrawGizmos()
        {
            if (_vertices == null)
            {
                return;
            }

            Gizmos.color = Color.black;
            for (int i = 0; i < _vertices.Length; i++)
            {
                Gizmos.DrawSphere(_vertices[i], 0.1f);
            }
        }
    }
}
