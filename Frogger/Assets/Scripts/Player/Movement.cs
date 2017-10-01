using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public enum Direction
    {
        Forward,
        Back,
        Left,
        Right
        
    }

    public class Movement : MonoBehaviour
    {
        
        private Vector2 startPos;


        void Update()
        {
            // keyboard controll
#if UNITY_EDITOR
            Move();
#endif
            // swipes controll
#if UNITY_ANDROID
            Swipe();
#endif
        }

        private void Move(int horizontal = 0, int vertical = 0)
        {
            // if player is busy then cant move
            if (Behaviours.instance.isBusy || GameManager.instance.isDead)
                return;

            if (horizontal == 0)
                horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            if (vertical == 0)
                vertical = (int)(Input.GetAxisRaw("Vertical"));

            // if some axis is pressed
            if (horizontal != 0 || vertical != 0)
            {
                if (horizontal > 0) {
                    //Debug.Log("[PlayerMovement] Move Right.");
                    Behaviours.instance.CheckPosition(Direction.Right);
                }
                else
                if (horizontal < 0)
                {
                    //Debug.Log("[PlayerMovement] Move Left.");
                    Behaviours.instance.CheckPosition(Direction.Left);
                }
                else
                if (vertical > 0)
                {
                    //Debug.Log("[PlayerMovement] Move Forward.");
                    Behaviours.instance.CheckPosition(Direction.Forward);
                }
                else
                if (vertical < 0) {
                    //Debug.Log("[PlayerMovement] Move Back.");
                    Behaviours.instance.CheckPosition(Direction.Back);
                }
            }
        }



        private void Swipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;
                    case TouchPhase.Ended:
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        if (swipeDistVertical > 100.0f)
                        {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                            if (swipeValue > 0)
                            {  //up swipe
                                Debug.Log("[SWIPE] Move Up. ");
                                Move(0, 1);
                            }
                            else if (swipeValue < 0)
                            {//down swipe
                                Debug.Log("[SWIPE] Move Down. ");
                                Move(0, -1);
                            }
                        }
                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                        if (swipeDistHorizontal > 100.0f)
                        {
                            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                            if (swipeValue > 0)
                            {//right swipe
                                Debug.Log("[SWIPE] Move Right. ");
                                Move(1, 0);
                            }
                            else if (swipeValue < 0)
                            {//left swipe
                                Debug.Log("[SWIPE] Move Left. ");
                                Move(-1, 0);
                            }
                        }
                        Debug.Log("[SWIPE] swipeDistVertical: " + swipeDistVertical + ", swipeDistHorizontal: " + swipeDistHorizontal);
                        break;
                }
            }
        }
    }
}
