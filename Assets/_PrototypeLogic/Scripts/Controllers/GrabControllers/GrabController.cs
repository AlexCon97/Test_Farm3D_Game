
using System;
using UnityEngine;

public class GrabController:IGrabber
{
    public void Grab(Transform ObjectToGrab, Transform ParentObject){
        ObjectToGrab.position=ParentObject.position;
        ObjectToGrab.SetParent(ParentObject);
    }
}
