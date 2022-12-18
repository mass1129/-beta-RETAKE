using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float hp; //���� ���� ü��
    public float maxHp = 50; //���� ��ü ü��
    public Image bossHpImage; //ü�¹� �̹���
    bool isHit = false; //�ǰ� üũ

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
