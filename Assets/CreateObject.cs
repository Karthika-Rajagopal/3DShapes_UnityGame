using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;

// Adding AR Foundation Package
// Adding ARCore XR Plugin Package
[System.Serializable]
public class CreateObject : MonoBehaviour
{
    public string url = "https://dev.wikibedtimestories.com/webservices/ARIES/api/get_all_game_Char.php?page_size=4";

    public class RootObject
    {
        public GetObjectData[] users;
    }
    void Start()
    {
        StartCoroutine(GetData_Coroutine());
    }
    void Update()
    {
    }
    IEnumerator GetData_Coroutine()
    {
        RootObject myObject = new RootObject();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);

            var jsonArrayString = request.downloadHandler.text;
            var apiDataList = JsonConvert.DeserializeObject<IList<GetObjectData>>(jsonArrayString);
            foreach (var apidata in apiDataList)
            {
                if (apidata.Level == 1)
                {
                    if (apidata.Type == "Cuboid")
                    {
                        float CubeX = float.Parse(apidata.x);
                        float CubeY = float.Parse(apidata.y);
                        float CubeZ = float.Parse(apidata.z);
                        GameObject Cuboid = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Cuboid.transform.position = new Vector3(CubeX, CubeY, CubeZ);
                        Cuboid.GetComponent<Renderer>().material.color = new Color(apidata.color1, apidata.color2, apidata.color3, 1f);
                        Cuboid.transform.localScale = new Vector3(10, 5, 5);
                    }
                }
                if (apidata.Level == 2)
                {
                    if (apidata.Type == "Sphere")
                    {
                        float sphereX = float.Parse(apidata.x);
                        float sphereY = float.Parse(apidata.y);
                        float sphereZ = float.Parse(apidata.z);
                        GameObject Sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Sphere1.transform.position = new Vector3(sphereX, sphereY, sphereZ);
                        Sphere1.GetComponent<Renderer>().material.color = new Color(apidata.color1, apidata.color2, apidata.color3, 1f);
                    }
                    if (apidata.Type == "Cube")
                    {
                        float cubeX = float.Parse(apidata.x);
                        float cubeY = float.Parse(apidata.y);
                        float cubeZ = float.Parse(apidata.z);
                        GameObject Cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Cube1.transform.position = new Vector3(cubeX, cubeY, cubeZ);
                        Cube1.GetComponent<Renderer>().material.color = new Color(apidata.color1, apidata.color2, apidata.color3);
                    }
                }
            }
        }
    }
}

