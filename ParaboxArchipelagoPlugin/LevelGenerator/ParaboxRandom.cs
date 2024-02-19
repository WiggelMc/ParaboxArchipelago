using System;
using System.Text;

namespace ParaboxArchipelago.LevelGenerator
{
    public class ParaboxRandom
    {
        private uint x;
        private uint y;
        private uint z;
        private uint w;

        private class Initializer
        {
            private uint state;

            public Initializer(uint seed)
            {
                state = seed;
            }

            public uint Next()
            {
                var result = state += 0x9971f3DC;
                result = (result ^ (result >> 14)) * 0xD7185219;
                result = (result ^ (result >> 10)) * 0xCF4bED29;
                return result ^ (result >> 15);
            }
        }
        
        public ParaboxRandom(uint seed)
        {
            var i = new Initializer(seed);
            x = i.Next();
            y = i.Next();
            z = i.Next();
            w = i.Next();
        }

        public uint Next(uint start = 0, uint end = uint.MaxValue)
        {
            var t = x ^ (x << 13);
            x = y;
            y = z;
            z = w;
            var result = w = (w ^ (w >> 7)) ^ (t ^ (t >> 6));

            end = Math.Max(start, end);
            if (start == 0 && end == uint.MaxValue)
                return result;
            return start + result % (end + 1 - start);
        }
        
        public static void TestRandom()
        {
            var r = new ParaboxRandom(0xFFFFFAEE);
            var x = new StringBuilder();
            for (var i = 0; i < 10000; i++)
            {
                var v = r.Next();
                x.Append(Convert.ToString(v, 2).PadLeft(32, '0'));
            }
            ParaboxArchipelagoPlugin.Log.LogInfo(x.ToString());
        }
    }
}