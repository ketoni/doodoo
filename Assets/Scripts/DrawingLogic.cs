using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DrawingLogic : MonoBehaviour
{

    private LineRenderer _lineRenderer;
    private List<Vector3> _points;
    public float OffSet = 0.5f;
    public GameObject SpawnObject;
    public SpriteShape Shape;
    
    // Start is called before the first frame update
    void Start()
    {
        _points = new List<Vector3>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            if (_points.Count == 0 || Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), _points[_points.Count - 1]) > OffSet)
            {
                //Debug.Log("MousePosition: " + Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString());
                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _points.Add(new Vector3(v.x,v.y,0));
                _lineRenderer.positionCount = _points.Count;
                _lineRenderer.SetPositions(_points.ToArray());
               // Debug.Log("TEST1: " +_lineRenderer.positionCount.ToString());
               // Debug.Log("TEST2: " + _points.Count.ToString());
                
            }      
        }
        if (Input.GetMouseButtonUp(0))
        {

            GameObject obj = Instantiate(SpawnObject);
            Vector3 spawnPos = _points[_points.Count - 1];
            obj.transform.position = spawnPos;
            Debug.Log("spawnPos: " + obj.transform.position.ToString());
            for (int i = 0; i < _points.Count; i++)
            {
                obj.GetComponent<SpriteShapeController>().spline.InsertPointAt(i, _points[i]-spawnPos);
            }
            //obj.GetComponent<SpriteShapeController>().BakeCollider();
            obj.AddComponent<PolygonCollider2D>();

            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[0]);
            _points = new List<Vector3>();
        }
    }
}
