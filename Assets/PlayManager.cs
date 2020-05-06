using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {
    public GameObject symbol;
    public Camera unit_cam;
    public Camera main_cam;
    public float spd;
    private bool isTactic;
    private bool isMoving = false;
    private bool isAiming = false;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool rotateAroundPlayer = true;
    public float rotateSpd = 5F;
    private Vector3 camOffset;


    void SelectUnit()
    {
        if (Input.GetMouseButtonDown(0) && isTactic)
        {
            Debug.Log("hit");
            RaycastHit hit;
            Ray ray = main_cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == symbol.GetComponent<Collider>())
                {
                    Debug.Log("Unit selected");
                    unit_cam.enabled = true;
                    unit_cam.GetComponent<AudioListener>().enabled = true;
                    main_cam.enabled = false;
                    main_cam.GetComponent<AudioListener>().enabled = false;
                    GameObject.Find("MapManager").GetComponent<MapManager>().isTactic = false;
                }
            }
        }
    }

    void CamOrbit()
    {
        if (rotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpd, Vector3.up);
            camOffset = camTurnAngle * camOffset;
            camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpd, new Vector3(0, 0, 1));
            camOffset = camTurnAngle * camOffset;
            unit_cam.transform.LookAt(transform);
        }
        Vector3 newCamPos = transform.position + camOffset;
        if (newCamPos[1] < 0.1F)
        {
            newCamPos[1] = 0.1F;
        }
        unit_cam.transform.position = Vector3.Slerp(unit_cam.transform.position, newCamPos, SmoothFactor);
    }

    void MoveUnit()
    {
        if (isMoving && !isAiming)
        {
            CamOrbit();
            var forward = unit_cam.transform.forward;
            var right = unit_cam.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.position += forward * spd * Time.deltaTime;
                Debug.Log("W key was pressed.");
            }
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.position += right * (-spd) * Time.deltaTime;
                Debug.Log("A key was pressed.");
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.position += forward * (-spd) * Time.deltaTime;
                Debug.Log("S key was pressed.");
            }
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position += right * spd * Time.deltaTime;
                Debug.Log("D key was pressed.");
            }
        }
    }

    void Aiming()
    {


    }

    void Attack()
    {

    }
    
    // Use this for initialization
	void Start () {
        main_cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camOffset = unit_cam.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        isTactic = GameObject.Find("MapManager").GetComponent<MapManager>().isTactic;
        isMoving = !isTactic;
        if (Input.GetMouseButtonDown(1) && isAiming == false)
        {
            isAiming = true;
            Aiming();
        }
        if (Input.GetMouseButtonDown(1) && isAiming == true)
        {
            isAiming = false;
            Aiming();
        }
        SelectUnit();
        MoveUnit();
    }
    
}
