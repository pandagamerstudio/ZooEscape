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
            for (int i = 0; i < paredes.Length; i++) {
                paredes[i].SetActive(false);
               
            }
            

        }
        }
  public  void sumar() {
      //  if (activados < 2)
            activados++;

        if (activados == 2) {

            if (paredes.Length != 0) {
                for (int i = 0; i < paredes.Length; i++) {
                    paredes[i].SetActive(false);
                  
                }
               
            }
            if (paredesBotonVerde.Length != 0) {
                for (int i = 0; i < paredesBotonVerde.Length; i++)
                {
                    paredesBotonVerde[i].SetActive(true);
                }
            }
            
          


        }
    }

    public void restar() {
        if (activados>0)
            activados--;
        if (activados < 2) {
            if (paredes.Length != 0)
            {
                for (int i = 0; i < paredes.Length; i++)
                {
                    paredes[i].SetActive(true);

                }
            }
            if (paredesBotonVerde.Length != 0)
            {
                for (int i = 0; i < paredesBotonVerde.Length; i++)
                {
                    paredesBotonVerde[i].SetActive(false);
                }
            }
        }
       

    }
}
