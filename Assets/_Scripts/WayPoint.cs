using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private WayPoint nextWayPoint;

    /// <summary>
    /// Devuelve la posicion del waypont actual
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    /// <summary>
    /// Retorna el siguiente Waypoint del camino
    /// </summary>
    /// <returns></returns>
    public WayPoint GetNextWayPoint()
    {
        return nextWayPoint;
    }
}
