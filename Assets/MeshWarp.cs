using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshWarp : MonoBehaviour
{
    public float SizeX;
    public float SizeY;
    public float SizeZ;
    public Mesh IterativeMesh;
    public Material SetMaterial;
    public Shader SetShader;
    public float Spacing;
    private List<GameObject> objectReference;
    Color objColor;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < SizeX;)
        {
            for (int y = 0; y < SizeY;)
            {
                objColor = new Color(Random.Range(128, 255), Random.Range(128, 255), Random.Range(0, 255), 255);
                string name = "A" + x + ":" + y;
                GameObject tempObject = new GameObject(name);
                tempObject.transform.position = new Vector3(x * Spacing, y * Spacing, SizeZ);
                MeshFilter meshF = tempObject.AddComponent<MeshFilter>();
                meshF.mesh = IterativeMesh;
                MeshRenderer meshRenderer = tempObject.AddComponent<MeshRenderer>();
                SetMaterial.shader = SetShader;
                SetMaterial.SetColor(0, objColor);
                meshRenderer.material = SetMaterial;
                tempObject.transform.parent = gameObject.transform;
                //objectReference.Add(tempObject);
                //fuckyou
                y++;
            }
            x++;
        }

    }
    private float sinx;
    // Update is called once per frame
    void Update()
    {


        int children = transform.childCount;
        GameObject obj;
        Mesh mesh;
        Vector3[] vertices;
        float sinx;
        float siny;
        float sinz;

        for (int i = 0; i < children; ++i)
        {
            obj = transform.GetChild(i).gameObject;
            if (obj != null)
            {
                mesh = obj.GetComponent<MeshFilter>().mesh;
                vertices = mesh.vertices;


                for (int j = 0; j < vertices.Length; j++)
                {

                    float x = vertices[j].x;
                    float y = vertices[j].y;
                    float z = vertices[j].z;
                    float r = Random.Range(0.0f, 2.0f);


                    sinx = (Mathf.Sin(Time.fixedTime * 1 ) * Time.deltaTime);
                    siny = (Mathf.Sin(Time.fixedTime * 1 ) * Time.deltaTime);
                    sinz = (Mathf.Sin(Time.fixedTime * 1 ) * Time.deltaTime);


                    x += x * sinx;
                    y += y * siny;
                    z += z * sinz;
                    Vector3 rot = new Vector3((float)x, (float)y, (float)z);

                    rot = Quaternion.Euler(r,r,r) * rot;




                    vertices[j].Set(rot.x, rot.y, rot.z);


                }
                mesh.vertices = vertices;
                mesh.RecalculateBounds();
            }
        }
    }
}


