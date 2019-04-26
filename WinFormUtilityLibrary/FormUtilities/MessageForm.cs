using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormUtilityLibrary.FormUtilities
{
    /// <summary>
    /// Creates message boxes
    /// </summary>
    public sealed class MessageForm
    {
        private static string Header = Application.ProductName;
        /// <summary>
        /// Shows a prompt dialog
        /// </summary>
        /// <param name="Message">The text to show</param>
        /// <returns></returns>
        public static string ShowPrompt(string Message)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = Header,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };
            Label textLabel = new Label() { Width=400, Left = 50, Top = 20, Text = Message };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        /// <summary>
        /// Shows error message
        /// </summary>
        /// <param name="Message">Message to show</param>
        /// <returns>Dialogresult depending on users answer to message box</returns>
        public static DialogResult ShowError(string Message)
        {
            return MessageBox.Show(CutDownMessage(Message), Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows warning message
        /// </summary>
        /// <param name="Message">Message to show</param>
        /// <returns>Dialogresult depending on users answer to message box</returns>
        public static DialogResult ShowWarning2(string Message)
        {
            return MessageBox.Show(CutDownMessage(Message), Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows warning message
        /// </summary>
        /// <param name="Message">Message to show</param>
        /// <returns>Dialogresult depending on users answer to message box</returns>
        public static DialogResult ShowWarning(string Message)
        {
            return MessageBox.Show(CutDownMessage(Message), Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows info message
        /// </summary>
        /// <param name="Message">Message to show</param>
        /// <returns>Dialogresult depending on users answer to message box</returns>
        public static DialogResult ShowInfo(string Message)
        {
            return MessageBox.Show(CutDownMessage(Message), Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows question message
        /// </summary>
        /// <param name="Message">Message to show</param>
        /// <returns>Dialogresult depending on users answer to message box</returns>
        public static DialogResult ShowQuestion(string Message)
        {
            return MessageBox.Show(CutDownMessage(Message), Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Shows exception message
        /// </summary>
        /// <param name="exception">Message to show</param>
        /// <returns>Dialogresult depending on users answer to message box</returns>
        public static void ShowException(Exception exception)
        {
            var exc = exception;
            while (exc.InnerException != null)
            {
                exc = exc.InnerException;
            }
            ShowError(exc.Message);
        }


        /// <summary>
        /// Cuts down message for neat display of message in message box
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        protected static string CutDownMessage(string Message)
        {
            string cutDown = Message, output = "";
            while (cutDown.Length > 0)
            {
                var i = 80;
                cutDown = cutDown.Trim();
                int re = cutDown.IndexOf('\n');
                if (i > re && re > -1)
                {
                    output += cutDown.Substring(0, re + 1);
                    cutDown = cutDown.Substring(re + 1);
                }
                else
                {
                    if (cutDown.Length > i)
                    {
                        while (cutDown.Length > i && cutDown[i] != ' ')
                        {
                            i++;
                        }
                        output += cutDown.Substring(0, i) + "\n";
                        cutDown = cutDown.Substring(i, cutDown.Length - i);
                    }
                    else
                    {
                        output += cutDown;
                        break;
                    }
                }

            }
            return output;
        }
    }
}
