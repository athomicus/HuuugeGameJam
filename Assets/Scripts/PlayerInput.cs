using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class PlayerInput : MonoBehaviour
{

    private RaycastHit hit;
    private Ray ray;
    private Vector3 touchPosition; 
    private Animator animator;
    //private Rigidbody _mrigidbody;
    public GameObject player;
    private Vector3 endPosition,startPosition;
    private float timeElapsed;
    [SerializeField]
    float speed = 1.0f;
    public float minDistance = 0.15f;
    
  enum PlayerState{Idle,RotationLeft,RotationFront,RotationRight,RunLeft,RunRight,TakeStone,ThrowStone};
  private PlayerState knightPlayerState,prevKnightPlayerState;   
    void Start()
    {
      animator = player.GetComponent<Animator>();
     // _mrigidbody = player.GetComponent<Rigidbody>();
      knightPlayerState = PlayerState.Idle;
       
       
    }

     
    void FixedUpdate()
    {
       if(Input.GetKeyDown(KeyCode.Space))  animator.SetBool("Run",true);
       GetPlayerTouchPositionAndName();
    //  Debug.Log(knightPlayerState);
       switch(knightPlayerState)
       {
         case PlayerState.Idle:
              animator.SetBool("aRun",false);
         break;

         case PlayerState.RotationFront:
              player.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
              knightPlayerState = PlayerState.Idle;
         break;

         case PlayerState.RotationLeft:
              player.transform.rotation = Quaternion.Euler(0.0f, 180f+90.0f, 0.0f);
              knightPlayerState = PlayerState.RunLeft;    
         break;
         
         case PlayerState.RotationRight:
               player.transform.rotation =Quaternion.Euler(0.0f, 180f-90.0f, 0.0f);
              knightPlayerState = PlayerState.RunRight;
         break;

         case PlayerState.RunLeft:
             
              animator.SetBool("aRun",true);
              MovePlayer();
            
         break;
         
         case PlayerState.RunRight:
            
                animator.SetBool("aRun",true);
                MovePlayer();
              
         break;

         case PlayerState.TakeStone:
         break;

        case PlayerState.ThrowStone:
         break;

       }
       
       // 
        
    }   

    private void GetPlayerTouchPositionAndName()
    {
      if((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
      {
              touchPosition = Input.GetTouch(0).position;
         
             
             endPosition = startPosition = player.transform.position;
         
             ray = Camera.main.ScreenPointToRay(touchPosition);
             if (Physics.Raycast(ray, out hit))
             {
                endPosition.x = hit.point.x;
              //  Debug.Log("newPosition=" + newPosition+"  playerPos="+player.transform.position );
                if(player.transform.position.x > endPosition.x ) 
                 {
                  knightPlayerState = PlayerState.RotationLeft; //Wpierw idz do stanu obrocenia gracza w strone biegu
                 }
                
                if(player.transform.position.x < endPosition.x)
                {
                  knightPlayerState = PlayerState.RotationRight;
                }
                
             }
         }
    }
     
  public void MovePlayer()
  { 
     if( Mathf.Abs(player.transform.position.x - endPosition.x) < minDistance )
     {       
       knightPlayerState = PlayerState.RotationFront; //przed idle stanem idz do przekrecenia gracza
       timeElapsed = 0;
       return;
     }
     timeElapsed+= Time.deltaTime;
     player.transform.position = Vector3.Lerp(startPosition, endPosition,  speed * timeElapsed ); // 
    // var lerpPosition = Vector3.Lerp(player.transform.position , newPosition, Time.deltaTime * speed );     
   //  player.transform.Translate( -lerpPosition + player.transform.position );   
  }
}
