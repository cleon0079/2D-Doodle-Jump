using System;

[Serializable]
public class PlatformSetting
{
    public enum PlatformType
    {
        Normal,
        Broken,
        Once,
        Doudle,
        Horizontal,
        Vertical,
        Ground
    }

    [Serializable]
    public class NormalPlatform
    {
        public PlatformType type = PlatformType.Normal;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class BrokenPlatform
    {
        public PlatformType type = PlatformType.Broken;
        public int index = 1;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class OncePlatform
    {
        public PlatformType type = PlatformType.Once;
        public int index = 2;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class DoublePlatform
    {
        public PlatformType type = PlatformType.Doudle;
        public int index = 3;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class HorizontalPlatform
    {
        public PlatformType type = PlatformType.Horizontal;
        public int index = 4;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
        public float distance;
        public float speed;
    }

    [Serializable]
    public class VerticalPlatform
    {
        public PlatformType type = PlatformType.Vertical;
        public int index = 5;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
        public float distance;
        public float speed;
    }

    [Serializable]
    public class GroundPlatform
    {
        public PlatformType type = PlatformType.Ground;
        public int index = 6;
        public float jumpHeight;
    }

    public NormalPlatform normalPlatform;
    public BrokenPlatform brokenPlatform;
    public OncePlatform oncePlatform;
    public DoublePlatform doublePlatform;
    public HorizontalPlatform horizontalPlatform;
    public VerticalPlatform verticalPlatform;
    public GroundPlatform groundPlatform;
}
