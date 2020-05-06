using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public Camera birdeye;
    public float cam_spd;
    public Vector2 map_size;
    public Vector2 boundary;
    public bool isTactic;
    

    private void Update()
    {
        if (isTactic)
        {
            if (Input.GetKey(KeyCode.W) && birdeye.WorldToViewportPoint(new Vector3(0, 0, map_size[1]))[1] - boundary[1] > 1.0)
            {
                birdeye.transform.position += new Vector3(0, 0, cam_spd) * Time.deltaTime;
                Debug.Log("W key was pressed.");
            }
            if (Input.GetKey(KeyCode.A) && birdeye.WorldToViewportPoint(new Vector3(0, 0, 0))[0] + boundary[0] < 0.0)
            {
                birdeye.transform.position += new Vector3(-cam_spd, 0, 0) * Time.deltaTime;
                Debug.Log("A key was pressed.");
            }
            if (Input.GetKey(KeyCode.S) && birdeye.WorldToViewportPoint(new Vector3(0, 0, 0))[1] + boundary[1] < 0.0)
            {
                birdeye.transform.position += new Vector3(0, 0, -cam_spd) * Time.deltaTime;
                Debug.Log("S key was pressed.");
            }
            if (Input.GetKey(KeyCode.D) && birdeye.WorldToViewportPoint(new Vector3(map_size[0], 0, 0))[0] - boundary[0] > 1.0)
            {
                birdeye.transform.position += new Vector3(cam_spd, 0, 0) * Time.deltaTime;
                Debug.Log("D key was pressed.");
            }
        }
    }
}
