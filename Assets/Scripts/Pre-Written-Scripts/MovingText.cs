using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingText : MonoBehaviour
{
    public TMP_Text textComponent;
    [SerializeField] float waitTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSecond());
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.unscaledTime * 2f + orig.x * 0.01f) * 5f, 0);
            }
        }

        for (int k = 0; k < textInfo.meshInfo.Length; ++k)
        {
            var meshInfo = textInfo.meshInfo[k];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, k);

        }

    }

    IEnumerator waitSecond()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
