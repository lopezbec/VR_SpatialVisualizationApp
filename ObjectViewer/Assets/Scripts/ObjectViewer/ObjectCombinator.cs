using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

public class ObjectCombinator : MonoBehaviour
{
    public GameObject[] progressBar;
    public Sprite progressCircleFinished;
    public GameObject completedText, imageToMatchObject, tryAnother, pressEnter;
    public GameObject objectManager;
    public GameObject matchObjectManager;
    public float minimumMatchPercentage = 0.4f; // Threshold for similarity
    public float matchAccuracy = 1.0f; // Number of sample points to check for similarity

    public float range = 0.1f;

    public Quaternion[] rotationToMatch;

    public int[]
        correctActiveObject, // Set from the inspector. Indicates which object should be active for each challenge.
        correctMatchingActiveObject; // Set from the inspector. Indicates which object the animation view should be set to.

    private int animationState = 0; // 0 = delay before rotation, 1 = rotating, 2 = delay after rotation.
    private GameObject collect;
    private Transform controlledObjectTransform, matchObjectTransform;
    private int delayCounter = 0, initialDelay = 60, finalDelay = 60;

    private int numberOfChallenges = 9;
    private readonly int progress = 0;
    private GameObject userObject, referenceObject;


    private void Start()
    {
        collect = GameObject.Find("CollectData");
        controlledObjectTransform = objectManager.GetComponent<Transform>();
        numberOfChallenges =
            rotationToMatch.Length +
            1; // The number of challenges is given by the number of rotations entered into the array.

        objectManager.GetComponent<CombinatorObjectManager>().SetActive(); // Set the next correct object to be active.
        referenceObject = matchObjectManager.GetComponent<ObjectManager>()
            .SetAndReturnActive(correctMatchingActiveObject[progress]); // Set the next correct object to be active.

        matchObjectTransform = matchObjectManager.GetComponent<Transform>();
        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);

        userObject = objectManager.transform.GetChild(0).gameObject;


        for (var i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
            progressBar[i].SetActive(i < numberOfChallenges - 1);
    }

    private void Update()
    {
        if (Input.GetKeyUp("i")) // Used for debugging. Checks if the current rotation of the object is correct or not without moving on to the next rotation.
            Debug.Log(CompareQuaternions(controlledObjectTransform.rotation, rotationToMatch[progress], 0.1f));

        if (Input.anyKey) // Gets rid of the "Try another" text.
            tryAnother.SetActive(false);

        if (Input.GetKeyUp(KeyCode.Return))
        {
            //ResetObjectRotations(userObject, referenceObject);
            const float distanceMoved = 100.0f;

            // 2. Copy and Isolate Objects
            GameObject userCopy = Instantiate(userObject);
            GameObject referenceCopy = Instantiate(referenceObject, matchObjectTransform.position, Quaternion.identity);

            // 3. Clean Up Objects
            //may not be needed but can enable if strange behaviour occurs
            //DestroyComponentsOnObject(userCopy, new[] { "VoxelAdder", "EdgeRemover" });

            
            DestroyColliders(userCopy);
            DestroyDisabledObjects(userCopy);
            
            
            Destroy(referenceCopy.GetComponent("ObjectManager"));
            Destroy(referenceCopy.GetComponent("CombinatorReferenceViewerRig"));
            
            DestroyEdges(userCopy);
            DestroyEdges(referenceCopy);
            
            // 4. Combine Sub-Meshes and Create OBBs

            CombineSubMeshes(userCopy);
            CombineSubMeshes(referenceCopy);
            
            // 5. Move Objects away from user
            userCopy.transform.position += Vector3.back * distanceMoved;
            referenceCopy.transform.position += Vector3.back * distanceMoved;
            
            // 6. Align meshes based on centroids
            Mesh meshA = userCopy.GetComponent<MeshFilter>().mesh;
            Mesh meshB = referenceCopy.GetComponent<MeshFilter>().mesh;
            
            //6.1 Create Sample List of Vertex Points
            List<Vector3> pointsA = SamplePoints(meshA, (int)Math.Round(meshA.vertexCount * matchAccuracy));
            List<Vector3> pointsB = SamplePoints(meshB, (int)Math.Round(meshB.vertexCount * matchAccuracy));
            
            Vector3 centroidA = CalculateCentroid(pointsA);
            Vector3 centroidB = CalculateCentroid(pointsB);
            AlignPointsToCentroid(pointsA, centroidA);
            AlignPointsToCentroid(pointsB, centroidB);

            bool similarMesh = false;
            
            //rotate objects and try to match
            List<Matrix4x4> rotationMatrices = Get90DegreeRotationMatrices();
            foreach (var rotationMatrix in rotationMatrices)
            {
                // Apply rotation to the user object
                RotateMesh(userCopy, rotationMatrix);
                
                // Update pointsA after rotation
                pointsA = SamplePoints(meshA, (int)Math.Round(meshA.vertexCount * matchAccuracy));
                AlignPointsToCentroid(pointsA, CalculateCentroid(pointsA));
                
                float hausdorffDistance = CalculateHausdorffDistance(pointsA, pointsB);
                Debug.Log("Hausdorff Distance: " + hausdorffDistance);
                
                if (hausdorffDistance < minimumMatchPercentage)
                {
                    similarMesh = true;
                    break;
                }
            }

            if (similarMesh) 
            {
                Debug.Log("Meshes are similar.");
            }
            else
            {
                Debug.Log("Meshes are different.");
            }
            
            //Clean up Objects once done
            Destroy(userCopy);
            Destroy(referenceCopy);
            
        }


        /*
        if (CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range))
        { // If the object is approximately aligned to the target rotation for this challenge.

            controlledObject.eulerAngles = new Vector3(0, 0, 0);

            if (progress < numberOfChallenges)
            {
                if (collect != null)
                {
                    CollectData data = collect.GetComponent<CollectData>() as CollectData;
                    data.newSubmission(SceneManager.GetActiveScene().name, true, progress + 1, numberOfChallenges);
                }

                progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.

                objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); // Set the next correct object to be active.
                matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);


                if (progress >= numberOfChallenges - 1)
                { // If the user has finished all the challenges, display the ending message.
                    completedText.SetActive(true);
                    imageToMatchObject.SetActive(false);
                    pressEnter.SetActive(false);
                    animationState = -1; // Stop the animation from playing.
                }
                else
                {
                    animationState = 0; // Reset the animation.
                    delayCounter = 0;
                    matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
                }

                if (collect != null)
                {
                    CollectData data = collect.GetComponent<CollectData>() as CollectData;
                    data.resetRotations();
                }
            }
        }
        else
        {
            if (collect != null)
            {
                CollectData data = collect.GetComponent<CollectData>() as CollectData;
                data.newSubmission(SceneManager.GetActiveScene().name, false, progress + 1, numberOfChallenges - 1);
            }
            tryAnother.SetActive(true); // Set the "try another rotation!" text to visible if the user enters an incorrect rotation.
        }
        */
        //tryAnother.SetActive(true);
    }

