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

    private GameObject[] wireArray = new GameObject[4];


    // Start is called before the first frame update
    void Start()
    {
        float relWireShift = 0.5f - (wirePrefab.transform.localScale.x)/2;

        if (rightWire) {
            wireArray[0] = Instantiate(wirePrefab, transform.position + new Vector3(relWireShift*this.transform.localScale.x, 0, 0), transform.rotation, this.transform);
        }
        if (upWire) {
            wireArray[1] = Instantiate(wirePrefab, transform.position + new Vector3(0, relWireShift*this.transform.localScale.y, 0), transform.rotation * Quaternion.Euler(0, 0, 90), this.transform);
        }
        if (leftWire) {
            wireArray[2] = Instantiate(wirePrefab, transform.position + new Vector3(-relWireShift*this.transform.localScale.x, 0, 0), transform.rotation * Quaternion.Euler(0, 0, 180), this.transform);
        }
        if (downWire) {
            wireArray[3] = Instantiate(wirePrefab, transform.position + new Vector3(0, -relWireShift*this.transform.localScale.y, 0), transform.rotation * Quaternion.Euler(0, 0, 270), this.transform);
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
    }
}
