using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace Design
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class DesignPanel : Panel
    {
        private Color borderColor = Color.FromArgb(127, 204, 194, 194);
        private int borderThickness = 2;

        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            } 
        }

        public int BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                borderThickness = value;
                this.Invalidate();
            }
        }

        public DesignPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(BorderColor), BorderThickness), new Rectangle(0, 0, this.Width - borderThickness, this.Height - borderThickness));
        }
    }
}
