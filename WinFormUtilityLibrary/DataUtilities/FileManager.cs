using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace WinFormUtilityLibrary.DataUtilities
{  
    public class FileManager
    {
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        public static Stream GetStream(string FileName)
        {
            FileStream F = null;
            try
            {
                if (FileName != null && File.Exists(FileName))
                {
                    F = new FileStream(FileName, FileMode.Open);
                    F.Position = 0;
                    return F;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (F != null)
                {
                    F.Dispose();
                }
            }
            return null;
        }

        public static string GetMimeType(string FileName)
        {
            if (FileName != null && File.Exists(FileName))
            {
                string Mime = "application/unknown";
                string Ext = Path.GetExtension(FileName).ToLower();
                Microsoft.Win32.RegistryKey RegKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Ext);
                if (RegKey != null && RegKey.GetValue("Content Type") != null)
                {
                    Mime = RegKey.GetValue("Content Type").ToString();
                }
                return Mime;
            }
            return null;
        }

        public static string GetExtension(string FileName)
        {
            if (FileName != null && File.Exists(FileName))
            {
                return Path.GetExtension(FileName);
            }
            return null;
        }
        public static string GetMimeTypeWithExtension(string Extension)
        {

            if (!string.IsNullOrWhiteSpace(Extension))
            {
                if (!Extension.StartsWith("."))
                {
                    Extension = "." + Extension;
                }

                string result = null;

                if (ExtensionToMimeType.TryGetValue(Extension, out result))
                {
                    return result;
                }

                Microsoft.Win32.RegistryKey regKey = default(Microsoft.Win32.RegistryKey);
                object value = null;

                regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Extension, false);
                value = regKey != null ? regKey.GetValue("Content Type", null) : null;
                result = value != null ? value.ToString() : string.Empty;

                ExtensionToMimeType[Extension] = result;
                return result;
            }
            return null;
        }
        private static System.Collections.Concurrent.ConcurrentDictionary<string, string> MimeTypeToExtension = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        private static System.Collections.Concurrent.ConcurrentDictionary<string, string> ExtensionToMimeType = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        public static string GetExtensionWithMimeType(string MimeType)
        {
            if (!string.IsNullOrWhiteSpace(MimeType))
            {
                string key = string.Format("MIME\\Database\\Content Type\\{0}", MimeType);
                string result = null;
                if (MimeTypeToExtension.TryGetValue(key, out result))
                {
                    return result;
                }

                Microsoft.Win32.RegistryKey regKey = default(Microsoft.Win32.RegistryKey);
                object value = null;

                regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(key, false);
                value = regKey != null ? regKey.GetValue("Extension", null) : null;
                result = value != null ? value.ToString() : string.Empty;

                MimeTypeToExtension[key] = result;
                return result;
            }
            return null;
        }

    }
    public class ImageFileManager : FileManager
    {
        public static byte[] GetBytes(string FileName)
        {
            try
            {
                if (FileName != null && File.Exists(FileName))
                {
                    return File.ReadAllBytes(FileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static byte[] GetBytes(string FileName, int Width, int Height = 0)
        {
            try
            {
                if (FileName != null && File.Exists(FileName))
                {
                    Image image = Image.FromFile(FileName);
                    return GetBytes(GetImage(FileName, Width, Height), image.RawFormat);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }


        public static byte[] GetBytes(Image Image, System.Drawing.Imaging.ImageFormat ImageFormat)
        {
            try
            {
                if (Image != null)
                {
                    MemoryStream MS = new MemoryStream();
                    Image.Save(MS, ImageFormat);
                    MS.Position = 0;
                    return MS.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Bitmap GetImage(string FileName, int Width = 0, int Height = 0)
        {
            try
            {
                if (FileName != null && File.Exists(FileName))
                {
                    dynamic Image = System.Drawing.Image.FromFile(FileName, true);
                    return RedimImage(Image, Width, Height);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Bitmap GetImage(string FileName,Size Size)
        {
            try
            {
                if (FileName != null && File.Exists(FileName))
                {
                    dynamic Image = System.Drawing.Image.FromFile(FileName, true);
                    return RedimImage(Image, Size);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Image GetImage(Stream Stream, int Width = 0, int Height = 0)
        {
            try
            {
                if (Stream != null)
                {
                    Stream.Position = 0;
                    dynamic Image = System.Drawing.Image.FromStream(Stream, true);
                    return RedimImage(Image, Width, Height);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Image GetImage(Stream Stream,Size Size)
        {
            try
            {
                if (Stream != null)
                {
                    Stream.Position = 0;
                    dynamic Image = System.Drawing.Image.FromStream(Stream, true);
                    return RedimImage(Image, Size);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Bitmap GetImage(byte[] Array, int Width = 0, int Height = 0)
        {
            try
            {
                if (Array != null)
                {
                    MemoryStream MS = new MemoryStream(Array);
                    MS.Position = 0;
                    dynamic Image = System.Drawing.Image.FromStream(MS, true);
                    return RedimImage(Image, Width, Height);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Bitmap GetImage(byte[] Array,Size Size)
        {
            try
            {
                if (Array != null)
                {
                    MemoryStream MS = new MemoryStream(Array);
                    dynamic Image = System.Drawing.Image.FromStream(MS, true);
                    return RedimImage(Image, Size);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public static Bitmap RedimImage(Image Image, int Width, int Height)
        {

            try
            {
                dynamic Bitmap = new Bitmap(Image);
                double PW = 0;
                if (Width != 0)
                {
                    PW = Image.Width / Width;
                }
                double PH = 0;
                if (Height != 0)
                {
                    PH = Image.Height / Height;
                }
                double P = PW > PH ? PW : PH;
                if (P > 1)
                {
                    Width = Convert.ToInt32(Image.Width / P);
                    Height = Convert.ToInt32(Image.Height / P);
                    Bitmap = new Bitmap(Image, Width, Height);
                    using (Graphics graphicsHandle =Graphics.FromImage(Bitmap))
                    {
                        var _with1 = graphicsHandle;
                        _with1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        _with1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        _with1.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        _with1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        _with1.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        _with1.DrawImage(Bitmap, 0, 0, Width, Height);
                    }
                }
                return Bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Bitmap RedimImage(Image Image,Size Size)
        {
            try
            {
                return RedimImage(Image, Size.Width, Size.Height);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static string BitMapToBase64(Bitmap bitmap)
        {
            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);
            return SigBase64;
        }

        public static Bitmap Base64ToBitmap(string base64)
        {
            Bitmap bmpReturn = null;


            byte[] byteBuffer = Convert.FromBase64String(base64);
            MemoryStream memoryStream = new MemoryStream(byteBuffer)
            {
                Position = 0
            };
            bmpReturn = (Bitmap)Image.FromStream(memoryStream);
            
            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;


            return bmpReturn;
        }

    }
}