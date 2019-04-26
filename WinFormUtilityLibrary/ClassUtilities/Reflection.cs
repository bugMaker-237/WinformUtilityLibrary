using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WinFormUtilityLibrary.ClassUtilities
{
    public static class Reflection
    {
        public static IEnumerable<Type> GetClasses(string assemblyName, string nameSpace)
        {
            Assembly asm = Assembly.Load(assemblyName);
            return asm.GetTypes()
                .Where(type => type.Namespace == nameSpace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atype"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<object, string>> GetPropsAndValues(this object atype)
        {
            if (atype == null) return new Dictionary<string, Dictionary<object, string>>();
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            Dictionary<string, Dictionary<object, string>> dict = new Dictionary<string, Dictionary<object, string>>();
            foreach (PropertyInfo prp in props)
            {
                object value = prp.GetValue(atype, new object[] { });
                string type = value.GetType().ToString();
                if (value == null) continue;
                dict.Add(prp.Name, new Dictionary<object, string>() { { value, type } });
            }
            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atype"></param>
        /// <returns></returns>
        //public static List<string> GetPropertiesList(this Type t)
        //{
        //    t.pro
        //    if (atype == null) return new Dictionary<string, object>();
        //    Type t = atype.GetType();
        //    PropertyInfo[] props = t.GetProperties();
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    foreach (PropertyInfo prp in props)
        //    {
        //        object value = prp.GetValue(atype, new object[] { });
        //        if (value == null) continue;
        //        dict.Add(prp.Name, value);
        //    }
        //    return dict;
        //}
    }
}
