using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public int i;
    public float brushStrenght;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Test")
                {

                    hit.collider.GetComponent<MeshGenerator>().DrawMesh(hit.triangleIndex, brushStrenght);
                }
                if (hit.collider.tag == "StandartObject")
                {

                    hit.collider.GetComponent<MeshObject>().DrawMesh(hit.triangleIndex, brushStrenght);
                }

            }
        }
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Test")
                {

                    hit.collider.GetComponent<MeshGenerator>().DrawMesh(hit.triangleIndex, -brushStrenght);
                }

            }
        }
       



    }
}
