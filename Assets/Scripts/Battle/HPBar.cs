using System.Collections;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private GameObject health;

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmooth(float newHP)
    {
        var currentHP = health.transform.localScale.x;
        var changeAmount = currentHP - newHP;

        while (currentHP - newHP > Mathf.Epsilon)
        {
            currentHP -= changeAmount * Time.deltaTime;
            health.transform.localScale = new Vector3(currentHP, 1f);
            yield return null;
        }

        health.transform.localScale = new Vector3(newHP, 1f);
    }
}
