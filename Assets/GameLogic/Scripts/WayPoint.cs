using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : Actor
{
    public PointType pointType;
}

public enum PointType{
    CHICKEN_FOOD,
    CHICKEN_WAY
}