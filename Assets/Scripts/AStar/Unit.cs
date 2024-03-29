﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public float timeElapsedSinceLastSearch;
    public const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public float speed = 2;
    public float turnSpeed = 3;
    public float turnDist = 5;
    public float stoppingDist = 10;

    [HideInInspector] public float chaseMultiplier;

    Path path;

    void Update()
    {
        timeElapsedSinceLastSearch += Time.deltaTime;
    }

    //This is the method that we sould call into the FSM
    public IEnumerator SearchPath(Transform target){
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        PathRequestManager.RequestPath(new PathRequest(transform.position,target.position, OnPathFound));
    }

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDist, stoppingDist);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

    public IEnumerator UpdatePath(Transform target)
    {

        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                if (pathIndex >= path.slowDownIndex && stoppingDist > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D));
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                Vector3 velocity = new Vector3(0.5f, 0.5f, 0.5f);
                Vector3 direction = Vector3.SmoothDamp(transform.up.normalized, (path.lookPoints[pathIndex] - transform.position).normalized, ref velocity, Time.deltaTime * turnSpeed);

                transform.up = new Vector3(direction.x, direction.y, 0f);
                transform.Translate(Vector3.up * Time.deltaTime * speed * speedPercent * chaseMultiplier, Space.Self);
            }

            yield return null;
        }

    }

	public void OnDrawGizmos() {
		if (path != null) {
            path.DrawWithGizmos();
		}
	}
}
