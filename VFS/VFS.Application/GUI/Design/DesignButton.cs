using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design
{
    public class DesignButton : Design.DesignPanel
    {
        private bool isSelected = false;

        public DesignButton()
        {

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            isSelected = true;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isSelected)
                e.Graphics.FillRectangle(new SolidBrush(Color.Orange), this.DisplayRectangle);
            e.Graphics.DrawImage(this.BackgroundImage, this.DisplayRectangle);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            isSelected = false;
            this.Invalidate();

        }
    }
}
