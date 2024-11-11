using UnityEngine;

public interface IReleaser
{
    void Release(Transform GrabbedObject, Transform PointToRelease);
}