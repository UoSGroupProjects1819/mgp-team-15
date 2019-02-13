using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform footPosition;
    public string LastName = "";

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Slope")
        {
            if (LastName == collision.transform.parent.name) { return; }

            LastName = collision.transform.parent.name;

            if (collision.transform.position.y > footPosition.position.y)
            {
                //slide up
                GetComponent<ConstantForce2D>().relativeForce = new Vector2(GetComponent<ConstantForce2D>().relativeForce.x, GetComponent<ConstantForce2D>().relativeForce.x);
            }
            else
            {
                //slide down
                GetComponent<ConstantForce2D>().relativeForce = new Vector2(GetComponent<ConstantForce2D>().relativeForce.x, -GetComponent<ConstantForce2D>().relativeForce.x);
            }
        }
        else if (collision.transform.tag == "Flip")
        {
            if (LastName == collision.transform.parent.name) { return; }

            LastName = collision.transform.parent.name;
            Debug.Log("Flip down");
            float yDif = transform.position.y - collision.transform.position.y;

            transform.position = new Vector3(transform.position.x, collision.transform.position.y - yDif - 0.5f, transform.position.z);
            GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;
            transform.Rotate(Vector2.left * 180);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Slope")
        {
            if (LastName == collision.transform.parent.name)
            {
                GetComponent<ConstantForce2D>().relativeForce = new Vector2(GetComponent<ConstantForce2D>().relativeForce.x, 0);
                LastName = "";
            }
        }
       /* else if (collision.transform.tag == "Flip")
        {
            if (LastName == collision.transform.parent.name)
            {
                float yDif = transform.position.y - collision.transform.position.y;

                transform.position = new Vector3(transform.position.x, collision.transform.position.y - yDif + 0.5f, transform.position.z);
                GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;
                transform.Rotate(Vector2.left * 180);

                LastName = "";
            }
        }*/
    }
}
