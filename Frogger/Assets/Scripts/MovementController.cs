using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{

    public Animator animator;

    private float lerpTime;
    private float speed = 5.0f;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isMoveing;
    private int horizontal;
    private int vertical;


    private int instanceIdGround; // use for correct detecting logs

    void Start()
    {
        animator.SetBool("drown", false);
    }


    void Update()
    {
        Move();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "FloatElements")
        {
            instanceIdGround = col.transform.GetInstanceID();
            transform.SetParent(col.transform);
            // when is on Float Elements then dont snap to grid
            endPos = new Vector3(transform.position.x + horizontal, Mathf.Round(transform.position.y), Mathf.Round(transform.position.z + vertical));
            // endPos = new Vector3((endPos.x + col.transform.position.x) / 2, endPos.y, endPos.z);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Water" && instanceIdGround == 0 && !isMoveing)
        {
            Drown();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (instanceIdGround == col.transform.GetInstanceID())
        {
            transform.SetParent(null);
            instanceIdGround = 0;
        }
    }

    private void Move()
    {
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        if (!GameManager.instance.isDead)
        {
            // if some axis is pressed
            if (horizontal != 0 || vertical != 0)
            {
                if (!isMoveing)
                {
                    // next position from axis
                    endPos = new Vector3(Mathf.Round(transform.position.x + horizontal), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z + vertical));
                    //Debug.Log("From:" + startPos + ", to:" + endPos);
                    // next rotation from axis
                    int dir = horizontal * 90 + Mathf.Clamp(-vertical * 180, 0, 180);
                    transform.rotation = Quaternion.Euler(0, dir, 0);
                    // animation and controls
                    lerpTime = 0;
                    isMoveing = true;
                    startPos = transform.position;
                    animator.SetBool("jump", true);
                }
            }
            if (isMoveing == true)
            {
                // smooth move animation
                lerpTime += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, lerpTime);
                // if move is ending
                if (lerpTime >= 1)
                {
                    //meters counter
                    GameManager.instance.meters = (int)transform.position.z;
                    // fit to grid and end animation
                    transform.position = endPos;
                    startPos = endPos;
                     Debug.Log("meters: " +transform.position.z);
                    animator.SetBool("jump", false);
                    // if all axis is not pressed then can move again
                    if (horizontal == 0 && vertical == 0)
                        isMoveing = false;
                }
            }
        }

    }
    private void Drown()
    {
        if (!GameManager.instance.isDead)
        {
            Debug.Log("DEAD");
            GameManager.instance.isDead = true;
            animator.SetBool("drown", true);
            Instantiate(GameManager.instance.drownParticle, transform.position, Quaternion.identity);
            
        }
    }

}