using UnityEngine;
using TMPro;

public class JiterryText : MonoBehaviour
{
    public TMP_Text textMesh;
    public float jitterAmount = 0.5f; // The amount of jitter

    private TMP_MeshInfo[] cachedMeshInfo;

    void Awake()
    {
        textMesh.ForceMeshUpdate();
        cachedMeshInfo = textMesh.textInfo.CopyMeshInfoVertexData();
    }

    void Update()
    {
        int characterCount = textMesh.textInfo.characterCount;

        if (characterCount == 0)
            return;

        for (int i = 0; i < characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;
            Vector3[] destinationVertices = textMesh.textInfo.meshInfo[materialIndex].vertices;

            Vector3 jitter = Random.insideUnitSphere * jitterAmount;

            // Apply the same jitter to all vertices of the character to prevent distortion
            for (int j = 0; j < 4; j++)
            {
                destinationVertices[vertexIndex + j] = sourceVertices[vertexIndex + j] + jitter;
            }
        }

        textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
}
