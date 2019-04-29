using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaypointWalker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform[] _waypoints;

    [SerializeField]
    private SimpleCharacterControl characterControl;

    [SerializeField]
    private float _tolerance = 0.5f;


    [Header("Debug")]
    [SerializeField]
    private float _distance;

    private LinkedList<Vector3> _waypointLocations;
    private LinkedListNode<Vector3> _targetNode;


    private void Awake()
    {
        this._waypointLocations = new LinkedList<Vector3>(this._waypoints.Select(x => x.position));
        this._waypointLocations.AddLast(this.transform.position);
        this._targetNode = this._waypointLocations.First;
    }

    // Upda te is called once per frame
    private void Update()
    {
        this._distance = Vector3.Distance(this.transform.position, this._targetNode.Value);
        bool justChangedWaypoints = false;
        if (this._distance < this._tolerance)
        {
            this.MoveToNextNode();
            justChangedWaypoints = true;
        }
        Vector3 targetPos = this._targetNode.Value;
        Vector3 remain = Utils.ObjectSide(this.transform, targetPos);
        if (justChangedWaypoints && remain.x.IsAbout(0) && !remain.z.IsAbout(0))
        {
            LogHelper.Log(typeof(WaypointWalker), "Next waypoint is right behind us?");
            remain = remain.WithX(1);
        }
        this.characterControl.MoveByInput(1, remain.x, false);
    }

    private void MoveToNextNode()
    {
        LogHelper.Log(typeof(WaypointWalker), "Moving to new waypoint");
        this._targetNode = this._targetNode.Next ?? this._waypointLocations.First;
    }
}
