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
    public int maxTilePosY;
    public int minTilePosY;

    private GameObject cam;
    private double camPosX;
    private double camPosY;
    

    private GameObject plane;

    // Use this for initialization
    void Start ()
    {
        cam = GameObject.Find("Main Camera");
        camPosX = cam.GetComponent<Mouse>().cameraStartPosX;
        camPosY = cam.GetComponent<Mouse>().cameraStartPosY;
        CreateMap();
    }
	
	// Update is called once per frame
	void Update ()
    {
        camPosX = cam.transform.position.x;
        camPosY = cam.transform.position.z;
        addRowOfTiles();
	}

    void addRowOfTiles()
    {
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
        //Left
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
        //Right
        if (camPosX + 4 > maxTilePosX)
        {
            for (int i = maxTileY; i >= minTileY; i--)
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
        }
        //Down
        if (camPosY - 4 < -maxTileY)
        {
            for (int i = minTilePosX + 1; i <= maxTilePosX; i++)
            {
                int j = i;
                if(j < 0)
                {
                    j = (j % totalTilesZoomX) + totalTilesZoomX;
                }
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = i + ", " + maxTileY;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = i;
                plane.GetComponent<CreateTile>().posY = -maxTileY;
                StartCoroutine(GetTextureMap(plane, j, maxTileY));
            }
            maxTileY++;
            totalTilesY++;
        }
        //Up
        if (camPosY + 4 > -minTileY)
        {
            for (int i = minTilePosX + 1; i <= maxTilePosX; i++)
            {
                int j = i;
                if (j < 0)
                {
                    j = (j % totalTilesZoomX) + totalTilesZoomX;
                }
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = i + ", " + maxTileY;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = i;
                plane.GetComponent<CreateTile>().posY = -maxTileY;
                StartCoroutine(GetTextureMap(plane, j, maxTileY));
            }
            maxTileY++;
            totalTilesY++;
        }
    }

    void CreateMap()
    {
        if (zoom == 0)
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
        for(int i = Convert.ToInt32(camPosX) - (startTilesX / 2); i <= Convert.ToInt32(camPosX) + (startTilesX / 2); i++)
        {
            for(int j = Convert.ToInt32(camPosY) - (startTilesY / 2); j <= Convert.ToInt32(camPosY) + (startTilesY / 2); j++)
            {
                plane = new GameObject();
                plane.transform.parent = GameObject.Find("TileHolder").transform;
                plane.name = i + ", " + j;
                plane.AddComponent<CreateTile>();
                plane.GetComponent<CreateTile>().posX = i;
                plane.GetComponent<CreateTile>().posY = j;
                StartCoroutine(GetTextureMap(plane, i, -j));
            }
        }

        minTilePosX = Convert.ToInt32(camPosX) - (startTilesX / 2);
        maxTilePosX = Convert.ToInt32(camPosX) + (startTilesX / 2);
        minTilePosY = Convert.ToInt32(camPosY) - (startTilesY / 2);
        maxTilePosY = Convert.ToInt32(camPosY) + (startTilesY / 2);

        maxTileX = (Math.Abs(Convert.ToInt32(camPosX))) + (startTilesX / 2) % totalTilesZoomX;
        minTileX = (Math.Abs(Convert.ToInt32(camPosX))) - (startTilesX / 2) % totalTilesZoomX;
        maxTileY = (Math.Abs(Convert.ToInt32(camPosY))) + (startTilesY / 2) % totalTilesZoomY;
        minTileY = (Math.Abs(Convert.ToInt32(camPosY))) - (startTilesY / 2) % totalTilesZoomY;

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
