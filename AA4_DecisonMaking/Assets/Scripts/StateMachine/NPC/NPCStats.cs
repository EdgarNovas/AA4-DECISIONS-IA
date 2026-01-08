using UnityEngine;

public class NPCStats : MonoBehaviour
{
    [Header("Necesidades Internas")]
    [Range(0, 100)] public float Hunger = 0f;
    [Range(0, 100)] public float Boredom = 0f;

    [SerializeField] private Vector2 npcRandomHungerOffset;
    [SerializeField] private Vector2 npcRandomBoredomOffset;

    private float npcHungerModifier;
    private float npcBoredomModifier;

    void Start()
    {
        npcHungerModifier = Random.RandomRange(npcRandomHungerOffset.x, npcRandomHungerOffset.y);
        npcBoredomModifier = SetModifier(npcHungerModifier, npcRandomBoredomOffset);
    }

    void Update()
    {
        Hunger += Time.deltaTime * npcHungerModifier;
        Boredom += Time.deltaTime * npcBoredomModifier;

        Hunger = Mathf.Clamp(Hunger, 0, 100);
        Boredom = Mathf.Clamp(Boredom, 0, 100);
    }

    private float SetModifier(float otherModifier, Vector2 offset)
    {
        float tempRandom = Random.RandomRange(offset.x, offset.y);

        if(tempRandom >= otherModifier - 1 && tempRandom <= otherModifier + 1)
        {
            tempRandom = SetModifier(otherModifier, offset);
        }

        return tempRandom;
    }
}
