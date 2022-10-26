using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
public class PlaneRecognition : MonoBehaviour
{
    public Transform BBoxTransform;  //Determine the center of the region in which the plane extraction happens. Use the head pose through the MainCamera
    public Vector3 BBoxExtents;  //determines the Size of the region. If the bounding box extent is 0,0,0, the size of the region from which you can extract planes is boundless.
    public GameObject PlaneGameObject; //MY QUAD
    private GameObject PlaneNames;
    public MLWorldPlanesQueryFlags QueryFlags;
    private float timeout = 5f;
    private float timeSinceLastRequest = 0f;
    private MLWorldPlanesQueryParams _queryParams = new MLWorldPlanesQueryParams();
    public List<GameObject> _planeCache = new List<GameObject>();  //List of planes saved
    //public Text _planeID;
    
    void Start()
    {
        MLWorldPlanes.Start();                       
    }

    private void OnDestroy()
    {
        MLWorldPlanes.Stop();    
    }

    void Update()
    {
        timeSinceLastRequest += Time.deltaTime;
        if (timeSinceLastRequest > timeout)
        {
            timeSinceLastRequest = 0f;
            RequestPlanes();
        }
    }

    void RequestPlanes()
    {
        _queryParams.Flags = QueryFlags;
        _queryParams.MaxResults = 100;
        _queryParams.BoundsCenter = BBoxTransform.position;
        _queryParams.BoundsRotation = BBoxTransform.rotation;
        _queryParams.BoundsExtents = BBoxExtents;

        MLWorldPlanes.GetPlanes(_queryParams, HandleOnReceivedPlanes);
    }

    private void HandleOnReceivedPlanes(MLResult result, MLWorldPlane[] planes, MLWorldPlaneBoundaries[] boundaries)
    {

        for (int i = _planeCache.Count - 1; i >= 0; --i)
        {
            Destroy(_planeCache[i]);
            _planeCache.Remove(_planeCache[i]);
        }

        GameObject newPlane;
        GameObject newChild;

        for (int i = 0; i < planes.Length; ++i)
        {
            newPlane = Instantiate(PlaneGameObject);
            newPlane.name = i.ToString(); 
            newChild = newPlane.transform.GetChild(0).gameObject; //get the first child
            newChild.GetComponent<TMPro.TextMeshPro>().text = i.ToString(); //set the text in TextMeshPro
            newChild.SetActive(false); //Turnin invisible
            newPlane.transform.position = planes[i].Center;
            newPlane.transform.rotation = planes[i].Rotation;
            newPlane.transform.localScale = new Vector3(planes[i].Width, planes[i].Height, 1f);
            _planeCache.Add(newPlane);//add in list
        }
    }

}
