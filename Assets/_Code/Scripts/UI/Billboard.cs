using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Referencia a la cámara principal
    private Camera mainCamera;

    void Start()
    {
        // Obtener la referencia a la cámara principal
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Obtener la dirección desde el objeto hacia la cámara
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        // Eliminar la componente Y de la dirección (para que no gire en el eje Y)
        directionToCamera.y = 0;

        // Si la dirección es diferente a cero (para evitar la división por cero)
        if (directionToCamera.sqrMagnitude > 0.01f)
        {
            // Obtener la rotación para que el objeto mire a la cámara solo en el eje Y
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Aplicar la rotación
            transform.rotation = targetRotation;
        }
    }
}