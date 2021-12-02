using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
    

public class GameState : MonoBehaviour
{


    //STRUCTS
    public struct CocheInstance
    {
        public Coche info;
        public GameObject modelo;
        public bool destruido;

        public CocheInstance(Coche info, GameObject modelo)
        {
            this.info = info;
            this.modelo = modelo;
            this.destruido = false;
        }
    }

    //API
    public Data myData;
    public int contador = 0;
    bool dataLoaded = false;

    //GAMEOBJECTS
    public GameObject[] models = new GameObject[10];

    //Objetos
    public List<CocheInstance> coches = new List<CocheInstance>();


    //API direcciones: 
    string local = "http://127.0.0.1:5000/";
    string ibm = "https://multiagentes-excellent-raven-co.mybluemix.net/";

    IEnumerator UpdatePositions()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(ibm+"getStepInfo"))
        {

            // Request and wait for the desired page.
            yield return www.SendWebRequest();
            myData = JsonUtility.FromJson<Data>(www.downloadHandler.text);
            Debug.Log(myData);
            if (!dataLoaded)
            {
                setData();
                dataLoaded = true;
            }
            else
            {
                updateData();
            }

        }
    }

    IEnumerator CreateModel()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest www = UnityWebRequest.Post(ibm+"createModel", form))
        {

            yield return www.SendWebRequest();
        }
    }


    GameObject randomModel()
    {
        int indice = Random.Range(0, 10);
        return models[indice];
    }


    void setData()
    {
        foreach(Coche coche in myData.coches)
        {
            Debug.Log(coche.posX);
            GameObject cocheObjecto = Instantiate(randomModel(), new Vector3(coche.posX, 0, coche.posY), Quaternion.identity);
            CocheInstance cocheCompleto = new CocheInstance(coche, cocheObjecto);
            coches.Add(cocheCompleto);
        }
    }

    void updateData()
    {
        int indice = 0;
        foreach (Coche coche in myData.coches)
        {
            if (indice > coches.Count-1)
            {
                int desplazamientoY = 7; 
                if(coche.posY == 0)
                {
                    desplazamientoY = 0;
                }
                GameObject cocheObjecto = Instantiate(randomModel(), new Vector3(coche.posX, 0, coche.posY + desplazamientoY), Quaternion.identity);
                CocheInstance cocheCompleto = new CocheInstance(coche, cocheObjecto);
                coches.Add(cocheCompleto);
            }
            else
            {
                Debug.Log("Actualizando valore");
                float speed = 1;
                GameObject miCocheObject = coches[indice].modelo;

                if (!coches[indice].info.noRenderizar)
                {
                    if (coche.finalizo)
                    {
                        Destroy(miCocheObject);
                        coches[indice].info.noRenderizar = true;
                    }
                    else
                    {
                        Debug.Log(coche.angulo);
                        Vector3 target = new Vector3(coche.posX, 0, coche.posY);

                        miCocheObject.transform.position = target;

                        Vector3 temp = transform.rotation.eulerAngles;
                        temp.y = coche.angulo;

                        miCocheObject.transform.rotation = Quaternion.Euler(temp);

                    }
                }
                indice++;
            }
             
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateModel());
        StartCoroutine(UpdatePositions());
     }

    // Update is called once per frame
    void Update()
    {
        if (contador == 60)
        {
            StartCoroutine(UpdatePositions());
            contador = 0;
        }

        contador++;
    }
}
