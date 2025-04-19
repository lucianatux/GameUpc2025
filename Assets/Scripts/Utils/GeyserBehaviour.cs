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
        public GameObject geysernotCharged;
    public GameObject geysertwoHits;
    private bool isCharged;
    [SerializeField] private Animator animator;
    private bool eruptionEnded;
    [SerializeField] float chargingRange;




    void Start()
    {
        currentHits = 0;
        eruptionEnded = false;
        isCharged = false;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDamage();
        ChargeGeyser();
        if (isCharged && Input.GetKeyDown(KeyCode.K))
        {
            currentHits ++;
            animator.SetInteger("currentHits", currentHits);

        }
    }


    private void ChargeGeyser()
    {
        float chargingCooldown = Random.Range(5, chargingRange);
        float chargingTimer = chargingCooldown;
        chargingRange -= Time.deltaTime;
        Debug.Log("cargadando");

        if (chargingRange <= 0)
        {
            animator.SetBool("isCharged", isCharged);

            Debug.Log("cargada");
            isCharged = true;
            UpdateDamage();
        }



    }
private void UpdateDamage()
    {
        if (currentHits == 0 && isCharged == true)
        {
            geyserzeroHits.SetActive(true);
            geysernotCharged.SetActive(false);
        }
        if (currentHits == 0 && isCharged == false)
        {
            geysernotCharged.SetActive(true);
            geyserzeroHits.SetActive(false);
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

    }

    IEnumerator Eruption()
    {
        Debug.Log("¡Comienza la erupción!");
        geysertwoHits.SetActive(true);

        yield return new WaitForSeconds(eruptionDuration);
        geysertwoHits.SetActive(false);
        eruptionEnded = true;
        animator.SetBool("eruptionEnded", eruptionEnded);

        currentHits = 0;
        Debug.Log("¡Erupción terminada!");
    }
}
