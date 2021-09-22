using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Transform bodyTransform;
    Vector3 horizontalVelocity;
    Vector3 verticalVelocity;
    public Vector3 faceDirection=new Vector3(1,0,0);
    public float maxVelocity=1;
    public float fadeTime=1;
    public float jumpMaxVelocity = 2;
    public float jumpMaxTime = 2;
    public bool onGround = true;
    public int wallLayerMask = 1 << 9;
    public float skin = 0.035f;

    //+/- 1,2,3 for x,y,z
    List<Vector3> rayPoints = new List<Vector3>();
    Vector3 rayCenter;
    public int pointsDensity = 2;
    void Start()
    {
        bodyTransform = GetComponent<Transform>();
        horizontalVelocity = new Vector3(0, 0, 0);
        FillRayPoints();
    }

    void FillRayPoints()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        Bounds headBounds = GetComponentInChildren<PlayerHeadController>().GetComponent<Collider>().bounds;
        bounds.Encapsulate(headBounds);
        bounds.Expand(-2 * skin);
        //x
        for(int i=0;i<pointsDensity;i++)
            for(int j=0;j<pointsDensity;j++)
            {
                float y = Mathf.Lerp(bounds.max.y, bounds.min.y, ((float)i+1) / (pointsDensity+1));
                float z = Mathf.Lerp(bounds.max.z, bounds.min.z, ((float)j + 1) / (pointsDensity + 1));
                rayPoints.Add(new Vector3(bounds.max.x, y, z)-bodyTransform.position);
                rayPoints.Add(new Vector3(bounds.min.x, y, z) - bodyTransform.position);
            }
        //y
        for (int i = 0; i < pointsDensity; i++)
            for (int j = 0; j < pointsDensity; j++)
            {
                float x = Mathf.Lerp(bounds.max.x, bounds.min.x, ((float)i + 1) / (pointsDensity + 1));
                float z = Mathf.Lerp(bounds.max.z, bounds.min.z, ((float)j + 1) / (pointsDensity + 1));
                rayPoints.Add(new Vector3(x, bounds.max.y, z) - bodyTransform.position);
                rayPoints.Add(new Vector3(x, bounds.min.y, z) - bodyTransform.position);
            }
        //z
        for (int i = 0; i < pointsDensity; i++)
            for (int j = 0; j < pointsDensity; j++)
            {
                float y = Mathf.Lerp(bounds.max.y, bounds.min.y, ((float)i + 1) / (pointsDensity + 1));
                float x = Mathf.Lerp(bounds.max.x, bounds.min.x, ((float)j + 1) / (pointsDensity + 1));
                rayPoints.Add(new Vector3(x, y, bounds.max.z) - bodyTransform.position);
                rayPoints.Add(new Vector3(x, y, bounds.min.z) - bodyTransform.position);
            }
        rayCenter = bounds.center - bodyTransform.position;
        rayPoints.Add(new Vector3(bounds.max.x, bounds.max.y, bounds.max.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.max.x, bounds.min.y, bounds.max.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.min.x, bounds.max.y, bounds.max.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.min.x, bounds.min.y, bounds.max.z) - bodyTransform.position);
        rayPoints.Add(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z) - bodyTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            float horizontal = -Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float faceAngle = Mathf.Atan2(faceDirection.z, faceDirection.x) * 180 / Mathf.PI;
            Vector3 moveDir = new Vector3(vertical, 0, horizontal);
            moveDir = Quaternion.Euler(0, -faceAngle, 0) * moveDir;
            if (moveDir.magnitude > 0)
                moveDir = moveDir.normalized * maxVelocity;
            float delta = Time.deltaTime * maxVelocity / fadeTime;
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, moveDir, delta);
        }

        onGround = transform.position.y <= 0;
        if(onGround&&Input.GetAxis("Jump")>0)
        {
            onGround = false;
            verticalVelocity = Vector3.up * jumpMaxVelocity;
        }
        if(onGround)
        {
            verticalVelocity = Vector3.zero;
        }
        else
        {
            verticalVelocity += Vector3.down*jumpMaxVelocity * 2 / jumpMaxTime*Time.deltaTime;
        }
        Vector3 shift = (horizontalVelocity + verticalVelocity) * Time.deltaTime;
        shift=AdjustShift(shift);
        transform.Translate(shift);
    }

    Vector3 AdjustShift(Vector3 oldShift)
    {
        List<Vector3> points = new List<Vector3>();
        points.AddRange(rayPoints);
        points = points.Where(p => Vector3.Dot((p - rayCenter), oldShift) > 0).ToList();
        RaycastHit hit;

        foreach(var point in points)
        {
            if (Physics.Raycast(point + bodyTransform.position, transform.TransformDirection(oldShift), out hit, oldShift.magnitude+skin, wallLayerMask))
            {
                Debug.DrawRay(point + bodyTransform.position, transform.TransformDirection(oldShift.normalized)*hit.distance*10, Color.red);
                oldShift = oldShift.normalized * (hit.distance-skin);
                horizontalVelocity = Vector3.zero;
            }
            else Debug.DrawRay(point + bodyTransform.position, transform.TransformDirection(oldShift)*10, Color.blue);
        }
        return oldShift;
    }
}
