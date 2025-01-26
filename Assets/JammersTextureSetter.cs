using UnityEngine;

[ExecuteInEditMode]
public class JammersTextureSetter : MonoBehaviour
{
    public Texture[] AllTextures;
    public JammerGenerator[] Jammers;


    [ContextMenu("Set")]
    public void SetAllTextures()
    {
        Jammers = FindObjectsByType<JammerGenerator>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        for (int i = 0; i < Jammers.Length; i++)
        {
            if(i > AllTextures.Length - 1)
            {
                Debug.LogError("SOBRAN JAMMERS");
                return;
            }

            JammerGenerator jammer = Jammers[i];
            jammer.FaceTexture = AllTextures[i];
            jammer.Generate();
        }
    }
}
