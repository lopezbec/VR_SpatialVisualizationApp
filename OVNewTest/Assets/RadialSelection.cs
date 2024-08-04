using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.Events;
using UnityEngine.UI;


public class RadialSelection : MonoBehaviour
{
    [Range(2,10)]
    public int NumberofRadialParts; 
    // public OVRInput.Button spawnButton; 

    public GameObject radialPartPrefab; 
    public Transform radialPartCanvas;
    public float angleBetweenParts = 10; 
    private List<GameObject> spawnedParts = new List<GameObject>();
    public UnityEvent<int> OnPartSelected; 
    public Transform handTransform; 
    private int currentSelectedRadianPart = -1; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if (OVRInput.GetDown(spawnButton)){
        //     SpawnRadialParts(); 
        // }
        // if (OVRInput.Get(spawnButton)){
        //     GetSelectedRadialPart(); 
        // }
        // if (OVRInput.GetUp(spawnButton)){
        //     HideAndTriggerSelected(); 
        // }
        
    }

    public void HideAndTriggerSelected(){
        OnPartSelected.Invoke(currentSelectedRadianPart);
        radialPartCanvas.gameObject.SetActive(false);

    }
    
    // This code is to select the radial prefab part using proyections in 3D. 
    public void GetSelectedRadialPart(){
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);
        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward); 
        if (angle < 0){
            angle += 360; 
        }
        currentSelectedRadianPart = (int) angle * NumberofRadialParts / 360; 
        for (int i = 0; i < spawnedParts.Count; i++){
            if (i == currentSelectedRadianPart){
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;

            }
            else{
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }
    // The radial prefab is created in a list of gameobjects, the list is cleaned at the begining 
    public void SpawnRadialParts(){
        // Spawning the radial part in the hand 
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation; 

        foreach(var item in spawnedParts){
            Destroy(item);
        }

        spawnedParts.Clear();

        for (int i = 0; i < NumberofRadialParts; i++){
            float angle = -i * 360 / NumberofRadialParts - angleBetweenParts/2;  
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);
            
            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position; 
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle; 

            spawnedRadialPart.GetComponent<Image>().fillAmount = 1 / (float)NumberofRadialParts - (angleBetweenParts/360);

            spawnedParts.Add(spawnedRadialPart);
            
        }

    }
}
