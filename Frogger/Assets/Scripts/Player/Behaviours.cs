using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{

    public class Behaviours : MonoBehaviour
    {
        public static Behaviours instance;
        public Animator animator;
        [HideInInspector]
        public bool isBusy;
        public float speed;
        
        private float lerpTime;
        private bool isJumpOnVehicle = false;
        private IEnumerator coroutine;


        void Awake()
        {
            if (instance == null) instance = this;
        }
        

        void OnTriggerEnter(Collider col)
        {
            //Debug.Log("[OnTriggerEnter] Triggered by " +col.transform.name +", " +col.transform.tag);
            if (!isJumpOnVehicle)
            {
                if (col.transform.tag == "Car")
                {
                    UnderVehicle();
                }
                else
                if (col.transform.tag == "Train")
                {
                    JumpOnVehicle(col.transform, Vector3.zero);
                }
            }
        }




        // check what is on position where player move and do reaction
        public void CheckPosition(Direction dir)
        {
            
            RaycastHit hit;
            Vector3 checkDirection = DirectionToVector(dir);
            Vector3 checkOrigin = transform.position + checkDirection + Vector3.up * 2;
            isBusy = true;
            lerpTime = 0;
            // rotate player to target direction
            transform.eulerAngles = DirectionToEulerAngles(dir);

            // debug raycasting
            //Debug.DrawRay(transform.position, checkDirection, Color.yellow, 5.0f);
            //Debug.DrawRay(checkOrigin, Vector3.down * 4, Color.green, 5.0f);

            if (Physics.Raycast(checkOrigin, Vector3.down * 4, out hit))
            {
                // take transform from collider hit by raycast
                Transform target = hit.collider.transform;
                // React to conditions  
                switch (target.tag)
                {
                    case "Ground":
                        MoveTo(target);
                        break;
                    case "FloatElements":
                        JumpTo(target);
                        break;
                    case "Water":
                        JumpToWater(target);
                        break;
                    case "Blocked":
                        MoveBlocked(target);
                        break;
                    case "Car":
                        JumpOnVehicle(target,checkDirection);
                        break;
                    case "Train":
                        JumpOnVehicle(target, checkDirection);
                        break;
                    default:
                        Debug.LogWarning("Detected object without tags. Object name: " + target.name);
                        break;
                }
            }
        }

        private Vector3 DirectionToVector(Direction dir)
        {
            Vector3 checkDirection = Vector3.zero;
            switch (dir)
            {
                case Direction.Forward:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    checkDirection = Vector3.forward;
                    break;
                case Direction.Back:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    checkDirection = Vector3.back;
                    break;
                case Direction.Left:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    checkDirection = Vector3.left;
                    break;
                case Direction.Right:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    checkDirection = Vector3.right;
                    break;
            }

            return checkDirection;
        }
        private Vector3 DirectionToEulerAngles(Direction dir)
        {
            // Vector.zero = forward
            Vector3 eulerAngles = Vector3.zero;
            switch (dir)
            {
                case Direction.Back:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    eulerAngles = Vector3.up * 180;
                    break;
                case Direction.Left:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    eulerAngles = Vector3.up * 270;
                    break;
                case Direction.Right:
                    //Debug.Log("[Behaviour] check position: " + dir.ToString());
                    eulerAngles = Vector3.up * 90;
                    break;
            }

            return eulerAngles;
        }


        #region Behaviour Functions
        // move to in ground area
        private void MoveTo(Transform target)
        {
            // animation jump
            animator.SetBool("jump", true);
            // move to target position
            coroutine = MovingAnimation(target);
            StartCoroutine(coroutine);
            // in case when player move from floating elements
            if (transform.parent != null)
                transform.SetParent(null);            
        }
        // jump in water area between floating elements
        private void JumpTo(Transform target)
        {
            // set parent
            transform.SetParent(target);
            // animation jump
            animator.SetBool("jump", true);
            // move to target position
            coroutine = MovingAnimation(target);
            StartCoroutine(coroutine);
        }

        private void JumpToWater(Transform target)
        {
            //Debug.Log("[Behaviour] Player JumpToWater.");
            // animation jump
            animator.SetBool("jumpToWater", true);
            // move to target position
            coroutine = JumpToWaterAnimation(target);
            StartCoroutine(coroutine);
            // in case when player jump to water from floating elements
            if (transform.parent != null)
                transform.SetParent(null);
        }

        private void MoveBlocked(Transform target)
        {
            //Debug.Log("[Behaviour] Player MoveBlocked.");
            //animation try to jump on block
            animator.SetBool("blocked", true);
            // after this behaviour do idle
            Invoke("Idle", 0.5f);
        }

        public void UnderVehicle()
        {
            //Debug.Log("[Behaviour] Player UnderVehicle.");
            StopCoroutine(coroutine);
            transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
            transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
            // after this behaviour do end game
            Dead();
        }

        private void JumpOnVehicle(Transform target, Vector3 dir)
        {
            //Debug.Log("[Behaviour] Player JumpOnVehicle.");
            // required for player not dead under vehicle
            isJumpOnVehicle = true;
            // scale player
            transform.localScale = new Vector3(1.0f, 1.0f, 0.1f);
            // set parent
            transform.SetParent(target);
            // stick player in vehicle script
            target.SendMessage("StickPlayer", gameObject);
            // animation
            animator.SetBool("jumpOnVehicle", true);
            // move to target position
            //Debug.Log("Start Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);

            coroutine = JumpOnVehicleAnimation(target, dir);
            StartCoroutine(coroutine);         
        }

        public void Drown(Transform target)
        {
            //Debug.Log("[Behaviour] Player Drown.");
            // after this behaviour do end game
            Dead();
        }

        private void Idle()
        {
            animator.SetBool("jump", false);
            animator.SetBool("blocked", false);
            isBusy = false;
        }
        
        private void Dead()
        {
            GetComponent<Movement>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GameManager.instance.EndGame();
            this.enabled = false;
        }
        
        #endregion

        #region Behaviour Animation
        IEnumerator MovingAnimation(Transform target)
        {
            // wait
            yield return new WaitForFixedUpdate();
            //Debug.Log("- Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);            
            // smooth move animation to target.position (target.position can change in time!)
            lerpTime += Time.deltaTime * speed;
            // position where player move, Vector3.up mean player go above target
            Vector3 targetPosition = target.position + Vector3.up;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpTime);
            // continue animation when lerptime < 1.0f
            if (lerpTime < 1.0f)
            {
                coroutine = MovingAnimation(target);
                StartCoroutine(coroutine);
            }
            else
            {
                //Debug.Log("End Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);
                // end animation and enable idle behaviours after 0.1f ( Invoke is anty glitch)
                transform.position = targetPosition;
                Invoke("Idle", 0.1f);
            }
        }

        IEnumerator JumpToWaterAnimation(Transform target)
        {
            // wait
            yield return new WaitForFixedUpdate();
            //Debug.Log("- Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);            
            // smooth move animation to target.position (target.position can change in time!)
            lerpTime += Time.deltaTime * speed;
            // position where player move
            Vector3 targetPosition = target.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpTime);
            // continue animation when lerptime < 1.0f
            if (lerpTime < 1.0f)
            {
                coroutine = JumpToWaterAnimation(target);
                StartCoroutine(coroutine);
            }
            else
            {
                //Debug.Log("End Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);
                // end animation and enable idle behaviours after 0.1f ( Invoke is anty glitch)
                transform.position = targetPosition;
                // after this behaviour end game
                Dead();
            }
        }

        IEnumerator JumpOnVehicleAnimation(Transform target,Vector3 dir)
        {
            // wait
            yield return new WaitForFixedUpdate();
            //Debug.Log("- Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);            
            // smooth move animation to target.position (target.position can change in time!)
            lerpTime += Time.deltaTime * speed * 2;
            // position where player move, 
            Vector3 targetPosition = target.position- dir/2;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpTime);
            // continue animation when lerptime < 1.0f
            if (lerpTime < 1.0f)
            {
                coroutine = JumpOnVehicleAnimation(target, dir);
                StartCoroutine(coroutine);
            }
            else
            {
                //Debug.Log("End Animation " + lerpTime.ToString("0.00") + ", position: " + transform.position);
                // end animation and enable idle behaviours after 0.1f ( Invoke is anty glitch)
                transform.position = targetPosition;
                // after this behaviour end game
                Dead();
            }
        }
        #endregion

    }

}
