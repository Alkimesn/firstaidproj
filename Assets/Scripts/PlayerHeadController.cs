using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadController : MonoBehaviour
{
    public float click_delay = 1;
    float current_delay = 0;

    Transform headTransform;
    PlayerController playerController;
    int layermask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        headTransform = GetComponent<Transform>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        headTransform.Rotate(0, horizontal, 0, Space.World);
        headTransform.Rotate(0, 0, vertical, Space.Self);
        playerController.faceDirection = Quaternion.Euler(0, horizontal, 0) * playerController.faceDirection;

        if(current_delay < Time.deltaTime) { current_delay = 0; }
        else { current_delay -= Time.deltaTime; }

        if(((Input.GetAxis("Fire1")!=0) || (Input.GetAxis("Fire2") != 0)) && current_delay == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layermask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                Debug.Log("Did Hit: "+ hit.transform.name);
                Swap_mat click = hit.transform.gameObject.GetComponent<Swap_mat>();
                if (Input.GetAxis("Fire1") != 0)
                {
                    click.DoAction(Swap_mat.Actions.LEFT);
                }
                else
                {
                    click.DoAction(Swap_mat.Actions.RIGHT);
                }
                current_delay = click_delay;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
}
