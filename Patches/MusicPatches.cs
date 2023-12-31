using System.Collections.Generic;

namespace ParaboxArchipelago.Patches
{
    public static class MusicPatches
    {
        public static class FMODSquare_AreaNameToFMODIndex
        {
            public static void Patch()
            {
                var patchDict = new Dictionary<string, int>()
                {
                    {
                        "Area_Introst",
                        0
                    }
                };
                
                foreach (var entry in patchDict)
                {
                    FMODSquare.AreaNameToFMODIndex[entry.Key] = entry.Value;
                }
            }
        }
    }
}