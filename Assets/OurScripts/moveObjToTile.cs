using UnityEngine;
using System.Collections;
using MapzenGo.Helpers;

public class moveObjToTile : MonoBehaviour {

    private float xCoord;//questGiver
    private float zCoord;
    int Zoom;
    float xCoord2, zCoord2; //start
    private GameObject world;
    //private string questGiver;

    // Use this for initialization
    void Start() {
        world = GameObject.FindWithTag("OpenWorld");
    }

    void Update() {
        if (!world) {
            MoveToTile(this.gameObject, this.GetComponent<quest>().xCoord, this.GetComponent<quest>().zCoord);
        }
    }

    // Use this for initialization
    /*void Start()
    {
        int Zoom = 16;
        xCoord = this.GetComponent<quest>().xCoord;
        zCoord = this.GetComponent<quest>().zCoord;
        var xCoord2 = 64.12429;
        var zCoord2 = -21.92686;

        Vector2d vec = new Vector2d(xCoord, zCoord);
        Vector2d vec2 = new Vector2d(xCoord2, zCoord2);

        Vector2d meters = GM.LatLonToMeters(vec);
        //Vector2d pixels = GM.MetersToPixels(meters, 16);
        Vector2d tiles = GM.MetersToTile(meters, 16);

        Vector2d meters2 = GM.LatLonToMeters(vec2);
        //Vector2d pixels2 = GM.MetersToPixels(meters2, 16);
        Vector2d tiles2 = GM.MetersToTile(meters2, 16);


        Vector2d CenterInMercator = GM.TileBounds(tiles2, Zoom).Center;
        var rect = GM.TileBounds(tiles, Zoom);
        this.gameObject.transform.parent = GameObject.Find("World").transform;
        this.gameObject.transform.position = (rect.Center - CenterInMercator).ToVector3();
        this.gameObject.transform.position += new Vector3(0, 15, 0);
        this.transform.localScale = new Vector3(100, 100, 0);
    }*/
    public void MoveToTile(GameObject obj, float lat, float lon) {
        world = GameObject.FindWithTag("OpenWorld");
        //  print("inside of MoveTOTile we start at: " + obj.transform.position);

        Zoom = world.GetComponent<MapzenGo.Models.TileManager>().Zoom;
        xCoord2 = world.GetComponent<MapzenGo.Models.TileManager>().Latitude;
        zCoord2 = world.GetComponent<MapzenGo.Models.TileManager>().Longitude;

        zCoord = lon;
        xCoord = lat;
        //        print("x is: " + xCoord + " x2 is: " + xCoord2);
        //        print("z is: " + zCoord + " z2 is: " + zCoord2);

        Vector2d vec = new Vector2d(xCoord, zCoord);
        Vector2d vec2 = new Vector2d(xCoord2, zCoord2);

        Vector2d meters = GM.LatLonToMeters(vec);
        Vector2d tiles = GM.MetersToTile(meters, Zoom);

        Vector2d meters2 = GM.LatLonToMeters(vec2);
        Vector2d tiles2 = GM.MetersToTile(meters2, Zoom);

        Vector2d CenterInMercator = GM.TileBounds(tiles2, Zoom).Center;
        var rect = GM.TileBounds(tiles, Zoom);
        //  print("after all the vector initiations. " + obj.transform.position);

        //obj.gameObject.transform.parent = world.transform;
        obj.gameObject.transform.localPosition = (rect.Center - CenterInMercator).ToVector3();
        //   print("after rect.center-centerinmercator " + obj.transform.position);

        //       print("RECT :" + rect.Center);
        //       print("MERC :" + CenterInMercator);
        //       print("rect: " + (rect.Center - CenterInMercator));

        obj.gameObject.transform.position += new Vector3(0, 15f, 0);
        obj.transform.localScale = new Vector3(100, 100, 0);


        GameObject.FindWithTag("GameController").GetComponent<controllerVariables>().questTiles.Add("tile " + ((tiles).ToString().Replace(" ", "")));
        //Öskjuhlíð, Reykjavík, Iceland
        //Hamraborg, Kópavogur, Iceland
        //Háskólinn
        //        print(obj.gameObject.transform.position);
        /*
        Vector2d centerX = new Vector2d(obj.transform.position.x, obj.transform.position.y);
        Vector2d CPixel = GM.TileToPixles(centerX);
        Vector2d CMeter = GM.PixelsToMeters(CPixel, Zoom);
        print("Cmeter: " + CMeter);
        print("Meter: " + meters);*/
    }
}
