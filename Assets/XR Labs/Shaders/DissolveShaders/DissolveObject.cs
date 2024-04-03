using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Renderer))]
public class DissolveObject : MonoBehaviour
{
    [SerializeField] float noiseStrength = 0.25f;
    [SerializeField] float noiseScale = 50f;
    [SerializeField] float cutoffHeight = 1.0f;
    [SerializeField] float edgeWith = 1.0f;
    [SerializeField] float TimeDelay = 0.15f;
    [SerializeField] float TimetoStop = 2.0f;
    
    private Material material;
    [SerializeField] bool hasStarted = true;
   public float timer = 0f;
    float InitHight = 0f;
    private void Awake()
    {
        
        material = GetComponent<Renderer>().material;

       
    }
    private void Start()
    {
        InitHight = cutoffHeight;

        material.SetFloat("_NoiseScale", noiseScale);
        material.SetFloat("_EdgeWidth", edgeWith);
        material.SetFloat("_NoiseStrength", noiseStrength);

    }

    private void Update()
     {
        if (hasStarted)
        {
            timer += Time.deltaTime;
           

            var time = Time.time * Mathf.PI *TimeDelay;

            float height = transform.position.y;
            height += Mathf.Sin(time) * (cutoffHeight / 2.0f);
            SetHeight(height);
        }
        
        if (hasStarted&& timer > TimetoStop)
        {
            
            material.SetFloat("_CutoffHeight", InitHight);
            hasStarted = false;
        }
     }
 

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        if (material.GetFloat("_CutoffHeight") >= cutoffHeight)
        {
            //use here Ienum
        
        }
    }
}
