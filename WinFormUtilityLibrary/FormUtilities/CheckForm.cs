using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormUtilityLibrary.ClassUtilities;
namespace WinFormUtilityLibrary.FormUtilities
{
    /// <summary>
    /// Utility class to check form objects.
    /// </summary>
    public class CheckForm
    {
        private IEnumerable<Control> FormControls { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="FormToClear">The form on which the clear actions will be performed.</param>
        public CheckForm(Form FormToClear): this(FormToClear.Controls)
        {
            if (FormToClear == null)
                throw new ArgumentNullException();
        }
        /// <summary>
        /// 
        /// </summary>
        public CheckForm()
        {

        }

        /// <summary>
        /// Second Constructor
        /// </summary>
        /// <param name="Controls">The controls on which the clear actions will be performed.</param>
        public CheckForm(Control.ControlCollection Controls)
        {
            if (Controls == null)
                throw new ArgumentNullException();
            this.FormControls = Helpers.GetControls(Controls);

        }
        /// <summary>
        /// Check's all the form's textboxes
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results CheckTextboxes()
        {
            try
            {
                var lst = FormControls.Where((x) => x.GetType() == typeof(TextBox) || x.GetType() == typeof(RichTextBox));
                lst.Aggregate((e1, e2) =>
                {
                    if (string.IsNullOrEmpty(e1.Text)) throw new Exception("000");

                    return e2;
                });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                if (ex.Message == "000")
                {
                    return Results.FAILURE;
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Check's all the form's comboboxes
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results CheckComboBoxes()
        {
            try
            {
                var lst = FormControls.Where((x) => x.GetType() == typeof(ComboBox));
               // if (lst.Count() == 0) return Results.FAILURE;

                foreach (ComboBox item in lst)
                {
                    if (item.GetType() == typeof(ComboBox) && (item.SelectedIndex.ToString() == null || item.SelectedIndex == -1))
                    {
                        return Results.FAILURE;
                    }
                }
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                return Results.FAILURE;
                throw ex;
            }
        }
        /// <summary>
        /// Check's all the form's checkboxes
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results CheckCheckBoxes()
        {
            try
            {
                var lst = FormControls.Where((x) => x.GetType() == typeof(CheckBox));
                lst.Aggregate((e1, e2) =>
                {
                    var c = e1 as CheckBox;
                    if (!c.Checked) throw new Exception("000");

                    c = e2 as CheckBox;
                    if (!c.Checked) throw new Exception("000");

                    return e2;
                });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                if (ex.Message == "000")
                {
                    return Results.FAILURE;
                }
                else
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Check's the form for all type of control
        /// </summary>
        /// <param name="WithCheckControls">Whether or not if checkable controls suck as checkboxes should be considered</param>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results CheckAll(bool WithCheckControls = false)
        {
            if (WithCheckControls)
                return (CheckTextboxes() == Results.SUCCES && CheckComboBoxes() == Results.SUCCES && CheckCheckBoxes() == Results.SUCCES) ? Results.SUCCES : Results.FAILURE;
            else
                return (CheckTextboxes() == Results.SUCCES && CheckComboBoxes() == Results.SUCCES) ? Results.SUCCES : Results.FAILURE;
        }
        /// <summary>
        /// Permet de charger des combobox 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="liste">Liste d'élements a chargé</param>
        /// <param name="combo">Le control combobox a chargé</param>
        /// <param name="displayMember">Le champ display member</param>
        /// <param name="valueMember">Le champ value member</param>
        public void chargerCombo<T>(List<T> liste, ComboBox combo, string displayMember, string valueMember)
        {
            try
            {
                combo.DataSource = liste;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;
                if (liste.Count > 0)
                {
                    
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
