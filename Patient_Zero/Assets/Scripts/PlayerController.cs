using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator CharacterAnim;
    public UnityEngine.AI.NavMeshAgent agent;
    float dist;
    public Camera cam;
    RaycastHit hit;
    Quaternion newRotation;
    float rotSpeed = 5f;

   
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //When the mouse is clicked to the camera will move with the character
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                CharacterAnim.SetBool("isRunning", true);

                Vector3 relativePos = hit.point - transform.position;
                newRotation = Quaternion.LookRotation(relativePos, Vector3.up);
                newRotation.x = 0.0f;
                newRotation.z = 0.0f;
            }

        }
        //When the character arrives at the selected point it will enter into the idle state
        dist = Vector3.Distance(hit.point, transform.position);
        if (dist < 1f)
         {
                CharacterAnim.SetBool("isRunning", false);
         }

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
    }
}