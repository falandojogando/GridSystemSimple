using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Snap : MonoBehaviour
{
    public GameObject mark_model, mark; // CHAMEI DE MARK PQ ESTAVA SEM IDEIA, MAS ESSE É O GAMEOBJECT VERDE QUE VAI MOSTRAR ONDE ESTÁ CONSTUIDO
    RaycastHit hit;
    RaycastHit[] hits; // AQUI SERVE PARA IDENTIFICAR SE TEM MAIS QUE 1 HIT NA MESMA POSIÇÃO

    public Vector3 gridSize = new Vector3(10f,1f,10f); // TAMANHO DA GRADE PARA QUE TENHA UMA CONSTRUÇÃO ORGANIZADA

    [Header("BUILDINGS")]
    public GameObject house;
    public Material[] houseMaterial; //PARA AS CASAS SEREM GERADAS COM DIFERENTES MATERIAIS
    void Update()
    {


            
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
              

            
             if (hits.Length > 1)
            {
                foreach (RaycastHit item in hits)
                {
                        if (item.transform.tag == "Ground")
                        {
                            hit = item;
                            break;
                        }
                }
            }
            else if (hits.Length == 1)
            {
                hit = hits[0];
            }

             if (hits.Length == 0 && !Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (mark != null)
                {
                    Destroy(mark);
                    mark = null;
                }
            }


        if (hit.transform != null)
        {
            
            if (hit.transform.tag == "Ground")
                {
                    //PARA INSTANCIAR O "mark"
                    if (mark == null) 
                    {
                        mark = Instantiate(mark_model);
                        mark.transform.localScale = new Vector3(gridSize.x / 10, mark_model.transform.localScale.y, gridSize.z / 10);
                    }

                print(mark.transform.localScale);
                //ESSA PARTA DE GRID QUE O "mark" 
                Vector3 posMark = new Vector3 (Mathf.Floor(hit.point.x/ gridSize.x) * gridSize.x + gridSize.x/2, 
                hit.point.y + 0.001f, 
                Mathf.Floor(hit.point.z / gridSize.z) * gridSize.z + gridSize.z/2);
                //Mathf.Floor = se um número for float(tiver o "." ex: 1.2f) ele retorna o menor número inteiro (ex: 10.2f = 10).
                //Então ele pega o tamanho da grid como referencia e esse "gridSize.x/2" foi um valor que encontrei para que ele fique bem posicionado.

                mark.transform.position = posMark;


                // PARTE DA CONSTRUÇÃO
                if (Input.GetMouseButton(0)) 
                {
                    if (!mark.GetComponent<Mark>().RaycastCheck()) // PARA EVITAR QUE O PLAYER CONSTRUA DUAS CASAS NO MESMO LUGAR
                    {
                        GameObject houseTemp = Instantiate(house, new Vector3(mark.transform.position.x, hit.point.y + house.transform.localScale.y / 2 + 0.0015f, mark.transform.position.z), Quaternion.identity);
                        houseTemp.GetComponent<MeshRenderer>().material = houseMaterial[Random.Range(0, houseMaterial.Length)]; //GERANDO CASAS DE MATERIAIS ALEATÓRIOS
                    }
                    else
                    {
                        //Não está colidindo
                    }
                }
                // PARTE DE DESTRUIR CONSTRUÇÃO
                if (Input.GetMouseButton(1) && mark.GetComponent<Mark>().RaycastCheck()) // PARA SABER SE O "mark" ESTÁ COLIDINDO COM ALGUMA CASA
                {
                    Destroy(mark.GetComponent<Mark>().hit.transform.gameObject);
                }

                }
            }

    }
}
