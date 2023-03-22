using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    public static class ButtonCopy
    {
        public static Button Copy(this Button parentBut)
        {
            return new Button
            {
                Anchor    = parentBut.Anchor,
                BackColor = parentBut.BackColor,
                BackgroundImageLayout = parentBut.BackgroundImageLayout,
                Text      = parentBut.Text,
                TextAlign = parentBut.TextAlign,
                Size      = parentBut.Size,
                Font      = parentBut.Font,
                ForeColor = parentBut.ForeColor,
                FlatStyle = parentBut.FlatStyle,
                Dock      = parentBut.Dock,
            };
        }
    }
}
