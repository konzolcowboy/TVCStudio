namespace TVCTape
{
    internal class TvcCrcGenerator
    {
        public TvcCrcGenerator()
        {
            GeneratedCrcValue = 0;
        }

        public ushort GeneratedCrcValue { get; set; }

        // Generate CRC by Programing TVC book
        private void AddBit(bool bit)
        {
            byte a = bit ? (byte)0x80 : (byte)0;

            a = (byte)(a ^ (byte)(GeneratedCrcValue >> 8));
            byte cy = (byte)(a & 0x80);

            if (cy != 0)
            {
                GeneratedCrcValue ^= 0x0810;
                cy = 1;
            }

            GeneratedCrcValue += (ushort)(GeneratedCrcValue + cy);
        }

        // Adds buffer content to the CRC
        public void AddBlock(byte[] buffer)
        {
            foreach (byte b in buffer)
            {
                AddByte(b);

            }
        }

        public void AddByte(byte data)
        {
            for (int i = 0; i < 8; i++)
            {
                AddBit((data & 0x01) != 0);
                data >>= 1;
            }
        }
    }
}
