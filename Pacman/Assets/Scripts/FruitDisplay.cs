using UnityEngine;
using UnityEngine.UI;

public class FruitDisplay : MonoBehaviour
{
    public Image fruitImage;
    public Sprite[] fruitSprites;

    public void SetFruitForLevel(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, fruitSprites.Length - 1);
        fruitImage.sprite = fruitSprites[index];
        fruitImage.enabled = true;
    }
}
