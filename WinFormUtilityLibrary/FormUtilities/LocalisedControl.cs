using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormUtilityLibrary.FormUtilities
{
    public class LocalizedControl : Control
    {
        /// <summary>
        /// Occurs when current UI culture is changed
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when current UI culture is changed")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Property Changed")]
        public event EventHandler CultureChanged;

        protected CultureInfo culture;
        protected ComponentResourceManager resManager;

        /// <summary>
        /// Current culture of this form
        /// </summary>
        [Browsable(false)]
        [Description("Current culture of this form")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CultureInfo Culture
        {
            get { return this.culture; }
            set
            {
                if (this.culture != value)
                {
                    this.ApplyResources(this, value);

                    this.culture = value;
                    this.OnCultureChanged();
                }
            }
        }

        public LocalizedControl()
        {
            this.resManager = new ComponentResourceManager(this.GetType());
            this.culture = CultureInfo.CurrentUICulture;
        }


        private void ApplyResources(Control parent, CultureInfo culture)
        {
            this.resManager.ApplyResources(parent, parent.Name, culture);

            foreach (Control ctl in parent.Controls)
            {
                if (ctl is MenuStrip)
                {
                    ApplyResources((ctl as MenuStrip).Items, culture);
                }
                this.ApplyResources(ctl, culture);
            }


        }

        private void ApplyResources(ToolStripItemCollection toolstriipItems, CultureInfo culture)
        {
            foreach (ToolStripItem item in toolstriipItems)
            {
                if (item is ToolStripDropDownItem)
                {
                    ApplyResources(((ToolStripDropDownItem)item).DropDownItems, culture);
                }
                resManager.ApplyResources(item, item.Name, culture);
            }
        }


        protected void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class LocalizedForm : Form
    {
        /// <summary>
        /// Occurs when current UI culture is changed
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when current UI culture is changed")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Property Changed")]
        public event EventHandler CultureChanged;

        protected CultureInfo culture;
        protected ComponentResourceManager resManager;

        /// <summary>
        /// Current culture of this form
        /// </summary>
        [Browsable(false)]
        [Description("Current culture of this form")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CultureInfo Culture
        {
            get { return this.culture; }
            set
            {
                if (this.culture != value)
                {
                    this.ApplyResources(this, value);

                    this.culture = value;
                    this.OnCultureChanged();
                }
            }
        }

        public LocalizedForm()
        {
            this.resManager = new ComponentResourceManager(this.GetType());
            this.culture = CultureInfo.CurrentUICulture;
        }

        private void ApplyResources(Control parent, CultureInfo culture)
        {
            this.resManager.ApplyResources(parent, parent.Name, culture);

            foreach (Control ctl in parent.Controls)
            {
                if(ctl is MenuStrip)
                {
                    ApplyResources((ctl as MenuStrip).Items, culture);
                }
                this.ApplyResources(ctl, culture);
            }
            

        }

        private void ApplyResources(ToolStripItemCollection toolstriipItems, CultureInfo culture)
        {
            foreach (ToolStripItem item in toolstriipItems)
            {
                if (item is ToolStripDropDownItem)
                {
                    ApplyResources(((ToolStripDropDownItem)item).DropDownItems, culture);
                }
                resManager.ApplyResources(item, item.Name, culture);
            }
        }

        protected void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class LocalizedUserControl : UserControl
    {
        /// <summary>
        /// Occurs when current UI culture is changed
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when current UI culture is changed")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Property Changed")]
        public event EventHandler CultureChanged;

        protected CultureInfo culture;
        protected ComponentResourceManager resManager;

        /// <summary>
        /// Current culture of this form
        /// </summary>
        [Browsable(false)]
        [Description("Current culture of this form")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CultureInfo Culture
        {
            get { return this.culture; }
            set
            {
                if (this.culture != value)
                {
                    this.ApplyResources(this, value);

                    this.culture = value;
                    this.OnCultureChanged();
                }
            }
        }

        public LocalizedUserControl()
        {
            this.resManager = new ComponentResourceManager(this.GetType());
            this.culture = CultureInfo.CurrentUICulture;
        }


        private void ApplyResources(Control parent, CultureInfo culture)
        {
            this.resManager.ApplyResources(parent, parent.Name, culture);

            foreach (Control ctl in parent.Controls)
            {
                if (ctl is MenuStrip)
                {
                    ApplyResources((ctl as MenuStrip).Items, culture);
                }
                this.ApplyResources(ctl, culture);
            }


        }

        private void ApplyResources(ToolStripItemCollection toolstriipItems, CultureInfo culture)
        {
            foreach (ToolStripItem item in toolstriipItems)
            {
                if (item is ToolStripDropDownItem)
                {
                    ApplyResources(((ToolStripDropDownItem)item).DropDownItems, culture);
                }
                resManager.ApplyResources(item, item.Name, culture);
            }
        }


        protected void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
