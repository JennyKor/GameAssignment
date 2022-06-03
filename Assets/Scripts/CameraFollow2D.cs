using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float timeOffset;
    [SerializeField] Vector2 posOffset;

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    public float bottomLimit;
    [SerializeField] float topLimit;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position; // where camera currently is

        Vector3 endPos = player.transform.position; //where we wish camera to go
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        //lerping
        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        //smoothdamping
        //transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset);

        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        transform.position = new Vector3
        (
            // Containing camera's x- and y-axis positions within given range.
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }

    //public void ResetBottomLimit(float newLimit)
    //{
    //    bottomLimit = newLimit;
    //}

    private void OnDrawGizmos()
    {
        // Draw a box around our camera boundary
        Gizmos.color = Color.red;
        //top line
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        //right line
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        //bottom line
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        //left line
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}
