using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxscript : MonoBehaviour
{
    [SerializeField] float parallaxEffect = 0.5f;

    Transform cam;
    Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;

        transform.position = new Vector3(
            cam.position.x,
            transform.position.y,
            transform.position.z
        );
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPos;
        transform.position += delta * parallaxEffect;
        lastCamPos = cam.position;
    }
}
