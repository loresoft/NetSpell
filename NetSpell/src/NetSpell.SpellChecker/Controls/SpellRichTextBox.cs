using System;
using System.Windows.Forms;

namespace NetSpell.SpellChecker.Controls
{
	/// <summary>
	/// Summary description for SpellRichTextBox.
	/// </summary>
	public class SpellRichTextBox : RichTextBox
	{
		
        public SpellRichTextBox()
        {
            Application.Idle +=new EventHandler(OnIdle);
        }
	
        protected override void WndProc(ref Message m)
        {
            base.WndProc (ref m);
        }
    
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged (e);
        }
    
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged (e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown (e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint (e);
        }

        private void OnIdle(object sender, EventArgs e)
        {

        }
    }
}
