﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public MeshFilter viewMeshFilter;

    private Mesh viewMesh;

    private Vector3 multiplier = new Vector3(1f, 1f, 1f);

    

    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay){
        while(true){
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawFieldOfView();
    }
    
    void FindVisibleTarget(){
        
        visibleTargets.Clear();
        
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        


        for (int i = 0; i < targetsInViewRadius.Length; i++){
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            
            if(Vector3.Angle(transform.up, dirToTarget) < viewAngle/2){
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)){
                    visibleTargets.Add(target);
                }
            }

        }


    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
            
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void DrawFieldOfView(){
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++){
            float angle = -transform.eulerAngles.z - viewAngle/2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count +1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2)*3];

        vertices[0] = Vector3.zero;

        for(int i = 0; i<vertexCount - 1; i++){
            vertices[i+1] = transform.InverseTransformPoint(viewPoints[i]);

            if(i<vertexCount-2){
                triangles[i*3] = 0;
                triangles[i*3+1] = i+1;
                triangles[i*3+2] = i+2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast (float globalAngle){
        Vector3 dir = DirFromAngle (globalAngle, true);
        RaycastHit2D hit;
        Vector2 closestPoint;

        if (hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask)){
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Stencil")){
                if (Vector2.Distance(hit.point + Vector2.Scale(dir, multiplier), transform.position) < viewRadius){
                    if (hit.collider.OverlapPoint(hit.point + Vector2.Scale(dir, multiplier))){
                        return new ViewCastInfo(true, hit.point + Vector2.Scale(dir, multiplier), hit.distance, globalAngle);
                    } else {
                        closestPoint = ClosestPoint(hit.collider, hit.point + Vector2.Scale(dir, multiplier));
                        return new ViewCastInfo(true, closestPoint, hit.distance, globalAngle);
                    }
                } else {
                    return new ViewCastInfo(true, transform.position + dir * viewRadius, hit.distance, globalAngle);
                }
            } else {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
        }
        else {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle){
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }
    

    public Vector2 ClosestPoint(Collider2D col, Vector2 point)
    {
        GameObject go = new GameObject("tempCollider");
        go.transform.position = point;
        CircleCollider2D c = go.AddComponent<CircleCollider2D>();
        c.radius = 0.1f;
        ColliderDistance2D dist = col.Distance(c);
        Object.Destroy(go);
        return dist.pointA;
    }
    
}