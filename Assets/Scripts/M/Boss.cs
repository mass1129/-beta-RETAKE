using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float hp; //보스 현재 체력
    public float maxHp = 50; //보스 전체 체력
    public Image bossHpImage; //체력바 이미지
    bool isHit = false; //피격 체크

    void Start()
    {
        bossHpImage.fillAmount = 1;
        hp = maxHp;
    }

    private void Update()
    {
        if(isHit)
            AddDamage();
    }

    public void AddDamage()
    {
        hp -= 0.01f;
        bossHpImage.fillAmount  = hp / maxHp;
        if (hp <= 0)
            Destroy(gameObject);
    }

    public void PointerDown()
    {
        isHit = true;
    }

    public void PointerUp()
    {
        isHit = false;
    }
}
