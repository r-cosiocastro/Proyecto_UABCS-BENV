using System;
using UnityEngine;

[Serializable]
public struct HSBColor {
    public float H;
    public float S;
    public float B;
    public float A;
    public HSBColor(float h, float s, float b, float a) {
        this.H = h;
        this.S = s;
        this.B = b;
        this.A = a;
    }
    public HSBColor(float h, float s, float b) {
        this.H = h;
        this.S = s;
        this.B = b;
        this.A = 1f;
    }
    public HSBColor(Color col) {
        HSBColor hSBColor = HSBColor.FromColor(col);
        this.H = hSBColor.H;
        this.S = hSBColor.S;
        this.B = hSBColor.B;
        this.A = hSBColor.A;
    }
    public static HSBColor FromColor(Color color) {
        HSBColor result = new HSBColor(0f, 0f, 0f, color.a);
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float num = Mathf.Max(r, Mathf.Max(g, b));
        if (num <= 0f) {
            return result;
        }
        float num2 = Mathf.Min(r, Mathf.Min(g, b));
        float num3 = num - num2;
        if (num > num2) {
            if (g == num) {
                result.H = (b - r) / num3 * 60f + 120f;
            } else if (b == num) {
                result.H = (r - g) / num3 * 60f + 240f;
            } else if (b > g) {
                result.H = (g - b) / num3 * 60f + 360f;
            } else {
                result.H = (g - b) / num3 * 60f;
            }
            if (result.H < 0f) {
                result.H += 360f;
            }
        } else {
            result.H = 0f;
        }
        result.H *= 0.00277777785f;
        result.S = num3 / num * 1f;
        result.B = num;
        return result;
    }
    public static Color ToColor(HSBColor hsbColor) {
        float value = hsbColor.B;
        float value2 = hsbColor.B;
        float value3 = hsbColor.B;
        if (hsbColor.S == 0f) {
            return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.A);
        }
        float b = hsbColor.B;
        float num = hsbColor.B * hsbColor.S;
        float num2 = hsbColor.B - num;
        float num3 = hsbColor.H * 360f;
        if (num3 < 60f) {
            value = b;
            value2 = num3 * num / 60f + num2;
            value3 = num2;
        } else if (num3 < 120f) {
            value = -(num3 - 120f) * num / 60f + num2;
            value2 = b;
            value3 = num2;
        } else if (num3 < 180f) {
            value = num2;
            value2 = b;
            value3 = (num3 - 120f) * num / 60f + num2;
        } else if (num3 < 240f) {
            value = num2;
            value2 = -(num3 - 240f) * num / 60f + num2;
            value3 = b;
        } else if (num3 < 300f) {
            value = (num3 - 240f) * num / 60f + num2;
            value2 = num2;
            value3 = b;
        } else if (num3 <= 360f) {
            value = b;
            value2 = num2;
            value3 = -(num3 - 360f) * num / 60f + num2;
        } else {
            value = 0f;
            value2 = 0f;
            value3 = 0f;
        }
        return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.A);
    }
    public Color ToColor() {
        return HSBColor.ToColor(this);
    }
    public override string ToString() {
        return string.Concat(new object[]
        {
            "H:",
            this.H,
            " S:",
            this.S,
            " B:",
            this.B
        });
    }
    public static HSBColor Lerp(HSBColor a, HSBColor b, float t) {
        float h;
        float s;
        if (a.B == 0f) {
            h = b.H;
            s = b.S;
        } else if (b.B == 0f) {
            h = a.H;
            s = a.S;
        } else {
            if (a.S == 0f) {
                h = b.H;
            } else if (b.S == 0f) {
                h = a.H;
            } else {
                float num;
                for (num = Mathf.LerpAngle(a.H * 360f, b.H * 360f, t); num < 0f; num += 360f) {
                }
                while (num > 360f) {
                    num -= 360f;
                }
                h = num / 360f;
            }
            s = Mathf.Lerp(a.S, b.S, t);
        }
        return new HSBColor(h, s, Mathf.Lerp(a.B, b.B, t), Mathf.Lerp(a.A, b.A, t));
    }
    public static void Test() {
        HSBColor hSBColor = new HSBColor(Color.red);
        Debug.Log("red: " + hSBColor);
        hSBColor = new HSBColor(Color.green);
        Debug.Log("green: " + hSBColor);
        hSBColor = new HSBColor(Color.blue);
        Debug.Log("blue: " + hSBColor);
        hSBColor = new HSBColor(Color.grey);
        Debug.Log("grey: " + hSBColor);
        hSBColor = new HSBColor(Color.white);
        Debug.Log("white: " + hSBColor);
        hSBColor = new HSBColor(new Color(0.4f, 1f, 0.84f, 1f));
        Debug.Log("0.4, 1f, 0.84: " + hSBColor);
        Debug.Log("164,82,84   .... 0.643137f, 0.321568f, 0.329411f  :" + HSBColor.ToColor(new HSBColor(new Color(0.643137f, 0.321568f, 0.329411f))));
    }
}
