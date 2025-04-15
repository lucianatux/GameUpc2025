using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int currentHits;
    [SerializeField] private float eruptionDuration;
    public GameObject geyserzeroHits;
    public GameObject geyseroneHit;
    public GameObject geysertwoHits;
    public GameObject geyserHits;

        
    void Start()
    {
        currentHits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDamage();
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHits ++;
        }
    }

    private void UpdateDamage()
    {
        if (currentHits == 0)
        {
            geyserzeroHits.SetActive(true);
        }
        if (currentHits == 1) 
        {
            geyserzeroHits.SetActive(false);
            geyseroneHit.SetActive(true); // Mostrar el VFX
        }
        if (currentHits == 2) 
        {
            geyseroneHit.SetActive(false);
            StartCoroutine(Eruption());
        }
        if(currentHits == 3) 
        {
        }

    }

    IEnumerator Eruption()
    {
        Debug.Log("¡Comienza la erupción!");
        geysertwoHits.SetActive(true);

        yield return new WaitForSeconds(eruptionDuration);
        geysertwoHits.SetActive(false);

        currentHits = 0;
        Debug.Log("¡Erupción terminada!");
    }
}
