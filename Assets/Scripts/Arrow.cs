using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    
    public float damage = 3;
    [SerializeField]
    float speed = 1.0f;
    private static readonly RaycastHit[] _rayCastHitBuffer = new RaycastHit[100]; 
    private RaycastHit hitInfo;
    
    [SerializeField]
    float maxDistance = 0.2f;


    [SerializeField]
    
    private Vector3 origin,direction;

    private float sphereRadius=0.2f;



    // Update is called once per frame 
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        this.transform.Translate( Vector3.forward * Time.deltaTime  * speed, Space.Self  );
       
        bool wasHit = Physics.SphereCast(origin,sphereRadius,direction,out hitInfo,maxDistance);

        if( !wasHit ) return;


        Orc orc = hitInfo.collider.GetComponent<Orc>();

        if( !orc ) return;

        orc.Damage( damage );

            // Kill the arrow
        Destroy( this.gameObject );
    }

 private void OnDrawGizmosSelected()
   {
        Gizmos.color = new Color( 1, 0, 0, .75f );
        Gizmos.DrawLine( transform.position, transform.position + transform.forward * maxDistance );
   }
         
 

    
}
