using System;

namespace QuakeDemoFun
{
    [Flags]
    public enum EntityEffects: byte
    {
        BrightField = 1,
        MuzzleFlash = 2,
        BrightLight = 4,
        DimLight = 8,
    }   
}