using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] int outerRingRadius = 20;
    [SerializeField] int totalRings = 6;
    [SerializeField] float startingHeight = 1;

    int cubesInRing;                                                                                    
    int radiusMultiplier;
      //Variable used to calculate the radius of the ring being created
    float circumference;
    float currentRingRadius;
    float cubeHeight; 
    float originRotation;                                   
        //Rotation increment of the origin game object
    
    
    [SerializeField] GameObject cubePrefab;                     
    [SerializeField] GameObject ringOriginPrefab;

    GameObject ringOrigin;
    GameObject spawningCube;
    Vector3 cubeSpawnPos;



    void Start ()
    {
        while (radiusMultiplier < totalRings)
        {
            radiusMultiplier++;

            transform.localScale = new Vector3(transform.localScale.x, startingHeight, transform.localScale.z);

            ringOrigin = Instantiate(ringOriginPrefab, new Vector3(transform.position.x ,startingHeight/2f, 
                transform.position.z), Quaternion.identity, transform);
                //Creating a new empty game object that will be the parent object of the squares in the ring

            ringOrigin.gameObject.name = ("Ring Origin " + (radiusMultiplier));
            
            RingPrep();
            RingBuild();
        }
    }


    /// <summary>
    /// Performing all calculations needed to create a ring
    /// </summary>
    void RingPrep()
    {
        Vector3 ringOriginPos = ringOrigin.transform.position;
      
        currentRingRadius = ((outerRingRadius / totalRings) * radiusMultiplier);

        circumference = 2 * 3.14159f * currentRingRadius;
            //Calculating ring circumference - Formula for circumference: 2 * π * radius

        cubesInRing = Mathf.RoundToInt(circumference / 3);                               

        originRotation = (360f / cubesInRing);                                      

        cubeSpawnPos = new Vector3(ringOriginPos.x, ringOriginPos.y, ringOriginPos.z + currentRingRadius);                                 

        cubeHeight = (currentRingRadius/2f) + startingHeight;
            
    }

    /// <summary>
    /// Assembles a ring using the calculations completed in RingPrep
    /// </summary>
    void RingBuild()
    {
        
       for (int i = 1; i<=cubesInRing; i++)
       {
            spawningCube = Instantiate(cubePrefab, cubeSpawnPos, Quaternion.identity, ringOrigin.transform);

            spawningCube.transform.localScale = new Vector3(transform.localScale.x, cubeHeight, 
                transform.localScale.z);

            spawningCube.transform.position = new Vector3(spawningCube.transform.position.x, cubeHeight/2,
                spawningCube.transform.position.z);

            ringOrigin.transform.Rotate(new Vector3(0, originRotation, 0));                                               
                //Rotate the ringOrigin (and subsequently all child objects)
       }
    }
}
