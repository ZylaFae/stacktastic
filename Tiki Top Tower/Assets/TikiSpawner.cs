using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikiSpawner : MonoBehaviour
{
    //Stackable Tiki block, array for randomization of different tiki styles
    public GameObject[] tiki;
    private GameObject currentTiki;
    private bool tikiActive;

    public float growthSpeed;
    public float topScale;
    public float highPoint;
    public float currentScale;
    public float errorMargin;

    public Camera camera;
    public Vector3 cameraOffset;
    private Vector3 highPointV3 = Vector3.zero;
    private int tikiStyle;
	// Use this for initialization
	void Start ()
    {
        SpawnTiki();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Creates a new tiki on button down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTiki();
        }

        // Makes the current tiki grow
        if (Input.GetKey(KeyCode.Space) && tikiActive)
        {
            currentScale += Time.deltaTime * growthSpeed;
            currentTiki.transform.localScale = Vector3.one * currentScale;
            // Destroys tiki if it gets too big
            if (currentScale > topScale)
            {
                DestroyTiki();
            }
        }

        // Finalizes tiki on button release
        if (Input.GetKeyUp(KeyCode.Space) && tikiActive)
        {
            if ((topScale - currentScale) < errorMargin)
            {
                currentScale = topScale;
                currentTiki.transform.localScale = Vector3.one * currentScale;
            }
            topScale = currentScale;
            highPoint += topScale;
            camera.transform.position = cameraOffset + highPointV3;
        }
    }

    // All code that executes when a new tiki is created goes here
    void SpawnTiki ()
    {
        highPointV3.y = highPoint;
        //Sets which tiki gets placed next (will be set randomly once more models exist)
        tikiStyle = 0;
        currentTiki = Instantiate(tiki[tikiStyle], highPointV3, Quaternion.identity);
        currentScale = 0;

        tikiActive = true;
    }

    // All code that executes on error runs here (flashes of red, lives lost, etc)
    void DestroyTiki ()
    {
        Destroy(currentTiki);
        tikiActive = false;
    }
}
