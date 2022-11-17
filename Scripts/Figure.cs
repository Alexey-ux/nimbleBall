using UnityEngine;

public class Figure : MonoBehaviour{
    public Transform StrengthBar;

    public byte initialStrength;

    private byte str;
    public byte strength
    {
        get { return str; }
        set
        {
            str = value;
            float y = StrengthBar.localScale.y / initialStrength * str;
            float x = StrengthBar.localScale.x;

            StrengthBar.localScale = new Vector2(x, y);

            if (str <= 0)
                Destroy(gameObject);
        }
    }

    private void Awake()
    {
        strength = initialStrength;
    }
}