using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParaboxArchipelago.LevelGenerator
{
    [Generate(since: "1.0.0")]
    [ItemDefinition]
    public class Possess
    {
        [ItemOption] 
        static readonly ItemOption.Seperate Option = new() {Default = ItemOption.SeperateOption.Progressive};
        [SingleItem]
        public static readonly ItemProperties.Single Single = new(id: 1);
        [ProgressiveItem]
        public static readonly ItemProperties.Progressive Progressive = new(id: 2, levels: 2);
        [SeperateItem]
        public static readonly ItemProperties.Seperate PossessBox = new(id: 3, level: 1);
        [SeperateItem]
        public static readonly ItemProperties.Seperate PossessWall = new(id: 4, level: 2);
    }
}