using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRaycaster : MonoBehaviour {
    GameObject cam;
    Camera camera;

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camera = cam.GetComponent<Camera>();
    }

    void Update() {

    }
    public void findTileLocation() {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            print("I'm looking at " + hit.transform.parent.name);

        }
        else
            print("I'm looking at nothing!");

    }
}
