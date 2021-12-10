using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorBotones : MonoBehaviour
{
    public GameObject [] paredes;
    public GameObject[] paredesBotonVerde;
    // Start is called before the first frame update
  public  int activados = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sumEspecial() {
        if (activados < 2)
            activados++;

        if (activados == 2)
        {
            Debug.Log("Paredes quitadas");
            paredes[0].SetActive(false);
            paredes[1].SetActive(false);

        }
        }
  public  void sumar() {
      //  if (activados < 2)
            activados++;

        if (activados == 2) {

            paredes[0].SetActive(false);
            paredes[1].SetActive(false);
            paredesBotonVerde[0].SetActive(true);
            paredesBotonVerde[1].SetActive(true);


        }
    }

    public void restar() {
        if (activados>0)
            activados--;
        if (activados < 2) {
            paredes[0].SetActive(true);
            paredes[1].SetActive(true);
            paredesBotonVerde[0].SetActive(false);
            paredesBotonVerde[1].SetActive(false);
        }
       

    }
}
