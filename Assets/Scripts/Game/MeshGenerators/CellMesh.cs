using UnityEngine;

namespace Assets.Scripts.Game.MeshGenerators
{
    public class CellMesh
    {
        #region Fields

        private int _cellID;

        /// <summary>
        /// Field indicies what walls in cell are enabled. Order: top, right, bottom, left
        /// </summary>
        private bool[] _wallsEnabled = new bool[4]{true, true, true, true};

        private float _posX = 0.0f;
        private float _posZ = 0.0f;

        private Vector3[] _vertices;
        private int[] _triangles;

        private Mesh _mesh;


            
        #endregion

        #region Properties
        
        public int CellID { get => _cellID; set => _cellID = value; }

        /// <summary>
        /// Global cell position of ?left-down? corner
        /// </summary>
        public float PosX { get => _posX; set => _posX  = value; }
        public float PosZ { get => _posZ; set => _posZ  = value; }

        /// <summary>
        /// Vertices of mesh
        /// </summary>
        /// <value></value>
        public Mesh Mesh {get => _mesh; private set => _mesh = value;}


        #endregion

        #region Constructors
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CellMesh(float posX = 0.0f, float posZ = 0.0f)
        {
            _posX = posX;
            _posZ = posZ;
            _mesh = ConstructDefaultMesh();
        }


        #endregion

        #region Private methods 

        private Mesh ConstructDefaultMesh()
        {
            Mesh mesh = new Mesh();

            _vertices = new Vector3[24]
            {
                new Vector3(_posX + 0.0f, 0.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.0f, 0.0f, _posZ + 0.9f),
                new Vector3(_posX + 0.1f, 0.0f, _posZ + 0.9f),
                new Vector3(_posX + 0.1f, 0.0f, _posZ + 1.0f),
                new Vector3(_posX + 0.9f, 0.0f, _posZ + 1.0f),
                new Vector3(_posX + 0.9f, 0.0f, _posZ + 0.9f),
                new Vector3(_posX + 1.0f, 0.0f, _posZ + 0.9f),
                new Vector3(_posX + 1.0f, 0.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.9f, 0.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.9f, 0.0f, _posZ + 0.0f),
                new Vector3(_posX + 0.1f, 0.0f, _posZ + 0.0f),
                new Vector3(_posX + 0.1f, 0.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.0f, 1.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.0f, 1.0f, _posZ + 0.9f),
                new Vector3(_posX + 0.1f, 1.0f, _posZ + 0.9f),
                new Vector3(_posX + 0.1f, 1.0f, _posZ + 1.0f),
                new Vector3(_posX + 0.9f, 1.0f, _posZ + 1.0f),
                new Vector3(_posX + 0.9f, 1.0f, _posZ + 0.9f),
                new Vector3(_posX + 1.0f, 1.0f, _posZ + 0.9f),
                new Vector3(_posX + 1.0f, 1.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.9f, 1.0f, _posZ + 0.1f),
                new Vector3(_posX + 0.9f, 1.0f, _posZ + 0.0f),
                new Vector3(_posX + 0.1f, 1.0f, _posZ + 0.0f),
                new Vector3(_posX + 0.1f, 1.0f, _posZ + 0.1f)
            };

            _triangles = new int[26*3]
            {
                0, 1, 2,
                0, 2, 11,
                2, 3, 4,
                2, 4, 5,
                5, 6, 7,
                5, 7, 8,
                8, 9, 10,
                8, 10, 11,
                2, 5, 8,
                2, 8, 11,
                1, 13, 2,
                13, 14, 2,
                2, 14, 15,
                2, 15, 3,
                4, 16, 5,
                5, 16, 17,
                5, 17, 18, 
                5, 18, 6,
                7, 19, 8,
                8, 19, 20,
                8, 20, 21,
                8, 21, 9,
                10, 22, 23,
                10, 23, 11,
                11, 23, 12,
                11, 12, 0
            };

            var uvs = new Vector2[24]
            {
                new Vector2(128.0f/384, 136.0f/384),
                new Vector2(128.0f/384, 247.0f/384),
                new Vector2(136.0f/384, 247.0f/384),
                new Vector2(136.0f/384, 256.0f/384),
                new Vector2(248.0f/384, 256.0f/384),
                new Vector2(248.0f/384, 248.0f/384),
                new Vector2(256.0f/384, 248.0f/384),
                new Vector2(256.0f/384, 136.0f/384),
                new Vector2(248.0f/384, 136.0f/384),
                new Vector2(248.0f/384, 128.0f/384),
                new Vector2(136.0f/384, 128.0f/384),
                new Vector2(136.0f/384, 136.0f/384),
                new Vector2(8.0f/384, 16.0f/384),
                new Vector2(8.0f/384, 368.0f/384),
                new Vector2(16.0f/384, 368.0f/384),
                new Vector2(16.0f/384, 374.0f/384),
                new Vector2(367.0f/384, 374.0f/384),
                new Vector2(367.0f/384, 368.0f/384),
                new Vector2(375.0f/384, 368.0f/384),
                new Vector2(375.0f/384, 16.0f/384),
                new Vector2(367.0f/384, 16.0f/384),
                new Vector2(367.0f/384, 8.0f/384),
                new Vector2(16.0f/384, 8.0f/384),
                new Vector2(16.0f/384, 16.0f/384)
            };

            //for (int i = 0; i < _vertices.Length; i++) uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);


            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            mesh.uv = uvs;
            return mesh;
        }



        #endregion

        #region Public methods
            
        /// <summary>
        /// Sets enabled specified walls
        /// </summary>
        /// <param name="up"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="left"></param>
        public void SetWalls (
            bool up = false,
            bool right = false,
            bool bottom = false,
            bool left = false)
        {            
            _wallsEnabled = new bool[4]
            {
                up,
                right,
                bottom, 
                left
            };
            Debug.Log($"Walls at cell id {_cellID} has been set to: " + 
            $"up-{up.ToString()}, right-{right.ToString()}, " + 
            $"bottom-{bottom.ToString()}, left-{left.ToString()}");
        }    

        #endregion

    }
}