using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekSewaPerawatanJasaPertanian_fix_.Requireds
{
    public class BigCheckBox : CheckBox
    {
        public int BoxSize { get; set; } = 32;  // bisa kamu ubah 24, 32, 40, dll
        public Color BorderColor { get; set; } = Color.Black;

        public BigCheckBox()
        {
            this.AutoSize = false;  // supaya tinggi kontrol bisa besar
            this.Height = 40;       // ubah sesuai BoxSize
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Bersihkan background (hilangkan warna hitam)
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Posisi kotak centang
            int y = (this.Height - BoxSize) / 2;
            Rectangle rect = new Rectangle(0, y, BoxSize, BoxSize);

            // Border kotak
            using (Pen pen = new Pen(BorderColor, 2))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }

            // Tanda centang
            if (this.Checked)
            {
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawLine(pen, rect.Left + 5, rect.Top + BoxSize / 2,
                                        rect.Left + BoxSize / 2, rect.Bottom - 5);

                    e.Graphics.DrawLine(pen, rect.Left + BoxSize / 2, rect.Bottom - 5,
                                        rect.Right - 5, rect.Top + 5);
                }
            }

            // Gambar teks
            TextRenderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                new Point(BoxSize + 10, (this.Height - Font.Height) / 2),
                this.ForeColor
            );

        }
    }
}
