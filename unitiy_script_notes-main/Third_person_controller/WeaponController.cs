using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    int attackIndex;

    public GameObject handSword;
    public GameObject backSword;
    public GameObject handAxe;
    public GameObject backAxe;

    bool isSword = false;
    bool isAxe = false;
    bool canAttack = true;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isS", isSword);
        anim.SetBool("isA", isAxe);

        if (Input.GetKeyDown(KeyCode.E))
        {
            isSword = !isSword;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isAxe = !isAxe;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isSword == true && canAttack) // Mouse'un sol tuþu 
        {
            attackIndex = Random.Range(0, 4); //0,1,2,3
            anim.SetInteger("attackIndex", attackIndex);
            anim.SetTrigger("Saldýr");
        }

        if(isSword == true)
        {
            GetComponent<IKLook>().IKLookAzal();
        }

        if(isSword == false)
        {
            GetComponent<IKLook>().IKLookArt();
        }
    }


    //Envanterde bulunanlar
    void equipOfSword()
    {
        backSword.SetActive(false);
        handSword.SetActive(true);
    }

    void unEquipOfSword()
    {
        backSword.SetActive(true);
        handSword.SetActive(false);
    }

    void equipOfAxe()
    {
        backAxe.SetActive(false);
        handAxe.SetActive(true);
    }

    void unEquipOfAxe()
    {
        backAxe.SetActive(true);
        handAxe.SetActive(false);
    }
}
