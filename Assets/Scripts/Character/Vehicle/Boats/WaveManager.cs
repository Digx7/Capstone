using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveManager : MonoBehaviour
{
    public MeshFilter meshFilter;
    public float amplitued = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    private void Update()
    {
        offset += Time.deltaTime * speed;
        RenderWave();
    }

    private void RenderWave()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = GetWaveHeight(transform.position.x + vertices[i].x);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }

    public float GetWaveHeight(float _x)
    {
        return amplitued * Mathf.Sin(_x / length + offset);
    }
}
