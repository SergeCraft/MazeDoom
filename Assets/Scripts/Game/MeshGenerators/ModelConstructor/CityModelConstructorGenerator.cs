using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.MeshGenerators.ModelConstructor
{
    public class CityModelConstructorGenerator : MeshGeneratorBase
    {
        public GameObject meshAsset;

        public CityModelConstructorGenerator()
        {
            meshAsset = Resources.Load<GameObject>("Prefabs/MeshAssets/CityMazeCells");
        }

        public override (Mesh, Mesh) GenerateMesh(MazeDescription descr)
        {
            Mesh wallsMesh = new Mesh();
            Mesh floorMesh = new Mesh();
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
                        cellVertices[i] += new Vector3(cellDescr.ColumnPosition * 1.00f, 0, -cellDescr.RowPosition * 1.00f);
                    for (int i = 0; i < cellTriangles.Count; i++)
                        cellTriangles[i] += vertices.Count;


                    vertices.AddRange(cellVertices);
                    triangles.AddRange(cellTriangles);
                    uvs.AddRange(cellMeshFilter.sharedMesh.uv);
                }

                GenerateFloorMesh(descr, floorMesh);

                //Debug vertices indexes
                //EnableVerticesIds(vertices);

                // Debug cell info
                //EnableCellInfo(descr);
            }
            catch (Exception ex)
            {

                Debug.Log($"Mesh generation failure! {ex.Message} at {ex.StackTrace}");
                return (null, null);
            };

            wallsMesh.vertices = vertices.ToArray();
            wallsMesh.uv = uvs.ToArray();
            wallsMesh.triangles = triangles.ToArray();

            wallsMesh.RecalculateNormals();



            Debug.Log("Mesh generation successful");
            return (wallsMesh, floorMesh);
        }

        private static void GenerateFloorMesh(MazeDescription descr, Mesh floorMesh)
        {
            floorMesh.vertices = new Vector3[8]
                            {
                    new Vector3(-0.5f, 0, 0.5f),
                    new Vector3(descr.ColumnCount - 0.5f, 0, 0.5f),
                    new Vector3(descr.ColumnCount - 0.5f, 0, -descr.RowCount + 0.5f),
                    new Vector3(-0.5f, 0, -descr.RowCount + 0.5f),
                    new Vector3(-0.5f, -0.1f, 0.5f),
                    new Vector3(descr.ColumnCount - 0.5f, -0.1f, 0.5f),
                    new Vector3(descr.ColumnCount - 0.5f, -0.1f, -descr.RowCount + 0.5f),
                    new Vector3(-0.5f, -0.1f, -descr.RowCount + 0.5f)
                            };
            floorMesh.triangles = new int[36]
            {
                    0, 1, 2,
                    2, 3, 0,
                    0, 4, 5,
                    0, 5, 1,
                    1, 5, 6,
                    1, 6, 2,
                    2, 6, 7,
                    2, 7, 3,
                    3, 7, 4,
                    3, 4, 0,
                    4, 5, 6,
                    4, 6, 7
            };
            floorMesh.uv = new Vector2[8]
            {
                    new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector2(0, 0)
            };

            floorMesh.RecalculateNormals();
        }

        private static void EnableCellInfo(MazeDescription descr)
        {
            foreach (var cell in descr.CellDescriptions)
            {
                GameObject index = new GameObject("CellID");
                TextMesh tm = index.AddComponent<TextMesh>();
                //tm.text = vertices.IndexOf(vert).ToString();
                tm.text = $"{descr.CellDescriptions.IndexOf(cell)}\n{cell.Type}";
                //tm.text = $"C{cell.ColumnPosition} R{cell.RowPosition} T{cell.ThreadId}";
                tm.fontSize = 20;
                index.transform.position = new Vector3(cell.ColumnPosition * 1.00f, 0.8f, -cell.RowPosition * 1.00f);
                index.transform.position = new Vector3(cell.ColumnPosition * 1.00f, 0.8f, -cell.RowPosition * 1.00f);
                index.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                index.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

        private static void EnableVerticesIds(List<Vector3> vertices)
        {
            GameObject[] ids = vertices.Select(vert =>
            {
                GameObject index = new GameObject("Vertice");
                TextMesh tm = index.AddComponent<TextMesh>();
                //tm.text = vertices.IndexOf(vert).ToString();
                tm.text = vert.x.ToString();
                tm.fontSize = 20;
                index.transform.position = vert;
                index.transform.localScale = new Vector3(0.03f, 0.03f, 0.1f);
                return index;
            })
            .ToArray();
        }
    }
}
