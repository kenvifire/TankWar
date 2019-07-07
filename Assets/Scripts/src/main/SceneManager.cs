using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject playerPrefeb;
    public GameObject airWallPrefeb;
    public GameObject heartPrefeb;
    public GameObject wallPrefeb;
    public float xStart;
    public float xEnd;
    public float yStart;
    public float yEnd;
    public float wallSize;
    // Start is called before the first frame update
    void Start()
    {
//        GeneratePlayer(0, 1);
        BuildAirWall();
        BuildBase(0, -8);
    }

    void BuildBase(float x, float y)
    {
        GenerateGameObject(heartPrefeb, x, y);
        for(int i = -1; i <= 1; i++)
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


    void GenerateEnimy()
    {

    }

    void GenerateWall(float x, float y)
    {

        GenerateGameObject(wallPrefeb, x, y);

    }

    void BuildAirWall()
    {
        //up
        for(float i = xStart; i < xEnd; i+= wallSize)
        {
            GenerateAirWall(i, 9); 
        }
        //down
        for (float i = xStart; i < xEnd; i += wallSize)
        {
            GenerateAirWall(i, -9);
        }

        //left
        for (float i = yStart; i < yEnd; i += wallSize)
        {
            GenerateAirWall(-11, i);
        }

        //right
        for (float i = yStart; i < yEnd; i += wallSize)
        {
            GenerateAirWall(11, i);
        }
    }

    void GenerateAirWall(float x, float y)
    {

        GenerateGameObject(airWallPrefeb, x, y);

    }

    void GeneratePlayer(float x, float y)
    {
        GenerateGameObject(playerPrefeb, x, y);

    }

    void GenerateGameObject(GameObject prefeb, float x, float y)
    {
        Instantiate(prefeb, new Vector3(x, y), Quaternion.identity);

    }


}
