using System;
using System.Runtime.Serialization;

namespace Z80.Kernel.Z80Assembler
{
    [Serializable]
    public class Z80AssemblerException : Exception
    {
        public Z80AssemblerException()
        {
        }

        public Z80AssemblerException(string message, int lineIndex = 0) : base(message)
        {
            LineIndex = lineIndex;
        }
        public int LineIndex { get; }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue("Line index", LineIndex);

            base.GetObjectData(info, context);
        }

    }
}
