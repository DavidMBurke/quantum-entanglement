using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTile : MonoBehaviour
{
    // These variables control what wires are created at the start of runtime
    public bool rightWire;
    public bool upWire;
    public bool leftWire;
    public bool downWire;
    public GameObject wirePrefab;

    private bool[] wireArray = new bool[4];
    private bool[] wireArrayOrient = new bool[4];


    // Start is called before the first frame update
    void Start()
    {
        float relWireShift = 0.5f - (wirePrefab.transform.localScale.x)/2;
        wireArray[0] = rightWire;
        wireArray[1] = upWire;
        wireArray[2] = leftWire;
        wireArray[3] = downWire;

        wireArrayOrient[0] = rightWire;
        wireArrayOrient[1] = upWire;
        wireArrayOrient[2] = leftWire;
        wireArrayOrient[3] = downWire;

        //Debug.Log("Starting orient: " + wireArrayOrient[0] + ", " + wireArrayOrient[1] + ", " + wireArrayOrient[2] + ", " + wireArrayOrient[3]);

        /*
        for (int i = 0; i < 4; i++) {
            if (wireArray[i]) {
                Instantiate(wirePrefab, transform.position + new Vector3(relWireShift*this.transform.localScale.x, 0, 0), transform.rotation, this.transform);
            }
        }
        */

        if (rightWire) {
            Instantiate(wirePrefab, transform.position + new Vector3(relWireShift*this.transform.localScale.x, 0, -1), transform.rotation, this.transform);
        }
        if (upWire) {
            Instantiate(wirePrefab, transform.position + new Vector3(0, relWireShift*this.transform.localScale.y, -1), transform.rotation * Quaternion.Euler(0, 0, 90), this.transform);
        }
        if (leftWire) {
            Instantiate(wirePrefab, transform.position + new Vector3(-relWireShift*this.transform.localScale.x, 0, -1), transform.rotation * Quaternion.Euler(0, 0, 180), this.transform);
        }
        if (downWire) {
            Instantiate(wirePrefab, transform.position + new Vector3(0, -relWireShift*this.transform.localScale.y, -1), transform.rotation * Quaternion.Euler(0, 0, 270), this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnMouseDown is called when a tile is clicked
    void OnMouseDown()
    {
        transform.Rotate(0, 0, 90);
        bool[] temp = {wireArrayOrient[0], wireArrayOrient[1], wireArrayOrient[2], wireArrayOrient[3]};
        for (int i = 1; i < 4; i++) {
            wireArrayOrient[i] = temp[i-1];
        }
        wireArrayOrient[0] = temp[3];
        
        //Debug.Log("New orient:" + wireArrayOrient[0] + ", " + wireArrayOrient[1] + ", " + wireArrayOrient[2] + ", " + wireArrayOrient[3]);

        if (wireArrayOrient[0] == wireArray[0] && wireArrayOrient[1] == wireArray[1] && wireArrayOrient[2] == wireArray[2] && wireArrayOrient[3] == wireArray[3]) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
