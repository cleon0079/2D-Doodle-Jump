using System;

// Setting of the platforms of my game
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
        // Type of the platforms
        public PlatformType type = PlatformType.Normal;
        // How high the player can jump
        public float jumpHeight;
        // Min dis of two spawning platform
        public float minHeight;
        // Max dis of two spawning platform
        public float maxHeight;
        // How many percentage of this platform we are spawning
        public float weight;
    }

    [Serializable]
    public class BrokenPlatform
    {
        // Type of the platforms
        public PlatformType type = PlatformType.Broken;
        // How high the player can jump
        public float jumpHeight;
        // Min dis of two spawning platform
        public float minHeight;
        // Max dis of two spawning platform
        public float maxHeight;
        // How many percentage of this platform we are spawning
        public float weight;
    }

    [Serializable]
    public class OncePlatform
    {
        // Type of the platforms
        public PlatformType type = PlatformType.Once;
        // How high the player can jump
        public float jumpHeight;
        // Min dis of two spawning platform
        public float minHeight;
        // Max dis of two spawning platform
        public float maxHeight;
        // How many percentage of this platform we are spawning
        public float weight;
    }

    [Serializable]
    public class DoublePlatform
    {
        // Type of the platforms
        public PlatformType type = PlatformType.Doudle;
        // How high the player can jump
        public float jumpHeight;
        // Min dis of two spawning platform
        public float minHeight;
        // Max dis of two spawning platform
        public float maxHeight;
        // How many percentage of this platform we are spawning
        public float weight;
    }

    [Serializable]
    public class HorizontalPlatform
    {
        // Type of the platforms
        public PlatformType type = PlatformType.Horizontal;
        // How high the player can jump
        public float jumpHeight;
        // Min dis of two spawning platform
        public float minHeight;
        // Max dis of two spawning platform
        public float maxHeight;
        // How many percentage of this platform we are spawning
        public float weight;
        // How far the platform can go
        public float distance;
        // How fast the platform is going
        public float speed;
    }

    [Serializable]
    public class VerticalPlatform
    {
        // Type of the platforms
        public PlatformType type = PlatformType.Vertical;
        // How high the player can jump
        public float jumpHeight;
        // Min dis of two spawning platform
        public float minHeight;
        // Max dis of two spawning platform
        public float maxHeight;
        // How many percentage of this platform we are spawning
        public float weight;
        // How far the platform can go
        public float distance;
        // How fast the platform is going
        public float speed;
    }

    [Serializable]
    public class GroundPlatform
    {
        // Type of the platforms
        public PlatformType type = PlatformType.Ground;
        // How high the player can jump
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
