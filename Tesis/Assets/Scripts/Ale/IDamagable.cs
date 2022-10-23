using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public interface IDamagable
{
    public void GetDamage(int damage);
    public void GetDamage(int damage, Vector3 point, Vector3 normal);
}
