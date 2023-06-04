namespace Chroma.Math
{
    public static class Mathf
    {
        public static float Lerp(float a, float b, float t) => a * (1f - t) + b * t;
    }
}