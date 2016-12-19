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

    public void MoveToTile(GameObject obj, float lat, float lon) {
        world = GameObject.FindWithTag("OpenWorld");

        Zoom = world.GetComponent<MapzenGo.Models.TileManager>().Zoom;
        xCoord2 = world.GetComponent<MapzenGo.Models.TileManager>().Latitude;
        zCoord2 = world.GetComponent<MapzenGo.Models.TileManager>().Longitude;

        zCoord = lon;
        xCoord = lat;

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
        obj.gameObject.transform.position += new Vector3(0, 15f, 0);
        obj.transform.localScale = new Vector3(300, 300, 0);


        if (obj.GetComponent<qWindowDB>().answeredThisQuestionCorrectAlready == false) {
            GameObject.FindWithTag("GameController").GetComponent<controllerVariables>().questTiles.Add("tile " + ((tiles).ToString().Replace(" ", "")));
        }
    }
}
