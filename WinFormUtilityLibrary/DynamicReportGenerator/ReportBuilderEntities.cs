
using System.Data;
using System.Drawing;

namespace WinFormUtilityLibrary.DynamicReportGenerator.ReportBuilderEntities
{
    
        public static class ReportGlobalParameters
        {
            public static string CurrentPageNumber = "=Globals!PageNumber";
            public static string TotalPages = "=Globals!OverallTotalPages";
        }
        public class ReportBuilder
        {
            public ReportPage Page { get; set; }
            public ReportBody Body { get; set; }
            public DataSet DataSource { get; set; }

            private bool autoGenerateReport = true;
            public bool AutoGenerateReport
            {
                get { return autoGenerateReport; }
                set { autoGenerateReport = value; }
            }

        }
        public class ReportItems
        {
            public ReportTextBoxControl[] TextBoxControls { get; set; }
            public ReportTable[] ReportTable { get; set; }
            public ReportImage[] ReportImages { get; set; }
        }
        public class ReportTable
        {
            public string ReportName { get; set; }
            public ReportColumns[] ReportDataColumns { get; set; }
        }
        public class ReportColumns
        {
            public bool isGroupedColumn { get; set; }
            public string HeaderText { get; set; }
            public ReportSort SortDirection { get; set; }
            public ReportFunctions Aggregate { get; set; }
            public ReportTextBoxControl ColumnCell { get; set; }
            public ReportDimensions HeaderColumnPadding { get; set; }
        }
        public class ReportTextBoxControl
        {
            public string Name { get; set; }
            public string[] ValueOrExpression { get; set; }
            public ReportActions Action { get; set; }
            public ReportDimensions Padding { get; set; }
            public int SpaceAfter { get; set; }
            public int SpaceBefore { get; set; }
            public ReportHorizantalAlign TextAlign { get; set; } = ReportHorizantalAlign.Default;
            public ReportHorizantalAlign VerticalAlign { get; set; } = ReportHorizantalAlign.Default;
            public ReportStyles BorderStyle { get; set; }
            public ReportColor BorderColor { get; set; }
            public ReportScale BorderWidth { get; set; }
            public Color BackgroundColor { get; set; }
            public ReportImage BackgroundImage { get; set; }
            public Font TextFont { get; set; }
            public double LineHeight { get; set; }
            public bool CanGrow { get; set; }
            public bool CanShrink { get; set; }
            public bool ToolTip { get; set; }
            public ReportDimensions Position { get; set; }
            public ReportScale Size { get; set; }
            public bool Visible { get; set; }
        }
        public class ReportBody
        {
            public ReportSections ReportBodySection { get; set; }
            public ReportItems ReportControlItems { get; set; }
        }
        public class ReportPage
        {
            public bool AutoRefresh { get; set; }
            public Color BackgroundColor { get; set; }
            public ReportImage BackgroundImage { get; set; }
            public ReportColor BorderColor { get; set; }
            public ReportScale BorderWidth { get; set; }
            public ReportColumnSettings Columns { get; set; }
            public ReportScale InteractiveSize { get; set; }
            public ReportDimensions Margins { get; set; }
            public ReportScale PageSize { get; set; }
            public ReportSections ReportHeader { get; set; }
            public ReportSections ReportFooter { get; set; }
        }
        public class ReportSections
        {
            public ReportStyles BorderStyle { get; set; }
            public ReportColor BorderColor { get; set; }
            public ReportScale BorderWidth { get; set; }
            public Color BackgroundColor { get; set; }
            public ReportImage BackgroundImage { get; set; }
            public ReportScale Size { get; set; }

            public bool PrintOnFirstPage { get; set; } = true;
            public bool PrintOnLastPage { get; set; } = true;
            public ReportItems ReportControlItems { get; set; }
        }
        public class ReportColumnSettings
        {
            public int Columns { get; set; }
            public int ColumnsSpacing { get; set; }
        }
        public class ReportActions
        {
            public ReportActionType ActionType { get; set; }
            public string ValueOrExpression { get; set; }
        }
        public class ReportDimensions
        {
            public double Left { get; set; }
            public double Right { get; set; }
            public double Top { get; set; }
            public double Bottom { get; set; }

            public double Default { get; set; } = 2;
        }
        public class ReportIndent
        {
            public double HangingIndent { get; set; }
            public double LeftIndent { get; set; }
            public double RightIndent { get; set; }
        }
        public class ReportScale
        {
            public double Height { get; set; }
            public double Width { get; set; }
        }
        public class ReportImage
        {
            public ReportImageSource ImagePath { get; set; }
            public string ValueOrExpression { get; set; }
            public ReportImageMIMEType MIMEType { get; set; }
            public ReportStyles Border { get; set; }
            public ReportColor Color { get; set; }
            public ReportDimensions Position { get; set; }
            public ReportScale Size { get; set; }
            public ReportDimensions Padding { get; set; }
            public ReportImageScaling ReportImageScaling { get; set; } = ReportImageScaling.AutoSize;
        }
        public class ReportColor
        {
            public Color Default { get; set; }
            public Color Left { get; set; }
            public Color Right { get; set; }
            public Color Top { get; set; }
            public Color Bottom { get; set; }
        }
        public class ReportStyles
        {
            public ReportStyle Default { get; set; }
            public ReportStyle Left { get; set; }
            public ReportStyle Right { get; set; }
            public ReportStyle Top { get; set; }
            public ReportStyle Bottom { get; set; }
        }
        public enum ReportActionType
        {
            None,
            HyperLink
        }
        public enum ReportHorizantalAlign
        {
            Left,
            Right,
            Center,
            General,
            Default
        }
        public enum ReportVerticalAlign
        {
            Top,
            Middle,
            Bottom,
            Default
        }
        public enum ReportImageRepeat
        {
            Default,
            Repeat,
            RepeatX,
            RepeatY,
            Clip
        }
        public enum ReportImageScaling
        {

            AutoSize,
            Flip,
            FlipProportional,
            Clip
        }
        public enum ReportImageSource
        {
            External,
            Embedded,
            Database
        }
        public enum ReportImageMIMEType
        {
            Bitmap,
            JPEG,
            GIF,
            PNG,
            xPNG
        }
        public enum ReportStyle
        {
            Default, Dashed, Dotted, Double, Solid, None
        }
        public enum ReportSort
        {
            Ascending,
            Descending
        }
        public enum ReportFunctions
        {
            Avg,
            Count,
            Sum,
            Min,
            Max,
            Aggregate


        }
}
