using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayController : MonoBehaviour
{
    [SerializeField] private float smooth = 5f; // Sway hareketinin ne kadar yumu�ak olaca��n� belirler.
    [SerializeField] private float multiplier = 2f; // Fare hareketlerinin d�n�� a��s�na olan etkisini belirler.
    [SerializeField] private float maxAngle = 45f; // Maksimum d�nme a��s�

    private Quaternion initialRotation;

    private void Start()
    {
        // Ba�lang�� rotasyonunu kaydet
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Fare hareketlerini al�r ve multiplier ile �arparak daha belirgin hale getirir.
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        // X ekseni etraf�nda d�n�� a��s�n� hesaplar.
        Quaternion rotationX = Quaternion.AngleAxis(Mathf.Clamp(mouseY, -maxAngle, maxAngle), Vector3.right);
        // Y ekseni etraf�nda d�n�� a��s�n� hesaplar.
        Quaternion rotationY = Quaternion.AngleAxis(Mathf.Clamp(mouseX, -maxAngle, maxAngle), Vector3.up);

        // Hedef d�n�� a��s�n� belirler.
        Quaternion targetRotation = initialRotation * rotationX * rotationY;

        // Mevcut d�n�� a��s�n� hedef d�n�� a��s�na do�ru yumu�ak bir �ekilde ge�irir.
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
