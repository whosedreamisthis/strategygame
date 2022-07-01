using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform rootBone;

    public void Setup(Transform originalRootBone)
    {
        MatchAllChildTransforms(originalRootBone, rootBone);
        ApplyExplosionRagdoll(rootBone, 300f, transform.position, 10f);
    }

    private void MatchAllChildTransforms(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform clonedChild = clone.Find(child.name);
            if (clonedChild != null)
            {
                clonedChild.position = child.position;
                clonedChild.rotation = child.rotation;
                MatchAllChildTransforms(child, clonedChild);
            }
        }
    }

    private void ApplyExplosionRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);

            }
            ApplyExplosionRagdoll(child, explosionForce, explosionPosition, explosionRange);

        }
    }
}
