using System.Collections;
using UnityEngine;


namespace com.triplewater
{


    public class SceneManager : MonoBehaviour
    {

        public static SceneManager sceneManager;

        public GameObject playerPrefeb;
        public GameObject smallEnemyPrefab;
        public GameObject largeEnemyPrefab;
        public GameObject airWallPrefeb;
        public GameObject heartPrefeb;
        public GameObject wallPrefeb;
        public GameObject barriarPrefeb;
        public GameObject riverPrefeb;
        public GameObject grassPrefeb;
        public GameObject bornEffectPrefab;
        private int enemyCnt = 8;
        private int playerCnt = 3;
            
        public int  xStart;
        public int xEnd;
        public int yStart;
        public int yEnd;

        public int wallSize;
        private BitArray _positions;
        private System.Random _random;
        private int _width;
        private int _height;

        private float[][] _enemyPositions = new float[][]
            {new float[] {-10, 8}, new float[] {-5, 8}, new float[] {0, 8}, new float[] {5, 8}, new float[]{10, 8} };

        private void Awake()
        {
            if (sceneManager != null)
            {
                GameObject.Destroy(sceneManager); 
            }
            else
            {
                sceneManager = this;
            }
            DontDestroyOnLoad(this);
            _random = new System.Random();
            _width = xEnd - xStart + 1;
            _height = yEnd - yStart + 1;

        } 
        // Start is called before the first frame update
        void Start()
        {
            _positions = new BitArray(_width * _height);
                
//            GeneratePlayer(-2, -8);
//            GeneratePlayer(2, -8);
            GeneratePlayer();
            GenerateEnemy();
            BuildAirWall();
            BuildBase(0, -8);
            GenerateMap();
        }
        
        void BuildBase(float x, float y)
        {
            GenerateGameObject(heartPrefeb, x, y);
            for (int i = -1; i <= 1; i++)
            {
                if (i != 0)
                {
                    GenerateWall(i, -8);
                }

                GenerateWall(i, -7);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void GenerateMap()
        {
            for (int i = xStart; i <= xEnd; i++)
            {
                for (int j = yStart; j <= yEnd; j++)
                {
                    if (!_positions.Get(ToBitPosition(i, j)))
                    {
                        GenerateGameObject(GetRandomObject(), i, j);
                    }
                }
            }
        }

        void GenerateEnemy()
        {
            foreach (float[] pos in _enemyPositions)
            {
                if (enemyCnt > 0)
                {
                    Born(smallEnemyPrefab, pos[0], pos[1]);
                    enemyCnt--;
                }
            }
        }

        void GenerateWall(float x, float y)
        {

            GenerateGameObject(wallPrefeb, x, y);

        }

        void BuildAirWall()
        {
            //up
            for (int i = xStart; i <= xEnd; i += wallSize)
            {
                GenerateAirWall(i, yEnd + 1);
            }

            //down
            for (float i = xStart; i <= xEnd; i += wallSize)
            {
                GenerateAirWall(i, yStart - 1);
            }

            //left
            for (float i = yStart; i <= yEnd; i += wallSize)
            {
                GenerateAirWall(xStart - 1, i);
            }

            //right
            for (float i = yStart; i <= yEnd; i += wallSize)
            {
                GenerateAirWall(xEnd + 1, i);
            }
        }

        void GenerateAirWall(float x, float y)
        {

            GenerateGameObject(airWallPrefeb, x, y);

        }

        void GeneratePlayer()
        {
            if (playerCnt > 0)
            {
                Born(playerPrefeb, -2, -8);
                playerCnt--;
            }
        }

        GameObject GenerateGameObject(GameObject prefeb, float x, float y)
        {
            if (prefeb == null) return null;
            int pos = ToBitPosition((int)x, (int)y);
            if (x <= xEnd && x >= xStart && y <= yEnd && y >= yStart)
            {
                _positions.Set(pos, true);
            }

            return Instantiate(prefeb, new Vector3(x, y), Quaternion.identity);

        }

        private int ToBitPosition(int x, int y)
        {
            return x * _height  + y  +_width * (_height/2) + _width/2;
        }

        private void Born(GameObject bornObject, float x, float y) 
        {
            GameObject born = GenerateGameObject(bornEffectPrefab, x, y);
            born.GetComponent<Born>().StartCoroutine(
                DelayDestroyAndGenerate(born, bornObject, x, y,  0.3f));
        }
        
        private GameObject GetRandomObject()
        {
            int rnd = _random.Next(0, 10);

            switch (rnd)
            {
                case 0:
                case 1:
                    return grassPrefeb;
                case 2:
                    return barriarPrefeb;
                case 3:
                    return riverPrefeb;
                case 4:
                case 5:
                case 6:
                    return wallPrefeb;
                default:
                    return null;
                
            }
        }
        
        private IEnumerator DelayDestroyAndGenerate(Object dead, Object born, 
            float x, float y, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            Destroy(dead);
            Instantiate(born, new Vector3(x, y), Quaternion.identity);

        }


    }
}
