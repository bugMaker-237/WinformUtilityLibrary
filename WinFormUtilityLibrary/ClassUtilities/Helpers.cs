using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace WinFormUtilityLibrary.ClassUtilities
{
    /// <summary>
    /// Application properties and descriptions
    /// </summary>
    public static class Helpers
    {
        #region Assembly Attribute Accessors
        /// <summary>
        /// Gets assembly's title
        /// </summary>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets assembly's version
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets assembly's description
        /// </summary>
        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets assembly's products
        /// </summary>
        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets assembly's copyright
        /// </summary>
        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets assembly's's Company
        /// </summary>
        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public static void DisableForm(object controls, bool v)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// Test if string is numeric
        /// </summary>
        /// <param name="input">String to test</param>
        /// <returns>Boolean true if is numeric</returns>
        public static bool IsNumeric(string input)
        {
            int test; double dbl; float flt;
            return int.TryParse(input, out test) || double.TryParse(input, out dbl) || float.TryParse(input, out  flt);
        }
        /// <summary>
        /// Generates sequential code on characters ranging from 000 - ZZZ
        /// </summary>
        /// <param name="LastCodeGenerated">The code from which the sequence will start</param>
        /// <param name="format">The number of characters on which the code shold be generated</param>
        /// <returns>A generated code string</returns>
        /// <example>var code = Helpers.CodeGenerator("045", "000");</example>
        public static string CodeGenerator(string LastCodeGenerated, string format)
        {
            Match m = Regex.Match(LastCodeGenerated, @"(^([a-zA-Z])+\d+([a-zA-Z]))");
            if (m.Success)
            {
                throw new ArgumentException("Unusual LastGeneratedCode. This code was not generated using this algorithm and cannot be handled.");
            }
            int first;
            char start;
            int most = Convert.ToInt32(new String('9', format.Length));
            LastCodeGenerated = LastCodeGenerated != null ? LastCodeGenerated : "";
            if (LastCodeGenerated.Length != format.Length) throw new ArgumentException("Format must have same length as LastGeneratedCode");
            if (int.TryParse(LastCodeGenerated, out first) && first != most)
            {
                return (first + 1).ToString(format);
            }
            else if (first == most)
            {
                return "A" + new String('0', format.Length - 1);
            }
            else if (LastCodeGenerated == "")
            {
                return format;
            }
            else if (LastCodeGenerated.Length > 1 && int.TryParse(LastCodeGenerated.Substring(1, LastCodeGenerated.Length - 1), out first))
            {
                char c = Convert.ToChar(LastCodeGenerated.Substring(0, 1));
                if (first == Convert.ToInt32(new String('9', format.Length - 1)))
                {
                    start = (char)(c + 1);
                    return start + new String('0', format.Length - 1);
                }
                else
                {
                    var ch = c.ToString() + ((first + 1).ToString());
                    return ch.ToString();
                }
            }
            else
            {
                //else
                //{
                //    if(LastCodeGenerated == new String('Z', format.Length))
                //    {
                //        throw new StackOverflowException("Maximum capacity of string format reached");
                //    }
                //    int i = 2;
                //    do
                //    {
                //        i++;
                //    }
                //    while (i < LastCodeGenerated.Length && !int.TryParse(LastCodeGenerated.Substring(i, LastCodeGenerated.Length - 1), out first));

                //    if (int.TryParse(LastCodeGenerated.Substring(i, LastCodeGenerated.Length - 1), out first))
                //    {
                //        string left = Convert.ToString(LastCodeGenerated.Substring(0, i - 1));
                //        char c = left.Last();
                //        if (first == Convert.ToInt32(new String('9', format.Length - i)))
                //        {
                //            if(c == Convert.ToChar(90))
                //            {
                //                //c = left
                //            }
                //            else
                //            {
                //                start = (char)(c + 1);
                //                left.Insert(left.Length, start.ToString());
                //                return left + start + new String('0', format.Length - i);
                //            }
                //        }
                //        else
                //        {
                //            var ch = c.ToString() + ((first + 1).ToString());
                //            return ch.ToString();
                //        }
                //    }
                //}
            }
            return "";
        }

        /// <summary>
        /// Gets the system MACID
        /// </summary>
        /// <returns></returns>
        public static string GetSystemMACID(string ComputerName = "localhost")
        {
            try
            {
                ManagementScope Scope;
                Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);
                Scope.Connect();
                ObjectQuery Query = new ObjectQuery("SELECT UUID, Name FROM Win32_ComputerSystemProduct");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    return string.Format("{1}@{0}", WmiObject["Name"], WmiObject["UUID"]);// String                     
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Gets a list of all the controls and the recursive children in a form
        /// </summary>
        /// <param name="Ctrls">A control collection from which to extract all the controls</param>
        /// <returns>A list of controls<returns>
        public static IEnumerable<Control> GetControls(Control.ControlCollection Ctrls)
        {
            foreach (Control item in Ctrls)
            {
                if (item.HasChildren)
                {
                    foreach (Control child in GetControls(item.Controls))
                        yield return child;
                }
                yield return item;
            }
        }
        /// <summary>
        /// Casts an object owner to another type which has the same or some similar properties
        /// </summary>
        /// <param name="owner">The object to cast</param>
        /// <returns></returns>
        public static T Cast<T>(this object owner)
        {
            Type objectType = owner.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = z.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = owner.GetType().GetProperty(memberInfo.Name).GetValue(owner, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;

        }

        /// <summary>
        /// Permet de desactiver ou activer plusieurs controls
        /// </summary>
        /// <param name="controls">Collection decontrol</param>
        /// <param name="val">valeur true ou false</param>
        public static void DisableForm(Control.ControlCollection controls, Boolean val)
        {
            var ctrls = GetControls(controls);
            foreach (var item in ctrls)
            {
                if (item.GetType() == typeof(ComboBox) || item.GetType() == typeof(TextBox) || item.GetType() == typeof(CheckBox) || item.GetType() == typeof(DateTimePicker))
                {
                    item.Enabled = val;
                }
            }
        }
        /// <summary>
        /// Permet de desactiver ou activer plusieurs controls
        /// </summary>
        /// <param name="controls">Collection decontrol</param>
        /// <param name="val">valeur true ou false</param>
        public static void DisableForm(IEnumerable<Control> controls, Boolean val)
        {
            foreach (var item in controls)
            {
                if (item.GetType() == typeof(ComboBox) || item.GetType() == typeof(TextBox) || item.GetType() == typeof(CheckBox) || item.GetType() == typeof(DateTimePicker))
                {
                    item.Enabled = val;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrAddress"></param>
        /// <returns></returns>

        public static ConnectionStatus PingHost(string nameOrAddress)
        {
            ConnectionStatus pingable = ConnectionStatus.SYNC;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = GetConnectionStatus(reply.Status);
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipstatus"></param>
        /// <returns></returns>
        public static ConnectionStatus GetConnectionStatus(IPStatus ipstatus)
        {
            switch (ipstatus)
            {
                case IPStatus.Success:
                    return ConnectionStatus.GOOD;
                case IPStatus.DestinationNetworkUnreachable:
                    break;
                case IPStatus.DestinationHostUnreachable:
                    break;
                case IPStatus.DestinationProtocolUnreachable:
                    break;
                case IPStatus.DestinationPortUnreachable:
                    break;
                case IPStatus.NoResources:
                    break;
                case IPStatus.BadOption:
                    break;
                case IPStatus.HardwareError:
                    break;
                case IPStatus.PacketTooBig:
                    break;
                case IPStatus.TimedOut:
                    return ConnectionStatus.SYNC;
                case IPStatus.BadRoute:
                    break;
                case IPStatus.TtlExpired:
                    return ConnectionStatus.SYNC;
                case IPStatus.TtlReassemblyTimeExceeded:
                    return ConnectionStatus.SYNC;
                case IPStatus.ParameterProblem:
                    break;
                case IPStatus.SourceQuench:
                    break;
                case IPStatus.BadDestination:
                    break;
                case IPStatus.DestinationUnreachable:
                    break;
                case IPStatus.TimeExceeded:
                    break;
                case IPStatus.BadHeader:
                    break;
                case IPStatus.UnrecognizedNextHeader:
                    break;
                case IPStatus.IcmpError:
                    break;
                case IPStatus.DestinationScopeMismatch:
                    break;
                case IPStatus.Unknown:
                    break;
                default:
                    break;
            }
            return ConnectionStatus.BAD;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        public static int GetDateDifference(this DateTime ToDate, DateTime? fromDate = null)
        {
            if(fromDate == null || fromDate.HasValue)
                return Math.Abs((ToDate - Convert.ToDateTime("01/01/" + DateTime.Today.Year)).Days);
            else
                return Math.Abs((ToDate - Convert.ToDateTime("01/01/" + fromDate.Value.Year)).Days);
        }
        
    }
    /// <summary>
    /// Result types
    /// </summary>
    public enum Results
    {
        /// <summary>
        /// Positive result
        /// </summary>
        SUCCES = 1,
        /// <summary>
        /// Negative result
        /// </summary>
        FAILURE = 0
    }
   /// <summary>
   /// 
   /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// 
        /// </summary>
        GOOD = 0,
        /// <summary>
        /// 
        /// </summary>
        BAD = 1,
        /// <summary>
        /// 
        /// </summary>
        SYNC = -1
    }
    /// <summary>
    /// Language to be used
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// French language
        /// </summary>
        FRENCH = 0,
        /// <summary>
        /// English language
        /// </summary>
        ENGLISH = 1
    }
}
