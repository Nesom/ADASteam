using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject
{
    public static class ByteArrayExtentions
    {
        public static byte[] AddValue(this byte[] values, int id)
        {
            return values.Concat(ParseIntToBytes(id)).ToArray();
        }

        public static void RemoveValue(this byte[] values, int id)
        {
            for (int i = 0; i < values.Length - 3; i += 4)
                if (values[i] * 256 + values[i + 1] == id)
                {
                    values[i] = 0;
                    values[i + 1] = 0;
                    values[i + 2] = 0;
                    values[i + 3] = 0;
                    return;
                }
        }

        public static void ChangeCount(this byte[] values, int id, byte count)
        {
            for (int i = 0; i < values.Length - 3; i += 4)
                if (values[i] * 256 + values[i + 1] == id)
                {
                    values[i + 2] = (byte)(count / 256);
                    values[i + 3] = (byte)(count % 256);
                }
        }

        public static Dictionary<int, int> GetValues(this byte[] values)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < values.Length - 3; i += 4)
                if (values[i] * 256 + values[i + 1] != 0)
                    dict[values[i] * 256 + values[i + 1]] = values[i + 2] * 256 + values[i + 3];
            return dict;
        }

        public static byte[] ParseIntToBytes(this int id)
        {
            if (id < 0)
                throw new System.ArgumentException();
            return new byte[4] { (byte)(id / 256), (byte)(id % 256), 0, 1 };
        }
    }
}
