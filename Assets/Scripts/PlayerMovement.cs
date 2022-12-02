using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public GameObject Avatar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetIsGameFailed()) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (GameManager.Instance.CurrentLevel == 2)
        {
            h = -Input.GetAxisRaw("Vertical");
            v = Input.GetAxisRaw("Horizontal");
            Vector3 playerPos = this.transform.position;
            playerPos.y = 10.2f;
            this.transform.position = playerPos;
        }
        // move related code
       
       
        if (h != 0 || v != 0)
        {
            float angle = 0;
            if (v == 1 && h == 0)
            {
                angle = 90;
            }
            else if (v == -1 && h == 0)
            {
                angle = -90;
            }
            else if (h == 1 && v == 0)
            {
                angle = 180;
            }
            else if (h == -1 && v == 0)
            {
                angle = 0;
            }
            else if(h == 1 && v == 1)
            {
                angle = 135;
            }
            else if(h == 1 && v == -1)
            {
                angle = -135;
            }
            else if( h == -1 && v == 1)
            {
                angle = 45;
            }
            else
            {
                angle = -45;
            }
            Avatar.transform.localEulerAngles = new Vector3(0, angle, 0);

            this.GetComponent<CharacterController>().Move(new Vector3((v * Speed) * Time.deltaTime, 0, (-h * Speed) * Time.deltaTime));
            //transform.Translate(new Vector3((v * Speed) * Time.deltaTime, 0, (-h * Speed) * Time.deltaTime), Space.Self);
            this.GetComponentInChildren<Animator>().SetBool("Move", true);
        }
        else
        {
            this.GetComponentInChildren<Animator>().SetBool("Move", false);
        }

        
    }
}
