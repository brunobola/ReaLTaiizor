﻿#region Imports

using ReaLTaiizor.Enum.Cyber;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static ReaLTaiizor.Util.CyberLibrary;
using Timer = System.Windows.Forms.Timer;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CyberSwitch

    [ToolboxBitmap(typeof(System.Windows.Forms.CheckBox))]
    [Description("Allows the user to enable or disable the corresponding setting.")]
    public partial class CyberSwitch : UserControl
    {
        #region Variables

        private float h = 0;
        private Rectangle rectangle_region = new();
        private GraphicsPath graphicsPath = new();
        private Size size_cyberswitch = new();

        #endregion

        #region Property Region

        private bool tmp_checked_status;
        [Category("Cyber")]
        [Description("On/Off")]
        public bool Checked
        {
            get => tmp_checked_status;
            set
            {
                tmp_checked_status = value;
                CheckedChanged();
                Refresh();
            }
        }

        private bool tmp_rgb_status;
        [Category("Cyber")]
        [Description("On/Off RGB")]
        public bool RGB
        {
            get => tmp_rgb_status;
            set
            {
                tmp_rgb_status = value;

                if (tmp_rgb_status == true)
                {
                    timer_rgb.Stop();
                    if (!DrawEngine.GlobalRGB.Enabled)
                    {
                        timer_rgb.Tick += (Sender, EventArgs) =>
                        {
                            h += 4;
                            if (h >= 360)
                            {
                                h = 0;
                            }

                            Refresh();
                        };
                        timer_rgb.Start();
                    }
                }
                else
                {
                    timer_rgb.Stop();
                    Refresh();
                }
            }
        }

        private bool tmp_background;
        [Category("Cyber")]
        [Description("Background On/Off")]
        public bool Background
        {
            get => tmp_background;
            set
            {
                tmp_background = value;
                Refresh();
            }
        }

        private bool tmp_rounding_status;
        [Category("Cyber")]
        [Description("On/Off Rounding")]
        public bool Rounding
        {
            get => tmp_rounding_status;
            set
            {
                tmp_rounding_status = value;
                Refresh();
            }
        }

        private int tmp_rounding_int;
        [Category("Cyber")]
        [Description("Percentage rounding")]
        public int RoundingInt
        {
            get => tmp_rounding_int;
            set
            {
                if (value is >= 0 and <= 100)
                {
                    tmp_rounding_int = value;
                    Refresh();
                }
            }
        }

        private Color color_value;
        [Category("Cyber")]
        [Description("The color of the circle inside")]
        public Color ColorValue
        {
            get => color_value;
            set
            {
                color_value = value;
                Refresh();
            }
        }

        private Color tmp_color_background;
        [Category("Cyber")]
        [Description("Background color")]
        public Color ColorBackground
        {
            get => tmp_color_background;
            set
            {
                tmp_color_background = value;
                Refresh();
            }
        }

        private readonly Timer timer_rgb = new() { Interval = 300 };
        [Category("Timers")]
        [Description("RGB mode refresh rate (redrawing in effect)")]
        public int Timer_RGB
        {
            get => timer_rgb.Interval;
            set => timer_rgb.Interval = value;
        }

        private bool tmp_background_pen;
        [Category("BorderStyle")]
        [Description("On/Off Border")]
        public bool BackgroundPen
        {
            get => tmp_background_pen;
            set
            {
                tmp_background_pen = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private float tmp_background_width_pen;
        [Category("BorderStyle")]
        [Description("Border size")]
        public float Background_WidthPen
        {
            get => tmp_background_width_pen;
            set
            {
                tmp_background_width_pen = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private Color tmp_color_background_pen;
        [Category("BorderStyle")]
        [Description("Border color")]
        public Color ColorBackground_Pen
        {
            get => tmp_color_background_pen;
            set
            {
                tmp_color_background_pen = value;
                Refresh();
            }
        }

        private bool tmp_lineargradient_background_status;
        [Category("LinearGradient")]
        [Description("On/Off background gradient")]
        public bool LinearGradient_Background
        {
            get => tmp_lineargradient_background_status;
            set
            {
                tmp_lineargradient_background_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_background;
        [Category("LinearGradient")]
        [Description("Color #1 for background gradient")]
        public Color ColorBackground_1
        {
            get => tmp_color_1_for_gradient_background;
            set
            {
                tmp_color_1_for_gradient_background = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_background;
        [Category("LinearGradient")]
        [Description("Color #2 for background gradient")]
        public Color ColorBackground_2
        {
            get => tmp_color_2_for_gradient_background;
            set
            {
                tmp_color_2_for_gradient_background = value;
                Refresh();
            }
        }

        private bool tmp_lineargradient_value_status;
        [Category("LinearGradient")]
        [Description("On/Off Gradient Value")]
        public bool LinearGradient_Value
        {
            get => tmp_lineargradient_value_status;
            set
            {
                tmp_lineargradient_value_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_value;
        [Category("LinearGradient")]
        [Description("Color #1 for the value gradient")]
        public Color ColorBackground_Value_1
        {
            get => tmp_color_1_for_gradient_value;
            set
            {
                tmp_color_1_for_gradient_value = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_value;
        [Category("LinearGradient")]
        [Description("Color #2 for the value gradient")]
        public Color ColorBackground_Value_2
        {
            get => tmp_color_2_for_gradient_value;
            set
            {
                tmp_color_2_for_gradient_value = value;
                Refresh();
            }
        }

        private bool tmp_lighting;
        [Category("Lighting")]
        [Description("On/Off backlight")]
        public bool Lighting
        {
            get => tmp_lighting;
            set
            {
                tmp_lighting = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private Color tmp_color_lighting;
        [Category("Lighting")]
        [Description("Backlight / Shadow Color")]
        public Color ColorLighting
        {
            get => tmp_color_lighting;
            set
            {
                tmp_color_lighting = value;
                Refresh();
            }
        }

        private int tmp_alpha;
        [Category("Lighting")]
        [Description("Lighting alpha")]
        public int Alpha
        {
            get => tmp_alpha;
            set
            {
                tmp_alpha = value;
                Refresh();
            }
        }

        private int tmp_pen_width;
        [Category("Lighting")]
        [Description("Lighting width")]
        public int PenWidth
        {
            get => tmp_pen_width;
            set
            {
                tmp_pen_width = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private bool tmp_lineargradient_pen_status;
        [Category("LinearGradient")]
        [Description("On/Off border gradient")]
        public bool LinearGradientPen
        {
            get => tmp_lineargradient_pen_status;
            set
            {
                tmp_lineargradient_pen_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_pen;
        [Category("LinearGradient")]
        [Description("Color #1 for border gradient")]
        public Color ColorPen_1
        {
            get => tmp_color_1_for_gradient_pen;
            set
            {
                tmp_color_1_for_gradient_pen = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_pen;
        [Category("LinearGradient")]
        [Description("Color #2 for border gradient")]
        public Color ColorPen_2
        {
            get => tmp_color_2_for_gradient_pen;
            set
            {
                tmp_color_2_for_gradient_pen = value;
                Refresh();
            }
        }

        private SmoothingMode tmp_smoothing_mode;
        [Category("Cyber")]
        [Description("Mode <graphics.SmoothingMode>")]
        public SmoothingMode SmoothingMode
        {
            get => tmp_smoothing_mode;
            set
            {
                if (value != SmoothingMode.Invalid)
                {
                    tmp_smoothing_mode = value;
                }

                Refresh();
            }
        }

        private TextRenderingHint tmp_text_rendering_hint;
        [Category("Cyber")]
        [Description("Mode <graphics.TextRenderingHint>")]
        public TextRenderingHint TextRenderingHint
        {
            get => tmp_text_rendering_hint;
            set
            {
                tmp_text_rendering_hint = value;
                Refresh();
            }
        }

        private StateStyle tmp_cyberswitch_style = StateStyle.Default;
        [Category("Cyber")]
        [Description("Switch style")]
        public StateStyle CyberSwitchStyle
        {
            get => tmp_cyberswitch_style;
            set
            {
                tmp_cyberswitch_style = value;
                switch (tmp_cyberswitch_style)
                {
                    case StateStyle.Default:
                        Size = new Size(35, 20);
                        BackColor = Color.Transparent;
                        ForeColor = Color.FromArgb(245, 245, 245);
                        Checked = false;
                        RGB = false;
                        Background = true;
                        Rounding = true;
                        RoundingInt = 90;
                        ColorValue = Color.FromArgb(29, 200, 238);
                        ColorBackground = Color.FromArgb(37, 52, 68);
                        Timer_RGB = 300;
                        BackgroundPen = true;
                        Background_WidthPen = 2F;
                        ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                        LinearGradient_Background = false;
                        ColorBackground_1 = Color.FromArgb(37, 52, 68);
                        ColorBackground_2 = Color.FromArgb(41, 63, 86);
                        LinearGradient_Value = false;
                        ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
                        ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
                        Lighting = false;
                        ColorLighting = Color.FromArgb(29, 200, 238);
                        Alpha = 50;
                        PenWidth = 10;
                        LinearGradientPen = false;
                        ColorPen_1 = Color.FromArgb(37, 52, 68);
                        ColorPen_2 = Color.FromArgb(41, 63, 86);
                        SmoothingMode = SmoothingMode.HighQuality;
                        TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        Font = HelpEngine.GetDefaultFont();
                        break;
                    case StateStyle.Custom:
                        break;
                    case StateStyle.Random:
                        HelpEngine.GetRandom random = new();
                        Background = random.Bool();
                        Rounding = random.Bool();
                        if (Rounding)
                        {
                            RoundingInt = random.Int(5, 90);
                        }

                        if (Background)
                        {
                            ColorBackground = random.ColorArgb(random.Int(0, 255));
                        }

                        BackgroundPen = random.Bool();

                        if (BackgroundPen)
                        {
                            Background_WidthPen = random.Float(1, 3);
                            ColorBackground_Pen = random.ColorArgb(random.Int(0, 255));
                        }

                        Lighting = random.Bool();
                        if (Lighting)
                        {
                            ColorLighting = random.ColorArgb();
                        }

                        LinearGradient_Background = random.Bool();
                        if (LinearGradient_Background)
                        {
                            ColorBackground_1 = random.ColorArgb();
                            ColorBackground_2 = random.ColorArgb();
                        }

                        LinearGradient_Value = random.Bool();
                        if (LinearGradient_Value)
                        {
                            ColorBackground_Value_1 = random.ColorArgb();
                            ColorBackground_Value_2 = random.ColorArgb();
                        }

                        LinearGradientPen = random.Bool();
                        if (LinearGradientPen)
                        {
                            ColorPen_1 = random.ColorArgb();
                            ColorPen_2 = random.ColorArgb();
                        }

                        ColorValue = random.ColorArgb(random.Int(0, 255));
                        break;
                }

                Refresh();
            }
        }

        #endregion

        #region Constructor Region

        public CyberSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
            DoubleBuffered = true;

            Tag = "Cyber";
            CyberSwitchStyle = StateStyle.Default;
            CyberSwitchStyle = StateStyle.Custom;

            OnSizeChanged(null);
        }

        #endregion

        #region Event Region

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                Settings_Load(e.Graphics);
                Draw_Background(e.Graphics);

                graphicsPath.ClearMarkers();
                graphicsPath.Dispose();
            }
            catch (Exception Ex)
            {
                HelpEngine.Error($"[{Name}] Error: \n{Ex}");
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Checked = !Checked;
            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? PenWidth / 4 : 0));
            size_cyberswitch = new Size(Width - (tmp * 2), Height - (tmp * 2));
            rectangle_region = new Rectangle(tmp, tmp, size_cyberswitch.Width, size_cyberswitch.Height);
        }
        #endregion

        #region Event Handler Region

        public delegate void EventHandler();
        [Category("Cyber")]
        [Description("Occurs whenever the Checked property is changed.")]
        public event EventHandler CheckedChanged = delegate { };

        #endregion

        #region Method Region

        private void Settings_Load(Graphics graphics)
        {
            BackColor = Color.Transparent;

            graphics.SmoothingMode = SmoothingMode;
            graphics.TextRenderingHint = TextRenderingHint;
        }

        private void Draw_Background(Graphics graphics_form)
        {
            float roundingValue = 0.1F;
            void BaseLoading()
            {
                //Rounding
                if (Rounding && RoundingInt > 0)
                {
                    roundingValue = size_cyberswitch.Height / 100F * RoundingInt;
                }

                //RoundedRectangle
                graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

                //Region
                Region = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                0, 0,
                Width, Height),
                roundingValue));
            }
            Bitmap Layer_1()
            {
                Bitmap bitmap = new(Width, Height);
                Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

                //Shadow
                if (Lighting)
                {
                    DrawEngine.DrawBlurred(graphics, ColorLighting, DrawEngine.RoundedRectangle(rectangle_region, roundingValue), Alpha, PenWidth);
                }

                //Background border
                if (Background_WidthPen != 0 && BackgroundPen == true)
                {
                    Pen pen;
                    if (LinearGradientPen)
                    {
                        pen = new Pen(new LinearGradientBrush(rectangle_region, ColorPen_1, ColorPen_2, 360), Background_WidthPen);
                    }
                    else
                    {
                        pen = new Pen(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Pen, Background_WidthPen);
                    }

                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, graphicsPath);
                }

                return bitmap;
            }
            Bitmap Layer_2()
            {
                Bitmap bitmap = new(Width, Height);
                Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

                //Region_Clip
                graphics.Clip = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                    rectangle_region.X - (int)(2 + Background_WidthPen),
                    rectangle_region.Y - (int)(2 + Background_WidthPen),
                    rectangle_region.Width + ((int)(2 + Background_WidthPen) * 2),
                    rectangle_region.Height + ((int)(2 + Background_WidthPen) * 2)), Rounding ? roundingValue : 0.1F));

                //Background
                if (Background == true)
                {
                    Brush brush = new LinearGradientBrush(rectangle_region, ColorBackground_1, ColorBackground_2, 360);
                    graphics.FillPath(LinearGradient_Background ? brush : new SolidBrush(ColorBackground), graphicsPath);
                }

                //Additionally
                Draw_Checked(graphics);

                return bitmap;
            }

            BaseLoading();
            graphics_form.DrawImage(Layer_1(), new PointF(0, 0));
            graphics_form.DrawImage(Layer_2(), new PointF(0, 0));
        }

        private void Draw_Checked(Graphics graphics)
        {
            //RoundedRectangle
            Rectangle rectangle_new = new();

            if (Checked)
            {
                int num_X = (int)(rectangle_region.Width / 10 * 6.2F);
                int num_Y = rectangle_region.Height / 6;
                rectangle_new.X = rectangle_region.X + num_X;
                rectangle_new.Y = rectangle_region.Y + num_Y;
                rectangle_new.Height = rectangle_region.Height - (num_Y * 2);
                rectangle_new.Width = rectangle_new.Height;

                if (LinearGradient_Value)
                {
                    graphics.FillEllipse(new LinearGradientBrush(rectangle_region,
                    RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1,
                    RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2,
                    360), rectangle_new);
                }
                else
                {
                    graphics.FillEllipse(new SolidBrush(
                    RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorValue), rectangle_new);
                }
            }
            else
            {
                int num_X = rectangle_region.Width / 10;
                int num_Y = rectangle_region.Height / 6;
                rectangle_new.X = rectangle_region.X + num_X;
                rectangle_new.Y = rectangle_region.Y + num_Y;
                rectangle_new.Height = rectangle_region.Height - (num_Y * 2);
                rectangle_new.Width = rectangle_new.Height;
                float size_brightness = 0.5F;

                if (LinearGradient_Value)
                {
                    graphics.FillEllipse(new LinearGradientBrush(rectangle_region,
                    Color.FromArgb(
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1).R * size_brightness),
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1).G * size_brightness),
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1).B * size_brightness)),
                    Color.FromArgb(
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2).R * size_brightness),
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2).G * size_brightness),
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2).B * size_brightness)),
                    360), rectangle_new);
                }
                else
                {
                    graphics.FillEllipse(new SolidBrush(Color.FromArgb(100,
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorValue).R * size_brightness),
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorValue).G * size_brightness),
                    (int)((RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorValue).B * size_brightness))), rectangle_new);
                }
            }
        }

        #endregion
    }

    #endregion
}