using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Referencia a la c�mara principal
    private Camera mainCamera;

    void Start()
    {
        // Obtener la referencia a la c�mara principal
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Obtener la direcci�n desde el objeto hacia la c�mara
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        // Eliminar la componente Y de la direcci�n (para que no gire en el eje Y)
        directionToCamera.y = 0;

        // Si la direcci�n es diferente a cero (para evitar la divisi�n por cero)
        if (directionToCamera.sqrMagnitude > 0.01f)
        {
            // Obtener la rotaci�n para que el objeto mire a la c�mara solo en el eje Y
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Aplicar la rotaci�n
            transform.rotation = targetRotation;
        }
    }
}