using System.Collections;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Transform ground;
    public float m1,m2;
    public float g=9.80665f;

    private Vector3 newPos;
    void Start()
    {
        StartCoroutine(FallToGround());
    }

    public IEnumerator FallToGround(){
        newPos=transform.position;
        bool IsGrounded=false;
        while(!IsGrounded) {
            IsGrounded=transform.position.y<=transform.localScale.y/2+ground.localScale.y/2;
            float gf=g*(m1*m2)/Mathf.Pow(Vector3.Distance(transform.position, ground.position),2);
            newPos.y-=gf;
            transform.position=newPos;
            Debug.Log("!OnGround");
            yield return null;
        }
        
        newPos.y=transform.localScale.y/2+ground.localScale.y/2;
        transform.position=newPos;
        Debug.Log("OnGround");
    }
}
