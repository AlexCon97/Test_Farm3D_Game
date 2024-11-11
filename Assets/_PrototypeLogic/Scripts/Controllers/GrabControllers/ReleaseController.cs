using UnityEngine;

public class ReleaseController : IReleaser
{
    public void Release(Transform GrabbedObject, Transform PointToRelease){
        GrabbedObject.SetParent(PointToRelease);
        if(PointToRelease)
            GrabbedObject.position=PointToRelease.position;
    }
}
