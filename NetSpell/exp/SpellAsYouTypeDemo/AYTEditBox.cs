using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SpellAsYouTypeDemo
{
	/// <summary>
	/// Summary description for AYTEditBox.
	/// </summary>
    public class AYTTextBox : System.Windows.Forms.TextBox
    {


        float _extendDisplay=0.50f;
        StringFormat _format=StringFormat.GenericDefault;
        NetSpell.SpellChecker.Spelling _spell;
        static System.Drawing.TextureBrush _spellBrush;
        bool _spellUpdateNeeded=false;
        ArrayList _wrongWord=new ArrayList();

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public struct WordInfo
        {
            public int start; 
            public string word;

            public static WordInfo Zero
            {
                get
                {
                    return new WordInfo();
                }
            }
        }

        
        static AYTTextBox()
        {
            Bitmap bmp=new Bitmap(4,2);
            bmp.SetPixel(0,0,Color.Red);
            bmp.SetPixel(1,1,Color.Red);
            bmp.SetPixel(2,1,Color.Red);
            bmp.SetPixel(3,0,Color.Red);

            _spellBrush=new TextureBrush(bmp);

        }

        public AYTTextBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();


            _format.FormatFlags=StringFormatFlags.FitBlackBox;			
            _spell=new NetSpell.SpellChecker.Spelling();

            Application.Idle+=new EventHandler(OnIdle);
			
        }

		
        private void OnIdle(object sender, EventArgs e)
        {
            if(_spellUpdateNeeded)
            {

                string str=Text;
                string []words=str.Split(new char[]{' ', ',' , '\'' , '.', ':', '-', '!', '?'});

                ArrayList wrongWordList=new ArrayList();

                int l=0;
                for(int i=0; i<str.Length; i++)
                {
                    if(char.IsLetterOrDigit(str[i]))
                    {
                        l++;
                    }
                    else
                    {
                        if(l>0)
                        {
                            string word=str.Substring(i-l, l);

                            if(word.Length>0 && _spell.TestWord(word)==false	)
                            {
                                WordInfo wi=new WordInfo();
                                wi.start=i-l;
                                wi.word=word;
                                wrongWordList.Add(wi);
                            }
                        }
                        l=0;
                    }
                }

                /*		foreach(string word in words)
                        {
                            if(word.Length>0 && wrongWordList.Contains(word)==false
                                && _spell.TestWord(word)==false	)
                            {
                                wrongWordList.Add(word);
                            }
                        }*/

                WrongWord=wrongWordList;
                Invalidate();
            }
            _spellUpdateNeeded=false;
        }

        private void OnIgnoreWord(object sender, EventArgs e)
        {

        }

		
        private void OnSuggestedWordClick(object sender, EventArgs e)
        {
            MenuItemEx mnu=sender as MenuItemEx;

            WordInfo wi=(WordInfo)mnu.Tag;

            int oldstart=SelectionStart;
            int oldlength=SelectionLength;

            Text=Text.Remove(wi.start, wi.word.Length);
            Text=Text.Insert(wi.start, mnu.Text);            

            SelectionStart=oldstart + mnu.Text.Length - wi.word.Length;
            SelectionLength=oldlength;
        }

        void UnderlineWord(Graphics g, int startpos, int length)
        {
            Point last_pt=new Point(0,0);

            Point startpt=PosFromChar(startpos);
            if(startpos + length >= TextLength) length--;
            Point endpt=PosFromChar(startpos + length);

            startpt.Y+=Font.Height;
            endpt.Y+=Font.Height;

			
            g.DrawLine(new Pen(_spellBrush, 2), startpt, endpt);
        }

        public int CharFromPos(Point pt)
        {
            int ptval=pt.Y & 0x0000FFFF;
            ptval=ptval<<16;
            ptval=ptval | (pt.X & 0x0000FFFF);

            return NativeMethods.SendMessage(Handle, NativeMethods.EM_CHARFROMPOS, 0, ptval) & 0x0000FFFF;
        }

		
        public Point PosFromChar(int chr)
        {			
            return new Point(NativeMethods.SendMessage(Handle, NativeMethods.EM_POSFROMCHAR, chr, 0));
        }

        public void UnderlineWord(Graphics g, WordInfo wi)
        {
            if(TextLength<=1) return;
			
            UnderlineWord(g, wi.start, wi.word.Length);
        }

        public WordInfo WordFromPos(Point pt)
        {
            int charpos=CharFromPos(pt);
            string str=Text;

            if(!char.IsLetterOrDigit(str[charpos])) return WordInfo.Zero;

            int start=charpos;
            int end=charpos;
            while(start>0 && char.IsLetterOrDigit(str[start-1]))
            {
                start--;
            }

            while(end<str.Length && char.IsLetterOrDigit(str[end]))
            {
                end++;
            }

            WordInfo wi=new WordInfo();
            wi.word=str.Substring(start, end-start).Trim();
            wi.start=start;

            return wi;
        }



		


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if( components != null )
                    components.Dispose();
            }
            base.Dispose( disposing );
        }
	
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right)
            {
                WordInfo wi=WordFromPos(new Point(e.X, e.Y));
                if(wi.word!=null)
                {
                    if(_spell.TestWord(wi.word)==false)
                    {
                        _spell.Suggest(wi.word);
                        ContextMenu menu=new ContextMenu();

                        foreach(string str in _spell.Suggestions)
                        {
                            MenuItemEx mnuitem=new MenuItemEx(str);
                            mnuitem.Click+=new EventHandler(OnSuggestedWordClick);
                            mnuitem.Tag=wi;
							
                            menu.MenuItems.Add(mnuitem);

                            // show only 7 suggestion
                            if(menu.MenuItems.Count>6) break;
                        }

                        if(menu.MenuItems.Count==0)
                        {
                            MenuItem nosug=new MenuItem("No suggestion");
                            nosug.Enabled=false;
                            menu.MenuItems.Add(nosug);
                        }

                        menu.MenuItems.Add(new MenuItem("-"));						
                        MenuItemEx mnuIgnore=new MenuItemEx("Ignore");
                        mnuIgnore.Tag=wi;
                        mnuIgnore.Click+=new EventHandler(OnIgnoreWord);
                        menu.MenuItems.Add(mnuIgnore);

                        Point pt=PosFromChar(wi.start);

                        pt.Y+=Font.Height +1 ;

                        menu.Show(this, pt);
                    }
                }				
            }
            Invalidate();
            base.OnMouseDown (e);
        }
	
        protected override void OnMouseUp(MouseEventArgs e)
        {
            //Invalidate();
            base.OnMouseUp (e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            foreach(WordInfo word in WrongWord)
            {
                UnderlineWord(pe.Graphics, word);
            }

            base.OnPaint(pe);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            _spellUpdateNeeded=true;
            //Invalidate();
            base.OnTextChanged (e);
        }
	
        //bool isScrolling=false;
        protected override void WndProc(ref Message m)
        {
            // TODO:  Add AYTTextBox.WndProc implementation

            if(m.Msg==0x000F)
            {			
                base.WndProc (ref m);
				
				
                Graphics g = base.CreateGraphics();
				
                OnPaint(new PaintEventArgs(g, base.ClientRectangle));

                g.Dispose();
				
                return;
            }
            else if(m.Msg == 533)				
            {
                //Invalidate();
            }
            else if(m.Msg == 277)				
            {	
                //Invalidate();
            }

            base.WndProc (ref m);
        }

        ArrayList WrongWord
        {
            get
            {
                return _wrongWord;
            }
            set
            {
                _wrongWord=value;
            }
        }
        public Rectangle TextRectangle
        {
            get
            {
                Rectangle r=DisplayRectangle;
                r.X+=(int)(Font.Size * _extendDisplay);
                r.Width-=(int)(Font.Size * _extendDisplay * 2) + 1;
                r.Y+=1;
                return r;
            }
        }

		
        public class MenuItemEx : MenuItem
        {	

            object _tag;
            public MenuItemEx(string text) : base(text)
            {
            }

            public object Tag
            {
                get
                {
                    return _tag;
                }set
                 {
                     _tag=value;
                 }
            }
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // AYTTextBox
            // 

        }
        #endregion
    }
}
