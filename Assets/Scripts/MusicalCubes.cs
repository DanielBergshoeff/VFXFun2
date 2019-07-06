using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalCubes : MonoBehaviour
{
    public float MaxScale;
    public GameObject SampleCube;
    GameObject[] sampleCubes = new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sampleCubes.Length; i++) {
            GameObject instanceSampleCube = Instantiate(SampleCube);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "Samplecube: " + i;
            instanceSampleCube.transform.position = Vector3.right * (100f * i - (100f * i/2));
            sampleCubes[i] = instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SampleCube == null || sampleCubes == null)
            return;

        for (int i = 0; i < sampleCubes.Length; i++) {
            sampleCubes[i].transform.localScale = new Vector3(10f, AudioManager.BandBuffers[i] * MaxScale + 2, 10f);
        }
    }
}
