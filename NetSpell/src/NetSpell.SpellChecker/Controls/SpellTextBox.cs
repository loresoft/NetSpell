using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;

using NetSpell.SpellChecker;

namespace NetSpell.SpellChecker.Controls
{
	/// <summary>
	/// Summary description for SpellTextBox.
	/// </summary>
	public class SpellTextBox : System.Windows.Forms.RichTextBox
	{
		private Spelling _SpellChecker;
		private bool _AutoSpellCheck = true;
		private UnderlineColor _MisspelledColor = UnderlineColor.Red;
		private UnderlineStyle _MisspelledStyle = UnderlineStyle.UnderlineWave;
		private int _lastTextLength = 0;
		private int _lastCharClass = 0;

		/// <summary>
		///		Underline Colors
		/// </summary>
		public enum UnderlineColor
		{
			Default = 0x00,
			Blue	= 0x10,
			Aqua	= 0x20,
			Lime 	= 0x30,
			Fuchsia = 0x40,
			Red		= 0x50,
			Yellow	= 0x60,
			White	= 0x70,
			Navy	= 0x80,
			Teal	= 0x90,
			Green	= 0xa0,
			Purple	= 0xb0,
			Maroon	= 0xc0,
			Olive	= 0xd0,
			Gray	= 0xe0,
			Silver	= 0xf0
		}

		/// <summary>
		///		Underline Styles
		/// </summary>
		public enum UnderlineStyle
		{
			Underline					=1,
			UnderlineDouble				=3,
			UnderlineDotted				=4,
			UnderlineDash				=5,
			UnderlineDashDot			=6,
			UnderlineDashDotDot			=7,
			UnderlineWave				=8,
			UnderlineThick				=9,
			UnderlineHairline			=10,
			UnderlineDoubleWave			=11,
			UnderlineHeavyWave			=12,
			UnderlineLongDash			=13,
			UnderlineThickDash			=14,
			UnderlineThickDashDot		=15,
			UnderlineThickDashDotDot	=16,
			UnderlineThickDotted		=17,
			UnderlineThickLongDash		=18
		}

		/// <summary>
		/// Summary description for SpellTextBox.
		/// </summary>
		public SpellTextBox()
		{
		}

		private void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			Console.WriteLine("Misspelled Word:{0}", args.Word);
			
			int selectionStart = base.SelectionStart;
			int selectionLength = base.SelectionLength;

			base.Select(selectionStart + args.TextIndex, args.Word.Length);

			NativeMethods.CHARFORMAT2 cf = new NativeMethods.CHARFORMAT2();
			cf.cbSize = Marshal.SizeOf(cf);
			cf.dwMask = NativeMethods.CFM_UNDERLINETYPE;
			cf.bUnderlineType = (byte)this.MisspelledStyle; 
			cf.bUnderlineType |= (byte)this.MisspelledColor; 
			
			int result = NativeMethods.SendMessage(base.Handle, 
				NativeMethods.EM_SETCHARFORMAT, 
				NativeMethods.SCF_SELECTION | NativeMethods.SCF_WORD, 
				ref cf);

			base.Select(selectionStart, selectionLength);
		}

		private NativeMethods.CHARFORMAT2 GetCharFormat()
		{
			NativeMethods.CHARFORMAT2 cf = new NativeMethods.CHARFORMAT2();
			cf.cbSize = Marshal.SizeOf(cf);
			int result = NativeMethods.SendMessage(base.Handle, 
				NativeMethods.EM_GETCHARFORMAT, 
				NativeMethods.SCF_SELECTION, 
				ref cf);

			return cf; 
		} 

		/// <summary>
		///     The Color to use to mark misspelled words
		/// </summary>
		[Browsable(true)]
		[CategoryAttribute("Spell")]
		[Description("The Color to use to mark misspelled words")]
		[DefaultValue(UnderlineColor.Red)]
		public UnderlineColor MisspelledColor
		{
			get {return _MisspelledColor;}
			set {_MisspelledColor = value;}
		}

		/// <summary>
		///     The Underline style used to mark misspelled words
		/// </summary>
		[Browsable(true)]
		[CategoryAttribute("Spell")]
		[Description("The underline style used to mark misspelled words")]
		[DefaultValue(UnderlineStyle.UnderlineWave)]
		public UnderlineStyle MisspelledStyle
		{
			get {return _MisspelledStyle;}
			set {_MisspelledStyle = value;}
		}

