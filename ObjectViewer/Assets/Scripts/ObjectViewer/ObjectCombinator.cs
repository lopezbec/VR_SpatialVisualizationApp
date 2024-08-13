using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
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

    public int[]
        correctActiveObject, // Set from the inspector. Indicates which object should be active for each challenge.
        correctMatchingActiveObject; // Set from the inspector. Indicates which object the animation view should be set to.
    
    private GameObject collect;
    private Transform controlledObjectTransform, matchObjectTransform;

    private int numberOfChallenges = 3;
    private int progress = 0;
    private GameObject userObject, referenceObject;


    private void Start()
    {
        //I don't know what this is collecting but I'm too scared to remove it
        collect = GameObject.Find("CollectData");
        controlledObjectTransform = objectManager.GetComponent<Transform>();

        objectManager.GetComponent<CombinatorObjectManager>().SetActive(); // Set the next correct object to be active.
        referenceObject = matchObjectManager.GetComponent<ObjectManager>()
            .SetAndReturnActive(correctMatchingActiveObject[progress]); // Set the next correct object to be active.

        matchObjectTransform = matchObjectManager.GetComponent<Transform>();
        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);

        userObject = objectManager.transform.GetChild(0).gameObject;


        for (var i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
            progressBar[i].SetActive(i < numberOfChallenges);
    }

    private void Update()
    {

        if (Input.anyKey) // Gets rid of the "Try another" text.
            tryAnother.SetActive(false);

        if (Input.GetKeyUp(KeyCode.Return))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            

            // 1. Random distance just to get the copies of objects away from the user
            const float distanceMoved = 100.0f;

            // 2. Copy and Isolate Objects
            GameObject userCopy = Instantiate(userObject);
            GameObject referenceCopy = Instantiate(referenceObject, matchObjectTransform.position, Quaternion.identity);

            // 3. Clean Up Objects
            //may not be needed but can enable if strange behaviour occurs
            //DestroyComponentsOnObject(userCopy, new[] { "VoxelAdder", "EdgeRemover" });

            /*
             * Below is an important part of optimizing this process. Later when we create one big mesh to represent the object
             * For comparison we want the least number of vertices while still making the objects look similar.
             * This is done by destroying the colliders, disabled objects, unused components, and ESPECIALLY edges.
             * Unity is not able to create meshes over 65535 vertices so we have to remove as many unneeded objects as possible. 
             */

            DestroyColliders(userCopy);
            DestroyDisabledObjects(userCopy);


            Destroy(referenceCopy.GetComponent("ObjectManager"));
            Destroy(referenceCopy.GetComponent("CombinatorReferenceViewerRig"));

            DestroyEdges(userCopy);
            DestroyEdges(referenceCopy);

            // 4. Combine Sub-Meshes to create overall mesh and Create OBBs
            CombineSubMeshes(userCopy);
            CombineSubMeshes(referenceCopy);

            // 5. Move Objects away from user
            userCopy.transform.position += Vector3.back * distanceMoved;
            referenceCopy.transform.position += Vector3.back * distanceMoved;

            //6 Create Sample List of Vertex Points (by default we are choosing the most accurate sample points by testing all points)
            Mesh meshA = userCopy.GetComponent<MeshFilter>().mesh;
            Mesh meshB = referenceCopy.GetComponent<MeshFilter>().mesh;
            
            List<Vector3> pointsA = SamplePoints(meshA, (int)Math.Round(meshA.vertexCount * matchAccuracy));
            List<Vector3> pointsB = SamplePoints(meshB, (int)Math.Round(meshB.vertexCount * matchAccuracy));

            // 7. Align meshes based on centroids (fancy way of saying centering a mesh)
            Vector3 centroidA = CalculateCentroid(pointsA);
            Vector3 centroidB = CalculateCentroid(pointsB);
            AlignPointsToCentroid(pointsA, centroidA);
            AlignPointsToCentroid(pointsB, centroidB);


            //calculate hausdorff distance with all rotations
            /*
             * Take a single point from one mesh and compare the distance of that point to every other point on a second mesh
             * return the smallest distance you found. Now do that with every point on the first mesh. Now that you have a
             * list of minimum distances, take the largest of those distances. This is your Hausdorff distance.
             */
            bool similarMesh = GetHausdorffResult(userCopy, meshA, pointsA, pointsB);

            if (similarMesh)
            {

                if (progress <= numberOfChallenges)
                {
                    progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.

                    DeleteAllExcept(objectManager.transform.GetChild(0).gameObject, "baseObject"); //reset user object
                    
                    ResetObjectRotations(objectManager);
                    ResetObjectRotations(matchObjectManager); //resets rotation of user and reference
					
                    if(progress >= numberOfChallenges){ // If the user has finished all the challenges, display the ending message.
                        completedText.SetActive(true);
                        imageToMatchObject.SetActive(false);
                        pressEnter.SetActive(false);
                    }
                    else{
                        referenceObject = matchObjectManager.GetComponent<ObjectManager>()
                            .SetAndReturnActive(correctMatchingActiveObject[progress]); // Set the next correct object to be active.
                    }
                }
            }
            else
            {
                tryAnother.SetActive(true); // Set the "try another rotation!" text to visible if the user enters an incorrect rotation.
            }


            //Clean up Objects once done
            Destroy(userCopy);
            Destroy(referenceCopy);


            // Stop measuring elapsed time
            stopwatch.Stop();

            // Get the elapsed time as a TimeSpan
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // Output the elapsed time
            Debug.Log($"Elapsed time: {elapsedTime}");
        }
    }
    

    private void ResetObjectRotations(GameObject Object)
    {
        Object.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void DeleteAllExcept(GameObject parentObject, string objectNameToKeep)
    {
        foreach (Transform child in parentObject.transform)
        {
            if (child != parentObject.transform) // Ensure we're not checking the objectManager itself
            {
                if (child.name != objectNameToKeep) // Keep the object with the specified name
                {
                    Destroy(child.gameObject); // Destroy all other child objects
                }
            }
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
        if (childTransform != null)
        {
            DestroyImmediate(childTransform.gameObject);
        }


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

        // Combine meshes if there are more than one MeshFilter found
        if (meshFilters.Length > 1)
        {
            var combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i] = new CombineInstance(); // Initialize CombineInstance
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }


            MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combine, true); // Apply triangle optimization. What that means I have no idea
            meshFilter.mesh.RecalculateBounds();
            meshFilter.mesh.RecalculateNormals();
            meshFilter.mesh.Optimize();
            
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

    bool GetHausdorffResult(GameObject user, Mesh meshA, List<Vector3> pointsA, List<Vector3> pointsB)
    {
        int[,] angles =
        {
            { 90, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },

            { 90, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },

            { 90, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },

            { 90, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },

            { 0, 90, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 },

            { 180, 0, 0 },
            { 0, 0, 90 },
            { 0, 0, 90 },
            { 0, 0, 90 }
        };


        for (int i = 0; i < angles.GetLength(0); i++)
        {
            Quaternion rotation = Quaternion.Euler(angles[i, 0], angles[i, 1], angles[i, 2]);
            // Apply rotation to the user object
            RotateMesh(user, rotation);

            // Update pointsA after rotation
            pointsA = SamplePoints(meshA, (int)Math.Round(meshA.vertexCount * matchAccuracy));
            AlignPointsToCentroid(pointsA, CalculateCentroid(pointsA));

            float hausdorffDistance = CalculateHausdorffDistance(pointsA, pointsB);
            Debug.Log("Hausdorff Distance: " + hausdorffDistance + " angle: " + angles[i, 0] + " " + angles[i, 1] +
                      " " + angles[i, 2]);

            if (hausdorffDistance < minimumMatchPercentage)
            {
                return true;
            }
        }


        return false;
    }

    void RotateMesh(GameObject obj, Quaternion rotation)
    {
        if (obj.TryGetComponent<MeshFilter>(out MeshFilter meshFilter))
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;


            // Transform the vertices based on the rotation in world space
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = rotationMatrix.MultiplyPoint3x4(vertices[i]);
            }

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }
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
            float minDistanceSqr = float.MaxValue;
            foreach (Vector3 pointB in target)
            {
                float distanceSqr = (pointA - pointB).sqrMagnitude;
                if (distanceSqr < minDistanceSqr)
                {
                    minDistanceSqr = distanceSqr;
                }

                if (minDistanceSqr <= maxMinDistance)
                {
                    break; // I don't know why this break statement works so well but it decreases the time for completion by 1000%
                }
            }

            maxMinDistance = Mathf.Max(maxMinDistance, minDistanceSqr);
        }

        return Mathf.Sqrt(maxMinDistance); // Take square root only once at the end
    }
}