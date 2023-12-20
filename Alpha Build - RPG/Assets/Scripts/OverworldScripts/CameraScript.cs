using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 bounds;
    [SerializeField] GameObject leftBound;
    [SerializeField] GameObject rightBound;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = 60f - (Options.fieldOfVision * 15f);

        Vector3 targetDestination = player.transform.position;
        targetDestination.z = transform.position.z;
        Vector3 toMove = ((targetDestination - transform.position) * 2 * Time.deltaTime);

        if(cam.WorldToViewportPoint(leftBound.transform.position).x > -0.1)
        {
            if(toMove.x < 0)
            {
                toMove.x = 0;
            }
        }
        if(cam.WorldToViewportPoint(rightBound.transform.position).x < 1.05)
        {
            if(toMove.x > 0)
            {
                toMove.x = 0;
            }
        }

        if(cam.WorldToViewportPoint(leftBound.transform.position).y > -0.1)
        {
            if(toMove.y < 0)
            {
                toMove.y = 0;
            }
        }
        if(cam.WorldToViewportPoint(rightBound.transform.position).y < 1.05)
        {
            if(toMove.y > 0)
            {
                toMove.y = 0;
            }
        }
        

        this.transform.Translate(toMove);
    }
}