    private void FixedUpdate()
    {
        /*
        float speed = 1f;

        // Animation controls.
        if (animationState == 0)
        { // delay before rotation.
            if (delayCounter >= initialDelay)
                animationState = 1;
            else
                delayCounter++;
        }
        else if (animationState == 1)
        { // Rotate the matching view towards the goal rotation for this challenge.

            matchObjectTransform.rotation = Quaternion.RotateTowards(matchObjectTransform.rotation, rotationToMatch[progress], speed);

            if (CompareQuaternions(matchObjectTransform.rotation, rotationToMatch[progress], 0.01f))
            { animationState = 2; delayCounter = 0; }
        }
        else if (animationState == 2)
        { // Delay after the rotation.
            if (delayCounter >= finalDelay)
            {
                animationState = 0; // Reset the animation and start all over.
                delayCounter = 0;
                matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
                delayCounter++;
        }
        */
    }

    private bool
        CompareQuaternions(Quaternion a, Quaternion b,
            float range) // Returns true if all the components of a are within "range" of b. In other words, if a and b are "mostly" equal.
    {
        return Mathf.Abs(a.x) <= Mathf.Abs(b.x) + range && Mathf.Abs(a.x) >= Mathf.Abs(b.x) - range &&
               Mathf.Abs(a.y) <= Mathf.Abs(b.y) + range && Mathf.Abs(a.y) >= Mathf.Abs(b.y) - range &&
               Mathf.Abs(a.z) <= Mathf.Abs(b.z) + range && Mathf.Abs(a.z) >= Mathf.Abs(b.z) - range &&
               Mathf.Abs(a.w) <= Mathf.Abs(b.w) + range && Mathf.Abs(a.w) >= Mathf.Abs(b.w) - range;
    }

    private void ResetObjectRotations(params GameObject[] objects)
    {
        foreach (var obj in objects) obj.transform.rotation = Quaternion.identity;
    }

    private void DestroyComponentsOnObject(GameObject obj, string[] componentNames)
    {
        foreach (var componentName in componentNames)
        {
            // Destroy components directly on the object
            var component = obj.GetComponent(componentName);
            if (component != null) Destroy(component);

            // Recursively destroy components on children (handles nested hierarchies)
            foreach (Transform child in obj.transform) 
                DestroyComponentsOnObject(child.gameObject, componentNames);
        }
    }

