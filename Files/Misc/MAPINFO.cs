﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShenmueDKSharp.Files.Misc
{
    /// <summary>
    /// Map information file containing various tokens.
    /// </summary>
    /// <seealso cref="ShenmueDKSharp.Files.BaseFile" />
    public class MAPINFO : BaseFile
    {
        public static bool EnableBuffering = false;
        public override bool BufferingEnabled => EnableBuffering;

        protected override void _Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void _Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
