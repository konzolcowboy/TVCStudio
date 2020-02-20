using System;
using System.Runtime.Serialization;

namespace TVC.Basic
{
    [Serializable]
    public class TvcBasicException : Exception
    {
        public TvcBasicException(string message) : base(message)
        {
            
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }

    }
}