    private void DestroyColliders(GameObject obj)
    {
        // Destroy colliders directly on the object
        var colliders = obj.GetComponents<Collider>();
        foreach (var collider in colliders) Destroy(collider);

        // Recursively destroy colliders on children
        foreach (Transform child in obj.transform) DestroyColliders(child.gameObject);
    }

    private void DestroyDisabledObjects(GameObject obj)
    {
        // Check for disabled child and destroy them
        foreach (Transform child in obj.transform)
            if (!child.gameObject.activeInHierarchy)
                Destroy(child.gameObject);
            else
                // Recursively check for disabled objects in children (optional)
                DestroyDisabledObjects(child.gameObject); // Uncomment if needed
    }
    public void DestroyEdges(GameObject parentObject)
    {
        // For Reference object
        Transform childTransform = parentObject.transform.Find("Edges");
        // Check if child was found
        if (childTransform != null) { DestroyImmediate(childTransform.gameObject); }
        
        
        // List to store objects to destroy
        List<GameObject> objectsToDestroy = new List<GameObject>();

        // Get all child transforms
        Transform[] allChildren = parentObject.GetComponentsInChildren<Transform>(true);

        // Find objects to destroy
        foreach (Transform child in allChildren)
        {
            if (child.name.Contains("dottable line") || child.name.Contains("Drawing Face"))
            {
                objectsToDestroy.Add(child.gameObject);
            }
        }

        // Destroy objects after loop completes
        foreach (GameObject obj in objectsToDestroy)
        {
            DestroyImmediate(obj);
        }
    }
    private void CombineSubMeshes(GameObject obj)
    {
        // Get all MeshFilter components in children (recursive for nested objects)
        MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
        //foreach (MeshFilter thing in meshFilters) Debug.Log(thing);

        // Combine meshes if there are more than one MeshFilter found
        if (meshFilters.Length > 1)
        {
            var combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i] = new CombineInstance(); // Initialize CombineInstance
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                // meshFilters[i].gameObject.SetActive(false);
            }
            

            MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combine, true);// Apply triangle optimization
            // meshFilter.transform.position = new Vector3(0, 0, 0);
            meshFilter.mesh.RecalculateBounds();
            meshFilter.mesh.RecalculateNormals();
            meshFilter.mesh.Optimize();


            //Clear meshes from other child MeshFilters (optional)
            // for (var i = 0; i < meshFilters.Length; i++) 
            //     meshFilters[i].mesh = null;
        }
    }
    List<Vector3> SamplePoints(Mesh mesh, int numPoints)
    {
        Vector3[] vertices = mesh.vertices;
        List<Vector3> points = vertices.OrderBy(v => Random.value).Take(numPoints).ToList();
        return points;
    }
    Vector3 CalculateCentroid(List<Vector3> points)
    {
        Vector3 centroid = Vector3.zero;
        foreach (var point in points)
        {
            centroid += point;
        }
        return centroid / points.Count;
    }

    void AlignPointsToCentroid(List<Vector3> points, Vector3 centroid)
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i] -= centroid;
        }
    }
    List<Matrix4x4> Get90DegreeRotationMatrices()
    {
        List<Matrix4x4> rotationMatrices = new List<Matrix4x4>();

        // 90 degree increments around X, Y, and Z axes
        int[] angles = { 0, 90, 180, 270 };

        foreach (int x in angles)
        {
            foreach (int y in angles)
            {
                foreach (int z in angles)
                {
                    Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(x, y, z));
                    rotationMatrices.Add(rotationMatrix);
                }
            }
        }

        return rotationMatrices;
    }

    void RotateMesh(GameObject obj, Matrix4x4 rotationMatrix)
    {
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = rotationMatrix.MultiplyPoint3x4(vertices[i]);
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
    float CalculateHausdorffDistance(List<Vector3> pointsA, List<Vector3> pointsB)
    {
        float maxDistanceAB = MaxMinDistance(pointsA, pointsB);
        float maxDistanceBA = MaxMinDistance(pointsB, pointsA);
        return Mathf.Max(maxDistanceAB, maxDistanceBA);
    }

    float MaxMinDistance(List<Vector3> source, List<Vector3> target)
    {
        float maxMinDistance = 0.0f;

        foreach (Vector3 pointA in source)
        {
            float minDistance = float.MaxValue;
            foreach (Vector3 pointB in target)
            {
                float distance = Vector3.Distance(pointA, pointB);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            //Debug.Log(minDistance);
            if (minDistance > maxMinDistance)
            {
                maxMinDistance = minDistance;
            }
        }

        return maxMinDistance;
    }
}