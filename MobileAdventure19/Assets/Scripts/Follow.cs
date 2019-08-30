using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    private GameObject target;

    public GameObject speechBubble;
    public GameObject speechBubbleOffCam;

    Camera cam;

    public float smoothSpeed = 10f;
    public Vector3 offset;

    void Start()
    {
        target = GameObject.FindWithTag("Player");

        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }

    private void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(speechBubble.transform.position);

        if (0.1f >= viewPos.y)
        {
            speechBubble.SetActive(false);

            speechBubbleOffCam.SetActive(true);
        }

        if (0.1f <= viewPos.y)
        {
            speechBubbleOffCam.SetActive(false);
            speechBubble.SetActive(true);
        }
    }
}
