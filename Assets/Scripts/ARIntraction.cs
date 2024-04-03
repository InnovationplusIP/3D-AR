using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteraction : MonoBehaviour
{
    [SerializeField] ARRaycastManager arRaycastManager;
    [SerializeField] Camera araCamManager;
    private bool placementCheck;
    private Pose placementPose;

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject productUI;
    [SerializeField] private GameObject []productBtn;
    [SerializeField] private GameObject[] placementObjects;
    GameObject temp=null;
    bool stopUpdate=false;

    private void Update()
    {
        if (!stopUpdate)
        {

        UpdatePlacement();
        UpdatePlacementIndicator();
       
        }
    }

    private void UpdatePlacement()
    {
     
        Vector3 screenCenter = araCamManager.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
     
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        
        placementCheck = hits.Count > 0;
        if (placementCheck)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementIndicator()
    {
        indicator.SetActive(placementCheck);
        if (placementCheck)
        {
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            productUI.SetActive(true);
        }
        else
        {
           productUI.SetActive(false);
        }
    }
   public void PlacementSelect(int i)
    {
         if (placementCheck)
        {
             PlaceObject(i);

            foreach (GameObject game in productBtn)
            {
                    game.SetActive(false);
              
            }
           
            i++;
        if (i < productBtn.Length)
        {
            productBtn[i].SetActive(true);
              
            }
            else
            {
                productBtn[0].SetActive(true);
            }
        }
    }
    public void PlaceObject(int randomIndex)
    {
        GameObject prefab = placementObjects[randomIndex];
        Vector3 objectSize = GetObjectSize(prefab);


        Vector3 placementPosition = temp ? temp.transform.position: placementPose.position;
        placementPosition.y = placementPose.position.y - (objectSize.y / 2);
        if (temp!=null)
        {
            Destroy(temp);
        }

       temp= Instantiate(prefab, placementPosition, placementPose.rotation);
     //  temp.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
      
        stopUpdate=true;    
       indicator.SetActive(false);    
    }

    private Vector3 GetObjectSize(GameObject obj)
    {
      
        Collider objCollider = obj.GetComponentInChildren<Collider>();
        if (objCollider != null)
        {
            return objCollider.bounds.size;
        }
        else
        {
           
            return Vector3.one;
        }
    }

}
