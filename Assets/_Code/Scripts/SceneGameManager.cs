using UnityEngine;

public class SceneGameManager : MonoBehaviour
{

    [SerializeField]
    GameObject progressBar;
    
    [SerializeField]
    public int garbageCounter = 0;

     [SerializeField]
    public int maxGarbage = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(garbageCounter>=maxGarbage){
            //gameOver
        }

        int percentage = (garbageCounter*maxGarbage/100/10);

        //apply percentage to bar
        Vector3 temp = progressBar.transform.localScale;
        temp.x=percentage;
        progressBar.transform.localScale = temp;
    }
}
