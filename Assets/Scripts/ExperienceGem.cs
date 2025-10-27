using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int experienceValue = 1; // 경험치 1을 줍니다.

    // 다른 물체와 '충돌(Trigger)'했을 때
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 "Player" 태그를 가지고 있다면
        if (other.CompareTag("Player"))
        {
            // (알림) 여기에 플레이어의 경험치를 올려주는 코드를 나중에 추가하면 됩니다.
            Debug.Log(experienceValue + " 경험치 획득!");

            // 젬 자신은 비활성화됩니다.
            gameObject.SetActive(false);
        }
    }
}