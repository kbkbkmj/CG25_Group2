using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int experienceValue = 1; // ����ġ 1�� �ݴϴ�.

    // �ٸ� ��ü�� '�浹(Trigger)'���� ��
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� "Player" �±׸� ������ �ִٸ�
        if (other.CompareTag("Player"))
        {
            // (�˸�) ���⿡ �÷��̾��� ����ġ�� �÷��ִ� �ڵ带 ���߿� �߰��ϸ� �˴ϴ�.
            GameManager.instance.GetExp();

            // �� �ڽ��� ��Ȱ��ȭ�˴ϴ�.
            gameObject.SetActive(false);
        }
    }
}