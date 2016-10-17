using UnityEngine;
using System.Collections;
using System;

public class CreatePlane : MonoBehaviour {

    protected readonly string osmUrl = "http://b.tile.openstreetmap.org/";
    protected readonly string osmApiKey = "xxx";
    public int zoom = 3;
    public int totalTilesZoomX;         //total number of X planes for this zoom
    public int totalTilesZoomY;         //total number of Y planes for this zoom
    public int totalTilesX = 0;         //Total X tiles
    public int totalTilesY = 0;         //Total Y tiles
    public int startTilesX = 10;        //total X planes per new world 
    public int startTilesY = 10;        //total Y planes per new world
    public int maxTilesX = 20;         //Maximum X planes at a tile 
    public int maxTilesY = 20;         //Maximum Y planes at a tile

    [Header("OSM coordinates of the map")]
    public int maxTileX = 10;
    public int maxTileY = 10;
    public int minTileX = 0;
    public int minTileY = 0;

    [Header("Unity coordinates X of the map")]
    public int maxTilePosX = 10;
    public int minTilePosX = 0;

    private GameObject cam;
    private double camPosX;
    private double camPosY;

    private GameObject plane;

    // Use this for initialization
    void Start ()
    {
        CreateMap();
        cam = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void Update ()
    {
        addRowOfTiles();
	}

    void addRowOfTiles()
    {
        camPosX = cam.transform.position.x;
        camPosY = cam.transform.position.z;

        if(maxTileX >= totalTilesZoomX)
        {
            maxTileX = 0;
        }
        else if(maxTileX < 0)
        {
            maxTileX = totalTilesZoomX - 1;
        }
        else if(minTileX >= totalTilesZoomX)
        {
            minTileX = 0;
        }
        else if(minTileX < 0)
        {
            minTileX = totalTilesZoomX - 1;
        }

        /*if (camPosX + 4 > maxTilePosX)
        {
            for(int i = 0; i < totalTilesY; i++)
            {
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = maxTilePosX + ", " + i;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = maxTilePosX;
                plane.GetComponent<CreateTile>().posY = -i;
                StartCoroutine(GetTextureMap(plane, maxTileX, i));
            }
            maxTileX++;
            totalTilesX++;
            maxTilePosX++;
        }*/

        if (camPosX - 4 < minTilePosX)
        {
            for (int i = minTileY; i <= maxTileY; i++)
            {
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = minTilePosX + ", " + i;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = minTilePosX;
                plane.GetComponent<CreateTile>().posY = -i;
                StartCoroutine(GetTextureMap(plane, minTileX, i));
            }
            minTileX--;
            totalTilesX++;
            minTilePosX--;
        }

        if (camPosY - 4 < -maxTileY)
        {
            for (int i = minTilePosX+1; i <= maxTilePosX; i++)
            {
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = i + ", " + maxTileY;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = i;
                plane.GetComponent<CreateTile>().posY = -maxTileY;
                StartCoroutine(GetTextureMap(plane, i, maxTileY));
            }
            maxTileY++;
            totalTilesY++;
        }
    }

    void CreateMap()
    {
        if(zoom == 0)
        {
            totalTilesZoomX = 1;
        }
        else
        {
            totalTilesZoomX = 1;
            for(int i = 0; i != zoom; i++)
            {
                totalTilesZoomX *= 2;
            }
            totalTilesZoomY = totalTilesZoomX;
        }
        for(int i = 0; i <= startTilesX; i++)
        {
            for(int j = 0; j <= startTilesY; j++)
            {
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = i + ", " + j;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = i;
                plane.GetComponent<CreateTile>().posY = -j;
                StartCoroutine(GetTextureMap(plane, i, j));
            }
        }
        totalTilesX = startTilesX;
        totalTilesY = startTilesY;
    }

    IEnumerator GetTextureMap(GameObject plane, int x, int y)
    {
        var url = osmUrl + zoom + "/" + x + "/" + y + ".png";
        WWW www = new WWW(url);
        yield return www;
        plane.GetComponent<Renderer>().material.mainTexture = www.texture;
    }

}
