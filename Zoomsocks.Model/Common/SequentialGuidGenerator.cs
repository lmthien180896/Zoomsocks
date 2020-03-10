﻿using System;
using System.Security.Cryptography;

namespace SingLife.ULTracker.Model.Common
{
    public enum SequentialGuidType
    {
        /// <summary>
        /// Generated GUIDs are optimized for using with MySQL or PostgreSQL.
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// Generated GUIDs are optimized for using with Oracle.
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// Generated GUIDs are optimized for using with Microsoft SQL Server.
        /// </summary>
        SequentialAtEnd
    }

    /// <summary>
    /// A GUID generator that generates sequential GUIDs.
    /// This is the implementation of Jeremy Todd, refer to this article for more details
    /// http://www.codeproject.com/Articles/388157/GUIDs-as-fast-primary-keys-under-multiple-database.
    /// </summary>
    public static class SequentialGuidGenerator
    {
        private static readonly RNGCryptoServiceProvider CryptoServiceProvider = new RNGCryptoServiceProvider();
        private static SequentialGuidType defaultSequentialGuidType = SequentialGuidType.SequentialAsString;

        /// <summary>
        /// Gets the default sequential GUID generation type.
        /// </summary>
        public static SequentialGuidType DefaultSequentialGuidType
        {
            get { return defaultSequentialGuidType; }
        }

        /// <summary>
        /// Sets the default sequential GUID generation type.
        /// This method should be called at the application initialization.
        /// </summary>
        /// <param name="value">A sequential GUID generation type.</param>
        public static void SetDefaultSequentialGuidType(SequentialGuidType value)
        {
            defaultSequentialGuidType = value;
        }

        public static Guid NewSequentialGuid(SequentialGuidType sequentialGuidType)
        {
            var randomBytes = new byte[10];
            CryptoServiceProvider.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];

            switch (sequentialGuidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (sequentialGuidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }

        /// <summary>
        /// Generates a new sequential GUID using the default GUID generation type.
        /// </summary>
        /// <returns>A new <see cref="Guid"/> value.</returns>
        public static Guid NewSequentialGuid()
        {
            return NewSequentialGuid(DefaultSequentialGuidType);
        }
    }
}