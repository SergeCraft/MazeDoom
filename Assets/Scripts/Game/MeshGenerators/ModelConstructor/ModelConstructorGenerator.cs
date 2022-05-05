using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.MeshGenerators
{
    public class ModelConstructorGenerator : MeshGeneratorBase
    {
        public GameObject meshAsset;

        public ModelConstructorGenerator()
        {
            meshAsset = Resources.Load<GameObject>("Prefabs/MeshAsset");
        }

        public override Mesh GenerateMesh(MazeDescription descr)
        {
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<int> triangles = new List<int>();

            try
            {
                if (meshAsset == null) throw new ArgumentNullException("Mesh asset not assigned!");
                List<MeshFilter> cellSamples = new List<MeshFilter>(meshAsset.GetComponentsInChildren<MeshFilter>());
                if (cellSamples.Count == 0) throw new ArgumentNullException("Mesh asset not contains any samples!");
                

                foreach (var cellDescr in descr.CellDescriptions)
                {
                    var cellMeshFilter = cellSamples[(int)cellDescr.Type - 1];
                    var cellVertices = cellMeshFilter.sharedMesh.vertices.ToList();
                    var cellTriangles = cellMeshFilter.sharedMesh.triangles.ToList();


                    for (int i = 0; i < cellVertices.Count; i++)
                        cellVertices[i] += new Vector3(cellDescr.ColumnPosition*1.00f, 0, -cellDescr.RowPosition * 1.00f);
                    for (int i = 0; i < cellTriangles.Count; i++)
                        cellTriangles[i] += vertices.Count;


                    vertices.AddRange(cellVertices);
                    triangles.AddRange(cellTriangles);
                    uvs.AddRange(cellMeshFilter.sharedMesh.uv);
                }

                //Debug vertices indexes
                //GameObject[] ids = vertices.Select(vert =>
                //{
                //    GameObject index = new GameObject("Vertice");
                //    TextMesh tm = index.AddComponent<TextMesh>();
                //    //tm.text = vertices.IndexOf(vert).ToString();
                //    tm.text = vert.x.ToString();
                //    tm.fontSize = 20;
                //    index.transform.position = vert;
                //    index.transform.localScale = new Vector3(0.03f, 0.03f, 0.1f);
                //    return index;
                //})
                //.ToArray();

                //Debug cell info
                //foreach (var cell in descr.CellDescriptions)
                //{
                //    GameObject index = new GameObject("CellID");
                //    TextMesh tm = index.AddComponent<TextMesh>();
                //    //tm.text = vertices.IndexOf(vert).ToString();
                //    //tm.text = $"{descr.CellDescriptions.IndexOf(cell)}\n{cell.Type}";
                //    tm.text = $"C{cell.ColumnPosition} R{cell.RowPosition} T{cell.ThreadId}";
                //    tm.fontSize = 20;
                //    index.transform.position = new Vector3(cell.ColumnPosition * 1.00f, 0.8f, -cell.RowPosition * 1.00f);
                //    index.transform.position = new Vector3(cell.ColumnPosition * 1.00f, 0.8f, -cell.RowPosition * 1.00f);
                //    index.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                //    index.transform.rotation = Quaternion.Euler(90, 0, 0);
                //}
            }
            catch (Exception ex)
            {

                Debug.Log($"Mesh generation failure! {ex.Message} at {ex.StackTrace}");
                return null;
            };

            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();

            mesh.RecalculateNormals();
             
            Debug.Log("Mesh generation successful");
            return mesh;
        }

    }
}
