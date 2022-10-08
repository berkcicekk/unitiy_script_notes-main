using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    float weight = .7f;

    Animator anim;
    Camera mainCam;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void OnAnimatorIK(int LayerIndex)
    {
        anim.SetLookAtWeight(weight, .5f, 1.2f, .5f, 5f);
        // vücut parçalarýnýn dönme oraný.

        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
        // Ray(baþlangýç pozisyonu, yönelimi)

        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
    public void IKLookArt()
    {
        weight = Mathf.Lerp(weight, 1f, Time.fixedDeltaTime);
        // Lerp(var olan deðer, gidilecek deðer)
    }
    public void IKLookAzal()
    {
        weight = Mathf.Lerp(weight, 0, Time.fixedDeltaTime);
        // Lerp(var olan deðer, gidilecek deðer)
    }
}



// Not: Animator'deki Layer ayarlarý kýsmýnda 'IK Pass' ayarý açýlmalý.
