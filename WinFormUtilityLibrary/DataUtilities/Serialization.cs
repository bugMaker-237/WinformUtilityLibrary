using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace WinFormUtilityLibrary.DataUtilities
{
    /// <summary>
    /// Class used to serialise data
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Path to the serialised file or to which it has to be serialised
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Object to serialised or object deserialised
        /// </summary>
        public object Obj { get; set; }
        /// <summary>
        /// File name of serialised or deserialised object
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// First Constructor
        /// </summary>
        /// <param name="obj">Object to serialise</param>
        /// <param name="fileName">File name of the serialised object</param>
	    public Serialization(object obj, string fileName)
        {
            if (obj == null)
            {
                throw new Exception("l'instance est nulle");
            }

            this.Obj = obj;
            this.FileName = fileName;
            this.Path = CreateOrOpenDirectory();
            //créer un nouveau chemin dans le cas on passe un chemin vide en paramètre
        }
        /// <summary>
        /// Second Constructor
        /// </summary>
        /// <param name="path">Path to which object will be serialised</param>
        /// <param name="obj">Object to serialise</param>
        public Serialization(string path, object obj)
        {
            if (obj == null)
            {
                throw new Exception("l'instance est nulle");
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception(" chemin ne doit pas etre nul");
            }
            this.Obj = obj;
            this.Path = path;
        }
        /// <summary>
        /// Serialise object to binary format
        /// </summary>
        public void BinarySerialization()
        {
            var file = new System.IO.FileStream(this.Path, System.IO.FileMode.Create);
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(file, this.Obj);
            file.Close();
        }
        /// <summary>
        /// Serialise object to xml format
        /// </summary>
        public void XmlSerialization()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(this.Path);
            var formatter = new XmlSerializer(this.Obj.GetType());
            formatter.Serialize(file, this.Obj);
            file.Close();
        }
        /// <summary>
        /// Deserialise binary object
        /// </summary>
        /// <returns>The deserialised object</returns>
        public object BinaryDeserialization()
        {
            if (System.IO.File.Exists(this.Path))
            {
                var file = new System.IO.FileStream(this.Path, System.IO.FileMode.Open);
                if (file.Length > 0)
                {
                    IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    this.Obj = (object)formatter.Deserialize(file);
                }
                file.Close();
            }
            else
            {
                //Throw New Exception("le fichier n'existe pas")
                return null;
            }
            return this.Obj;
        }
        /// <summary>
        /// Deserialise binary object
        /// </summary>
        /// <param name="fileStream">The stream to deserialise</param>
        /// <returns>The deserialised object</returns>
        public object BinaryDeserialization(Stream fileStream)
        {
            if (fileStream.CanRead)
            {
                if (fileStream.Length > 0)
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    this.Obj = (object)formatter.Deserialize(fileStream);
                }
                fileStream.Close();
            }
            else
            {
                //Throw New Exception("le fichier n'existe pas")
                return null;
            }
            return this.Obj;
        }
        /// <summary>
        /// Deserialise xml object
        /// </summary>
        /// <returns>The deserialised object</returns>
        public object XmlDeserialization()
        {
            if (System.IO.File.Exists(this.Path))
            {
                var file = new System.IO.StreamReader(this.Path);
                if (file.BaseStream.Length > 0)
                {
                    var formatter = new XmlSerializer(this.Obj.GetType());
                    this.Obj = (object)formatter.Deserialize(file);
                }
                file.Close();
            }
            else
            {
                //Throw New Exception("le fichier n'existe pas")
                return null;
            }
            return this.Obj;
        }
        /// <summary>
        /// Deserialise xml object
        /// </summary>
        /// <param name="fileStream">The stream to deserialise</param>
        /// <returns>The deserialised object</returns>
        public object XmlDeserialization(Stream fileStream)
        {

            if (fileStream.CanRead)
            {
                if (fileStream.Length > 0)
                {
                    var formatter = new XmlSerializer(this.Obj.GetType());
                    this.Obj = (object)formatter.Deserialize(fileStream);
                }
                fileStream.Close();
            }
            else
            {
                //Throw New Exception("le fichier n'existe pas")
                return null;
            }
            return this.Obj;
        }
        /// <summary>
        /// Create or opens the directory where the file willl be sérialised
        /// </summary>
        /// <returns>Path to created file</returns>
        private string CreateOrOpenDirectory()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\Data\\" + FileName))
            {
                var f = File.Create((Directory.GetCurrentDirectory() + "\\Data\\" + FileName));
                f.Close();
            }
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data\\" + FileName);
        }
    }
}
