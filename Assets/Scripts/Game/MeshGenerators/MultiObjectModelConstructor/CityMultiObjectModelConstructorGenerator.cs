using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.MeshGenerators.MultiObjectModelConstructor
{
    internal class CityMultiObjectModelConstructorGenerator : MeshGeneratorBase
    {
        public GameObject meshAsset;

        public CityMultiObjectModelConstructorGenerator()        {

            meshAsset = Resources.Load<GameObject>("Prefabs/MeshAssets/CityMazeCells");
        }

        public override (List<Mesh>, List<Mesh>) GenerateMesh(MazeDescription description)
        {
            List<Mesh> wallsMesh = new List<Mesh>();
            List<Mesh> floorMesh = new List<Mesh>() { new Mesh() };

            GenerateFloorMesh(description, floorMesh[0]);

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
    }
}
