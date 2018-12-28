﻿using ShenmueDKSharp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShenmueDKSharp.Files.Models._MT7
{
    /// <summary>
    /// MDOX/MDO7 Node
    /// </summary>
    public class MDOX
    {
        public readonly static List<byte[]> Identifiers = new List<byte[]>()
        {
            new byte[4] { 0x4D, 0x44, 0x4F, 0x58 }, //MDOX
            new byte[4] { 0x4D, 0x44, 0x4F, 0x37 }  //MDO7
        };

        public static bool IsValid(uint identifier)
        {
            return IsValid(BitConverter.GetBytes(identifier));
        }

        public static bool IsValid(byte[] identifier)
        {
            for (int i = 0; i < Identifiers.Count; i++)
            {
                if (FileHelper.CompareSignature(Identifiers[i], identifier)) return true;
            }
            return false;
        }
    }
}
