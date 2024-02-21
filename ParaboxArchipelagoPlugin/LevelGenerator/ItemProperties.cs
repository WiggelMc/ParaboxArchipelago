using JetBrains.Annotations;

namespace ParaboxArchipelago.LevelGenerator
{
    public abstract class ItemProperties
    {
        public const long ITEM_ID_OFFSET = 17771000;
        
        [CanBeNull] public string Name;
        public long IDOffset;
        public long ID;
        public ItemType Type = ItemType.Progression;

        protected ItemProperties(long id)
        {
            IDOffset = id;
            ID = ITEM_ID_OFFSET + id;
        }

        public class Single : ItemProperties
        {
            public Single(long id) : base(id) {}
        }
        
        public class Progressive : ItemProperties
        {
            public int Levels;
            
            public Progressive(long id, int levels) : base(id)
            {
                Levels = levels;
            }
        }
        
        public class Seperate : ItemProperties
        {
            public int Level;

            public Seperate(long id, int level) : base(id)
            {
                Level = level;
            }
        }
    }
}