		/// <summary>
		///     Gets or sets the selections background color
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SelectionBackColor
		{
			get
			{
				NativeMethods.CHARFORMAT2 cf = this.GetCharFormat();
				return ColorTranslator.FromOle(cf.crBackColor);
			}
			set
			{
				NativeMethods.CHARFORMAT2 cf = new NativeMethods.CHARFORMAT2();
				cf.cbSize = Marshal.SizeOf(cf);
				cf.dwMask = NativeMethods.CFM_BACKCOLOR;
				cf.crBackColor = ColorTranslator.ToWin32(value);
			
				int result = NativeMethods.SendMessage(base.Handle, 
					NativeMethods.EM_SETCHARFORMAT, 
					NativeMethods.SCF_SELECTION, 
					ref cf);
			}
		}

		/// <summary>
		///     Automatically mark misspelled words
		/// </summary>
		[Browsable(true)]
		[CategoryAttribute("Spell")]
		[Description("Automatically mark misspelled words")]
		[DefaultValue(true)]
		public bool AutoSpellCheck
		{
			get {return _AutoSpellCheck;}
			set {_AutoSpellCheck = value;}
		}

		/// <summary>
		///     The Spelling Object to use when checking words
		/// </summary>
		[Browsable(true)]
		[CategoryAttribute("Spell")]
		[Description("The Spelling Object to use when checking words")]
		public Spelling SpellChecker
		{
			get {return _SpellChecker;}
			set 
			{
				if(value != null)
				{
					_SpellChecker = value;
					_SpellChecker.MisspelledWord += new Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
				}
			}
		}

	
		protected override void OnTextChanged(EventArgs e)
		{
			int changeRange = this.Text.Length - _lastTextLength;
			_lastTextLength = this.Text.Length;

			if(_SpellChecker != null && _AutoSpellCheck && changeRange > 0)
			{
				
				// get char flags for previous char
				int charFlags = NativeMethods.SendMessage(this.Handle, 
					NativeMethods.EM_FINDWORDBREAK, 
					NativeMethods.WB_CLASSIFY, 
					base.SelectionStart - 1);

				int charClass = (charFlags & NativeMethods.WBF_CLASS);
				bool isBreak = (charFlags & NativeMethods.WBF_BREAKLINE) == NativeMethods.WBF_BREAKLINE ? true : false;
				bool isWhite = (charFlags & NativeMethods.WBF_ISWHITE) == NativeMethods.WBF_ISWHITE ? true : false;
			
				if(((isBreak || isWhite)&& _lastCharClass == 0) || changeRange > 1)
				{
					int eventMask = NativeMethods.SendMessage(this.Handle, 
						NativeMethods.EM_SETEVENTMASK, 0, 0);
					NativeMethods.SendMessage(base.Handle, 
						NativeMethods.WM_SETREDRAW, 0, 0);

					
					int	selectStart = base.SelectionStart;
					int selectLength = base.SelectionLength;

					int wordLeft = NativeMethods.SendMessage(this.Handle, 
						NativeMethods.EM_FINDWORDBREAK, 
						NativeMethods.WB_LEFT, 
						selectStart - (changeRange + 1));
										
					base.Select(wordLeft, selectStart - wordLeft);
					
					bool dialog = this.SpellChecker.ShowDialog;
					Console.WriteLine("Spell Check:{0}", base.SelectedText);

					this.SpellChecker.Text = base.SelectedText;
					this.SpellChecker.ShowDialog = false;
					while (this.SpellChecker.SpellCheck()) 
					{
						this.SpellChecker.WordIndex += 1;
					}

					this.SpellChecker.ShowDialog = dialog;

					base.Select(selectStart, selectLength);

					eventMask = NativeMethods.SendMessage(this.Handle,
						NativeMethods.EM_SETEVENTMASK, 0, eventMask);
					NativeMethods.SendMessage(base.Handle, 
						NativeMethods.WM_SETREDRAW, 1, 0);
				}
				_lastCharClass = charClass;
			}
			else
			{
				// recheck current word if previously misspelled

			}
			base.OnTextChanged (e);
		}
	}
}
