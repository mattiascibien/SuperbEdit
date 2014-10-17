using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SuperbEdit.TextEditor.Controls
{
    /// <summary>
    /// Interaction logic for SuperbTextBox.xaml
    /// </summary>
    public partial class SuperbTextBox : TextBox
    {
        #region LineNumber Properties

        public static readonly DependencyProperty ShowLineNumbersProperty = DependencyProperty.Register("ShowLineNumbers", typeof(bool), typeof(SuperbTextBox),
   new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
        [Category("LineNumbers")]
        public bool ShowLineNumbers
        {
            get { return (bool)GetValue(ShowLineNumbersProperty); }
            set { SetValue(ShowLineNumbersProperty, value); }
        }

        public static readonly DependencyProperty LineNumberForegroundProperty = DependencyProperty.Register("LineNumberForeground", typeof(Brush), typeof(SuperbTextBox),
   new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Gray), FrameworkPropertyMetadataOptions.AffectsRender));
        [Category("LineNumbers")]
        public Brush LineNumberForeground
        {
            get { return (Brush)GetValue(LineNumberForegroundProperty); }
            set { SetValue(LineNumberForegroundProperty, value); }
        }


        public static readonly DependencyProperty LineNumberMarginWidthProperty = DependencyProperty.Register("LineNumberMarginWidth", typeof(double), typeof(SuperbTextBox),
   new FrameworkPropertyMetadata(25.0, FrameworkPropertyMetadataOptions.AffectsRender));
        [Category("LineNumbers")]
        public double LineNumberMarginWidth
        {
            get { return (Double)GetValue(LineNumberMarginWidthProperty); }
            set { SetValue(LineNumberMarginWidthProperty, value); }
        }


        public static readonly DependencyProperty StartingLineNumberProperty = DependencyProperty.Register("StartingLineNumber", typeof(int), typeof(SuperbTextBox),
   new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsRender));
        [Category("LineNumbers")]
        public int StartingLineNumber
        {
            get { return (int)GetValue(StartingLineNumberProperty); }
            set { SetValue(StartingLineNumberProperty, value); }
        }

        #endregion


        private FormattedText formattedText;
        private SuperbRenderInfo renderinfo = new SuperbRenderInfo();
        private DispatcherTimer renderTimer;


        private string VisibleText
        {
            get
            {
                if (this.Text == "") { return ""; }
                string visibleText = "";
                try
                {
                    int textLength = Text.Length;
                    int firstLine = GetFirstVisibleLineIndex();
                    int lastLine = GetLastVisibleLineIndex();

                    int lineCount = this.LineCount;
                    int firstChar =
                       (firstLine == 0) ? 0 : GetCharacterIndexFromLineIndex(firstLine);

                    int lastChar = GetCharacterIndexFromLineIndex(lastLine) +
                                   GetLineLength(lastLine) - 1;
                    int length = lastChar - firstChar + 1;
                    int maxlenght = textLength - firstChar;
                    string text = Text.Substring(firstChar, Math.Min(maxlenght, length));
                    if (text != null)
                    {
                        visibleText = text;
                    }
                }
                catch
                {
                    Debug.WriteLine("GetVisibleText failure");
                }
                return visibleText;
            }
        }

        private Point GetRenderPoint(int firstChar)
        {
            try
            {
                Rect cRect = GetRectFromCharacterIndex(firstChar);
                Point renderPoint = new Point(cRect.Left, cRect.Top);
                if (!Double.IsInfinity(cRect.Top))
                {
                    renderinfo.RenderPoint = renderPoint;
                }
                else
                {
                    this.renderTimer.IsEnabled = true;
                }
                return renderinfo.RenderPoint;
            }
            catch (Exception e)
            {
                this.renderTimer.IsEnabled = true;
                return renderinfo.RenderPoint;
            }
        }

        public SuperbTextBox()
        {
            this.TextChanged += OnTextChanged;
            this.PreviewMouseWheel += OnPreviewMouseWheel;
            this.PreviewKeyDown += OnPreviewKeyDown;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.TextWrapping = TextWrapping.Wrap;
            renderTimer = new System.Windows.Threading.DispatcherTimer();
            renderTimer.IsEnabled = false;
            renderTimer.Tick += RenderTimerOnTick;
            renderTimer.Interval = TimeSpan.FromMilliseconds(50);
            InitializeComponent();
            this.AcceptsReturn = true;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            if (keyEventArgs.Key == Key.NumPad0 || keyEventArgs.Key == Key.D0)
            {
                //SET to the default font size
            }
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs mouseWheelEventArgs)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            int delta = mouseWheelEventArgs.Delta;

            delta = Math.Sign(delta);

            if (this.FontSize >= 2.0 && delta < 0)
                this.FontSize -= 1.0;
            else
            {
                this.FontSize += 1.0;
            }

        }

        private void RenderTimerOnTick(object sender, EventArgs eventArgs)
        {
            //TODO: this actually happens after the glitch
            renderTimer.IsEnabled = false;
            this.InvalidateVisual();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            //TODO: commenting this removes the glitch but line numbers do not draw
            //unless line > line before
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            EnsureScrolling();
            base.OnRender(drawingContext);

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                
            }
            else
            {
                if (this.LineCount == 0)
                {
                    ReRenderLastRuntimeRender(drawingContext);
                    renderTimer.IsEnabled = true;
                }
                else
                {
                    OnRenderRuntime(drawingContext);
                }
            }
        }

        private void OnRenderRuntime(DrawingContext drawingContext)
        {
            drawingContext.PushClip(new RectangleGeometry(new Rect(0, 0, this.ActualWidth, this.ActualHeight)));//restrict drawing to textbox
            drawingContext.DrawRectangle(this.Background, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));//Draw Background
            if (this.Text == "") return;

            int firstLine = GetFirstVisibleLineIndex();// GetFirstLine();
            int firstChar = (firstLine == 0) ? 0 : GetCharacterIndexFromLineIndex(firstLine);// GetFirstChar();
            string visibleText = VisibleText;
            if (visibleText == null) return;

            Double leftMargin = 4.0 + this.BorderThickness.Left;
            Double topMargin = 2.0 + this.BorderThickness.Top;

            formattedText = new FormattedText(
                   this.VisibleText,
                    CultureInfo.GetCultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface(this.FontFamily.Source),
                    this.FontSize,
                    this.Foreground);  //Text that matches the textbox's
            formattedText.Trimming = TextTrimming.None;

            ApplyTextWrapping(formattedText);

            Point renderPoint = GetRenderPoint(firstChar);

            drawingContext.DrawText(formattedText, renderPoint);

            if (ShowLineNumbers && this.LineNumberMarginWidth > 0) //Are line numbers being used
            { //Even if we gey this far it is still possible for the line numbers to fail
                if (this.GetLastVisibleLineIndex() != -1)
                {
                    FormattedText lineNumbers = GenerateLineNumbers();
                    drawingContext.DrawText(lineNumbers, new Point(3, renderPoint.Y));
                    renderinfo.LineNumbers = lineNumbers;
                }
                else
                {
                    drawingContext.DrawText(renderinfo.LineNumbers, new Point(3, renderPoint.Y));
                }
            }

            //Cache information for possible rerender
            renderinfo.BoxText = formattedText;
        }

        private void ApplyTextWrapping(FormattedText formattedText)
        {
            switch (this.TextWrapping)
            {
                case TextWrapping.NoWrap:
                    break;
                case TextWrapping.Wrap:
                    formattedText.MaxTextWidth = this.ViewportWidth; //Used with Wrap only
                    break;
                case TextWrapping.WrapWithOverflow:
                    
                    break;
            }
        }

        private FormattedText GenerateLineNumbers()
        {
            switch (this.TextWrapping)
            {
                case TextWrapping.NoWrap:
                    return LineNumberWithoutWrap();
                case TextWrapping.Wrap:
                    return LineNumberWithWrap();
                case TextWrapping.WrapWithOverflow:
                    return LineNumberWithWrap();
            }
            return null;
        }

        /// <summary>
        /// Generates FormattedText for line numbers when TextWrapping = None
        /// </summary>
        /// <returns></returns>
        private FormattedText LineNumberWithoutWrap()
        {
            int firstLine = GetFirstVisibleLineIndex();
            int lastLine = GetLastVisibleLineIndex();
            StringBuilder sb = new StringBuilder();
            for (int i = firstLine; i <= lastLine; i++)
            {
                sb.Append((i + StartingLineNumber) + "\n");
            }
            string lineNumberString = sb.ToString();
            FormattedText lineNumbers = new FormattedText(
                  lineNumberString,
                    CultureInfo.GetCultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface(this.FontFamily.Source),
                    this.FontSize,
                    LineNumberForeground);
            return lineNumbers;
        }

        /// <summary>
        /// Generates FormattedText for line numbers when TextWrapping = Wrap or WrapWithOverflow
        /// </summary>
        /// <returns></returns>
        private FormattedText LineNumberWithWrap()
        {
            try
            {
                int[] linePos = MinLineStartCharcterPositions();
                int[] lineStart = VisibleLineStartCharcterPositions();
                if (lineStart != null)
                {
                    string lineNumberString = LineNumbers(lineStart, linePos);
                    FormattedText lineText = new FormattedText(
                          lineNumberString,
                            CultureInfo.GetCultureInfo("en-us"),
                            FlowDirection.LeftToRight,
                            new Typeface(this.FontFamily.Source),
                            this.FontSize,
                            LineNumberForeground);

                    renderinfo.LineNumbers = lineText;
                    return lineText;
                }
                else
                {
                    return renderinfo.LineNumbers;
                }
            }
            catch
            {
                return renderinfo.LineNumbers;

            }

        }

        /// <summary>
        /// Returns the character positions that start lines as determined only by the characters.
        /// </summary>
        /// <returns></returns>
        private int[] MinLineStartCharcterPositions()
        {
            int totalChars = this.Text.Length;
            char[] boxChars = this.Text.ToCharArray();
            char newlineChar = Convert.ToChar("\n");
            char returnChar = Convert.ToChar("\r");
            char formfeed = Convert.ToChar("\f");
            char vertQuote = Convert.ToChar("\v");

            List<int> breakChars = new List<int>() { 0 };

            //This looks a bit exotic but keep in mind that \r\n or \r or \n or \f or \v all will 
            //signify a new line to the textbox.
            if (boxChars.Length > 1)
            {
                for (int i = 2; i < boxChars.Length; i++)
                {
                    if (boxChars[i - 1] == returnChar && boxChars[i - 2] == newlineChar)
                    {
                        breakChars.Add(i);
                    }
                    if (boxChars[i - 1] == newlineChar && boxChars[i] != returnChar)
                    {
                        breakChars.Add(i);
                    }
                    if (boxChars[i - 1] == formfeed || boxChars[i - 1] == vertQuote)
                    {
                        breakChars.Add(i);
                    }
                }
            }
            int[] MinPositions = new int[breakChars.Count];
            breakChars.CopyTo(MinPositions);
            return MinPositions;
        }


        /// <summary>
        /// Returns the character positions that the textbox declares to begin the 
        /// visible lines.
        /// </summary>
        /// <returns></returns>
        private int[] VisibleLineStartCharcterPositions()
        {
            int firstLine = GetFirstVisibleLineIndex();
            int lastLine = GetLastVisibleLineIndex();
            if (lastLine != -1)
            {
                int lineCount = lastLine - firstLine + 1;
                int[] startingPositions = new int[lineCount];
                for (int i = firstLine; i <= lastLine; i++)
                {
                    int startPos = this.GetCharacterIndexFromLineIndex(i);
                    startingPositions[i - firstLine] = startPos;
                }


                return startingPositions;

            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// Create the String of line numbers. Uses merge algorithm http://en.wikipedia.org/wiki/Merge_algorithm
        /// </summary>
        /// <param name="listA">The List of the first characters of the visible lines. This is affected by box width.</param>
        /// <param name="listB">The List of First Characters of the Lines determined by characters rather than  box width.</param>
        /// <returns></returns>
        private string LineNumbers(int[] listA, int[] listB)
        {
            StringBuilder sb = new StringBuilder();
            int a = 0;
            int b = 0;
            List<int> matches = new List<int>();
            List<int> skipped = new List<int>();
            while (a < listA.Length && b < listB.Length)
            {
                if (listA[a] == listB[b])
                {
                    matches.Add(b);
                    a++;
                    b++;
                }
                else if (listA[a] < listB[b])
                {
                    matches.Add(-1);
                    a++;
                }
                else
                {
                    skipped.Add(b);
                    b++;
                }
            }
            while (a < listA.Length)
            {
                a++;
            }

            while (b < listB.Length)
            {
                b++;
            }

            //There will be missing lien numbers where the lines are blank.
            //The skipped lines are returned.

            // in reverse because ther could be more than one sequential blank line.
            for (int i = (skipped.Count - 1); i >= 0; i--)
            {
                // index  is the position directly before the index in the matches array of
                //one greaer than the missing elements. 
                int index = matches.IndexOf(skipped[i] + 1) - 1;
                if (index > -1)
                {
                    matches[index] = skipped[i];
                }
            }

            //Adjusts the line numbers so that line 0 has the value of StartingLineNumber
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i] != -1) matches[i] += this.StartingLineNumber;
            }

            StringBuilder sb2 = new StringBuilder();
            foreach (int i in matches)
            {
                if (i == -1)
                {
                    sb2.Append("\n");
                }
                else
                {
                    sb2.Append(i + "\n");
                }
            }
            return sb2.ToString();
        }

        private void ReRenderLastRuntimeRender(DrawingContext drawingContext)
        {
            drawingContext.DrawText(renderinfo.BoxText, renderinfo.RenderPoint);
            if (this.LineNumberMarginWidth > 0) //Are line numbers being used
            {
                drawingContext.DrawText(renderinfo.LineNumbers, new Point(3, renderinfo.RenderPoint.Y));
            }
        }

        private void EnsureScrolling()
        {
            if (!mScrollingEventEnabled)
            {
                try
                {
                    DependencyObject dp = VisualTreeHelper.GetChild(this, 0);
                    dp = VisualTreeHelper.GetChild(dp, 0);
                    ScrollViewer sv = dp as ScrollViewer;
                    sv.ScrollChanged += SvOnScrollChanged;
                    mScrollingEventEnabled = true;
                }
                catch (Exception e)
                {
                    
                }
            }
        }

        private void SvOnScrollChanged(object sender, ScrollChangedEventArgs scrollChangedEventArgs)
        {
            this.InvalidateVisual();
        }

        public bool mScrollingEventEnabled { get; set; }
    }
}
