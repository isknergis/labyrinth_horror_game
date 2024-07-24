using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayController : MonoBehaviour
{
    [SerializeField] private float smooth = 5f; // Sway hareketinin ne kadar yumuþak olacaðýný belirler.
    [SerializeField] private float multiplier = 2f; // Fare hareketlerinin dönüþ açýsýna olan etkisini belirler.
    [SerializeField] private float maxAngle = 45f; // Maksimum dönme açýsý

    private Quaternion initialRotation;

    private void Start()
    {
        // Baþlangýç rotasyonunu kaydet
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Fare hareketlerini alýr ve multiplier ile çarparak daha belirgin hale getirir.
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        // X ekseni etrafýnda dönüþ açýsýný hesaplar.
        Quaternion rotationX = Quaternion.AngleAxis(Mathf.Clamp(mouseY, -maxAngle, maxAngle), Vector3.right);
        // Y ekseni etrafýnda dönüþ açýsýný hesaplar.
        Quaternion rotationY = Quaternion.AngleAxis(Mathf.Clamp(mouseX, -maxAngle, maxAngle), Vector3.up);

        // Hedef dönüþ açýsýný belirler.
        Quaternion targetRotation = initialRotation * rotationX * rotationY;

        // Mevcut dönüþ açýsýný hedef dönüþ açýsýna doðru yumuþak bir þekilde geçirir.
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
