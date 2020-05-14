using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;
    TextMesh tM;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 snapPos;

        snapPos.x = Mathf.RoundToInt(transform.position.x / 10f) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / 10f) * gridSize;

        tM = GetComponentInChildren<TextMesh>();
        tM.text = snapPos.x/gridSize + "," + snapPos.z/gridSize;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);
    }
}
