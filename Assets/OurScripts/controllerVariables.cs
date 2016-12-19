using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerVariables : MonoBehaviour {
    public List<string> questTiles;
    GameObject world;

    private void Start() {
        world = GameObject.Find("World");
    }
    private void Update() {
        if (!world) {
            questTiles.Clear();
            world = GameObject.Find("World");
        }
    }
}
