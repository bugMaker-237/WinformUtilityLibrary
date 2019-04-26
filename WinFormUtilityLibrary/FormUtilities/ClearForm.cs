using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormUtilityLibrary.ClassUtilities;

namespace WinFormUtilityLibrary.FormUtilities
{
    /// <summary>
    /// Utility class to clear form objects.
    /// </summary>
    public class ClearForm
    {

        private IEnumerable<Control> FormControls { get; set; }

        /// <summary>
        /// Unique Constructor
        /// </summary>
        /// <param name="FormToClear">The form on which the clear actions will be performed.</param>
        public ClearForm(Form FormToClear) : this(FormToClear.Controls)
        {
            if (FormToClear == null)
                throw new ArgumentNullException();
        }
        /// <summary>
        /// Second Constructor
        /// </summary>
        /// <param name="Controls">The controls on which the clear actions will be performed.</param>
        public ClearForm(Control.ControlCollection Controls)
        {
            if (Controls == null)
                throw new ArgumentNullException();
            this.FormControls = Helpers.GetControls(Controls);
        }

        

        /// <summary>
        /// Clear all the form's textboxes
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results ClearTextboxes()
        {
            try
            {
                var lst = FormControls.Where((x) => x.GetType() == typeof(TextBox) || x.GetType() == typeof(RichTextBox));
                lst.Aggregate((e1, e2) => { e1.ResetText();  e2.ResetText(); return e2; });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                return Results.FAILURE;
                throw ex;
            }
        }
        /// <summary>
        /// Clear all the form's NumericUpDown
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results ClearNumericUpDown()
        {
            try
            {
                var lst = FormControls.Where((x) => x.GetType() == typeof(NumericUpDown));
                lst.Aggregate((e1, e2) => { e1.ResetText(); e2.ResetText(); return e2; });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                return Results.FAILURE;
                throw ex;
            }
        }
        /// <summary>
        /// Clear all the form's pictures
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results ClearPictures()
        {
            try
            {
                List<Control> lst = FormControls.Where((x) => x.GetType() == typeof(PictureBox)).ToList();
                lst.Aggregate((e1, e2) => {

                    PictureBox pic = (PictureBox)e1;
                    pic.Image = null;
                    pic.ImageLocation = null;

                    pic = (PictureBox)e2;
                    pic.Image = null;
                    pic.ImageLocation = null;

                    pic.Dispose();
                    return e2;

                });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                return Results.FAILURE;
                throw ex;
            }
        }

        /// <summary>
        /// Clear all the form's checkboxes
        /// </summary>
        /// <returns>Results.SUCCES if not exceptions and Results.FAILURE if exception</returns>
        public Results ClearCheckBoxes()
        {
            try
            {
                List<Control> lst = FormControls.Where((x) => x.GetType() == typeof(CheckBox)).ToList();
                lst.Aggregate((e1, e2) => {

                    CheckBox chk = (CheckBox)e1;
                    chk.Checked = false;
                    chk.CheckState = CheckState.Unchecked;

                    chk = (CheckBox)e2;
                    chk.Checked = false;
                    chk.CheckState = CheckState.Unchecked;

                    chk.Dispose();
                    return e2;

                });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                return Results.FAILURE;
                throw ex;
            }
        }

        /// <summary>
        /// Clears all the form's radiobuttons
        /// </summary>
        /// <returns></returns>
        public Results ClearRadioButtons()
        {
            try
            {
                List<Control> lst = FormControls.Where((x) => x.GetType() == typeof(RadioButton)).ToList();
                lst.Aggregate((e1, e2) => {

                    RadioButton chk = (RadioButton)e1;
                    chk.Checked = false;

                    chk = (RadioButton)e2;
                    chk.Checked = false;

                    chk.Dispose();
                    return e2;

                });
                return Results.SUCCES;
            }
            catch (Exception ex)
            {
                return Results.FAILURE;
                throw ex;
            }
        }

    }
}
