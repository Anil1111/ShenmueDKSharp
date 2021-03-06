﻿using Pfim;
using ShenmueDKSharp.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShenmueDKSharp.Files.Images
{
    public class DDS : BaseImage
    {
        public static bool EnableBuffering = true;
        public override bool BufferingEnabled => EnableBuffering;

        public readonly static List<string> Extensions = new List<string>()
        {
            "DDS"
        };

        public readonly static List<byte[]> Identifiers = new List<byte[]>()
        {
            new byte[4] { 0x44, 0x44, 0x53, 0x20 }, //DDS 
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

        public override int DataSize => Size;

        public int Size { get; set; }

        private byte[] m_buffer;

        protected override void _Read(BinaryReader reader)
        {
            long baseOffset = reader.BaseStream.Position;

            Dds image = Dds.Create(reader.BaseStream, new PfimConfig());

            Size = image.Data.Length;

            Width = image.Width;
            Height = image.Height;

            byte[] pixels = image.Data;
            Pixels = new Color4[image.Width * image.Height];
            for (int i = 0; i < image.Width * image.Height; i++)
            {
                int index = i * image.BytesPerPixel;
                if (image.BytesPerPixel > 3)
                {
                    if (pixels[index + 3] < 0.8)
                    {
                        HasTransparency = true;
                    }
                    Pixels[i] = new Color4(pixels[index + 2], pixels[index + 1], pixels[index], pixels[index + 3]);
                }
                else
                {
                    HasTransparency = false;
                    Pixels[i] = new Color4(pixels[index + 2], pixels[index + 1], pixels[index], 255);
                }
            }

            if (EnableBuffering)
            {
                reader.BaseStream.Seek(baseOffset, SeekOrigin.Begin);
                m_buffer = reader.ReadBytes(image.Data.Length);
            }
        }

        protected override void _Write(BinaryWriter writer)
        {
            writer.Write(m_buffer);
        }
    }
}
