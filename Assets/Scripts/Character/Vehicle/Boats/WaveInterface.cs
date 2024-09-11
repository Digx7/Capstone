using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveInterface : MonoBehaviour
{
    public MeshFilter meshFilter;
    public float GetWaveHeightAtLocation(Vector3 location)
    {
        Vector3[] vertices = meshFilter.mesh.vertices;

        int bestVertex = 0;
        float shortestDistance = Mathf.NegativeInfinity; 

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexPosition = transform.position + vertices[i];
            float distance = Vector3.Distance(location, vertexPosition);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                bestVertex = i;
            }
        }

        float height = transform.position.y + vertices[bestVertex].y;

        Debug.Log("WaveHeight " + height);
        return height;
    }
}
