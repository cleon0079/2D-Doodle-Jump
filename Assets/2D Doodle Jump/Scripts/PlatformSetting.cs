using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class PlatformSetting
{
    [Serializable]
    public class NormalPlatform
    {
        public int index = 0;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class BrokenPlatform
    {
        public int index = 1;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class OncePlatform
    {
        public int index = 2;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class DoublePlatform
    {
        public int index = 3;
        public float jumpHeight;
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class HorizontalPlatform
    {
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
