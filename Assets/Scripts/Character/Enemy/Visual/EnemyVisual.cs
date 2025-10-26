using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    //public Animator animator;

    //, RuntimeAnimatorController anim
    public void ApplyVisual(Mesh mesh, Material material)
    {
        meshRenderer.sharedMesh = mesh;
        meshRenderer.sharedMaterial = material;
        //animator.runtimeAnimatorController = anim;
    }
}
