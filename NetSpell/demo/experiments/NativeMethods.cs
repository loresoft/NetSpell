using System;
using System.Runtime.InteropServices;

namespace NetSpell.SpellChecker.Controls
{
	/// <summary>
	/// Summary description for NativeMethods.
	/// </summary>
	internal sealed class NativeMethods
	{
		// RichEdit messages 
		internal const int WM_SETREDRAW				 =0x000B; 

		internal const int WM_CONTEXTMENU			 =0x007B; 
		internal const int WM_UNICHAR				 =0x0109; 
		internal const int WM_PRINTCLIENT			 =0x0318; 
		internal const int WM_USER					 =0x0400;

		internal const int EM_GETLIMITTEXT			 =(WM_USER + 37); 
		internal const int EM_POSFROMCHAR			 =(WM_USER + 38); 
		internal const int EM_CHARFROMPOS			 =(WM_USER + 39); 
		internal const int EM_SCROLLCARET			 =(WM_USER + 49); 
		internal const int EM_CANPASTE				 =(WM_USER + 50); 
		internal const int EM_DISPLAYBAND			 =(WM_USER + 51); 
		internal const int EM_EXGETSEL				 =(WM_USER + 52); 
		internal const int EM_EXLIMITTEXT			 =(WM_USER + 53); 
		internal const int EM_EXLINEFROMCHAR		 =(WM_USER + 54); 
		internal const int EM_EXSETSEL				 =(WM_USER + 55); 
		internal const int EM_FINDTEXT				 =(WM_USER + 56); 
		internal const int EM_FORMATRANGE			 =(WM_USER + 57); 
		internal const int EM_GETCHARFORMAT			 =(WM_USER + 58); 
		internal const int EM_GETEVENTMASK			 =(WM_USER + 59); 
		internal const int EM_GETOLEINTERFACE		 =(WM_USER + 60); 
		internal const int EM_GETPARAFORMAT			 =(WM_USER + 61); 
		internal const int EM_GETSELTEXT			 =(WM_USER + 62); 
		internal const int EM_HIDESELECTION			 =(WM_USER + 63); 
		internal const int EM_PASTESPECIAL			 =(WM_USER + 64); 
		internal const int EM_REQUESTRESIZE			 =(WM_USER + 65); 
		internal const int EM_SELECTIONTYPE			 =(WM_USER + 66); 
		internal const int EM_SETBKGNDCOLOR			 =(WM_USER + 67); 
		internal const int EM_SETCHARFORMAT			 =(WM_USER + 68); 
		internal const int EM_SETEVENTMASK			 =(WM_USER + 69); 
		internal const int EM_SETOLECALLBACK		 =(WM_USER + 70); 
		internal const int EM_SETPARAFORMAT			 =(WM_USER + 71); 
		internal const int EM_SETTARGETDEVICE		 =(WM_USER + 72); 
		internal const int EM_STREAMIN				 =(WM_USER + 73); 
		internal const int EM_STREAMOUT				 =(WM_USER + 74); 
		internal const int EM_GETTEXTRANGE			 =(WM_USER + 75); 
		internal const int EM_FINDWORDBREAK			 =(WM_USER + 76); 
		internal const int EM_SETOPTIONS			 =(WM_USER + 77); 
		internal const int EM_GETOPTIONS			 =(WM_USER + 78); 
		internal const int EM_FINDTEXTEX			 =(WM_USER + 79); 

		internal const int EM_GETWORDBREAKPROCEX	 =(WM_USER + 80); 
		internal const int EM_SETWORDBREAKPROCEX	 =(WM_USER + 81); 


		// RichEdit 2.0 messages 
		internal const int EM_SETUNDOLIMIT			 =(WM_USER + 82); 
		internal const int EM_REDO					 =(WM_USER + 84); 
		internal const int EM_CANREDO				 =(WM_USER + 85); 
		internal const int EM_GETUNDONAME			 =(WM_USER + 86); 
		internal const int EM_GETREDONAME			 =(WM_USER + 87); 
		internal const int EM_STOPGROUPTYPING		 =(WM_USER + 88); 

		internal const int EM_SETTEXTMODE			 =(WM_USER + 89); 
		internal const int EM_GETTEXTMODE			 =(WM_USER + 90); 

		internal const int EM_AUTOURLDETECT			 =(WM_USER + 91); 
		internal const int EM_GETAUTOURLDETECT		 =(WM_USER + 92); 
		internal const int EM_SETPALETTE			 =(WM_USER + 93); 
		internal const int EM_GETTEXTEX				 =(WM_USER + 94); 
		internal const int EM_GETTEXTLENGTHEX		 =(WM_USER + 95); 
		internal const int EM_SHOWSCROLLBAR			 =(WM_USER + 96); 
		internal const int EM_SETTEXTEX				 =(WM_USER + 97); 

		// Extended edit style specific messages 
		internal const int EM_SETEDITSTYLE			 =(WM_USER + 204); 
		internal const int EM_GETEDITSTYLE			 =(WM_USER + 205); 

		// Extended edit style masks 
		internal const int SES_EMULATESYSEDIT		 =1; 
		internal const int SES_BEEPONMAXTEXT		 =2; 
		internal const int SES_EXTENDBACKCOLOR		 =4; 
		internal const int SES_MAPCPS				 =8; 
		internal const int SES_EMULATE10			 =16; 
		internal const int SES_USECRLF				 =32; 
		internal const int SES_USEAIMM				 =64; 
		internal const int SES_NOIME				 =128; 

		internal const int SES_ALLOWBEEPS			 =256; 
		internal const int SES_UPPERCASE			 =512; 
		internal const int SES_LOWERCASE			 =1024; 
		internal const int SES_NOINPUTSEQUENCECHK	 =2048; 
		internal const int SES_BIDI					 =4096; 
		internal const int SES_SCROLLONKILLFOCUS	 =8192; 
		internal const int SES_XLTCRCRLFTOCR		 =16384; 
		internal const int SES_DRAFTMODE			 =32768; 

		internal const int SES_USECTF				 =0x0010000; 
		internal const int SES_HIDEGRIDLINES		 =0x0020000; 
		internal const int SES_USEATFONT			 =0x0040000; 
		internal const int SES_CUSTOMLOOK			 =0x0080000; 
		internal const int SES_LBSCROLLNOTIFY		 =0x0100000; 
		internal const int SES_CTFALLOWEMBED		 =0x0200000; 
		internal const int SES_CTFALLOWSMARTTAG		 =0x0400000; 
		internal const int SES_CTFALLOWPROOFING		 =0x0800000; 

		// Options for EM_SETLANGOPTIONS and EM_GETLANGOPTIONS 
		internal const int IMF_AUTOKEYBOARD			 =0x0001; 
		internal const int IMF_AUTOFONT				 =0x0002; 
		internal const int IMF_IMECANCELCOMPLETE	 =0x0004; 	// High completes comp string when aborting, low cancels
		internal const int IMF_IMEALWAYSSENDNOTIFY	 =0x0008; 
		internal const int IMF_AUTOFONTSIZEADJUST	 =0x0010; 
		internal const int IMF_UIFONTS				 =0x0020; 
		internal const int IMF_DUALFONT				 =0x0080; 

		// Values for EM_GETIMECOMPMODE 
		internal const int ICM_NOTOPEN				 =0x0000; 
		internal const int ICM_LEVEL3				 =0x0001; 
		internal const int ICM_LEVEL2				 =0x0002; 
		internal const int ICM_LEVEL2_5				 =0x0003; 
		internal const int ICM_LEVEL2_SUI			 =0x0004; 
		internal const int ICM_CTF					 =0x0005; 

		// Options for EM_SETTYPOGRAPHYOPTIONS 
		internal const int TO_ADVANCEDTYPOGRAPHY	 =1; 
		internal const int TO_SIMPLELINEBREAK		 =2; 
		internal const int TO_DISABLECUSTOMTEXTOUT	 =4; 
		internal const int TO_ADVANCEDLAYOUT		 =8; 

		// RichEdit 4.0 messages
		internal const int EM_GETPAGE				 =(WM_USER + 228); 
		internal const int EM_SETPAGE				 =(WM_USER + 229); 
		internal const int EM_GETHYPHENATEINFO		 =(WM_USER + 230); 
		internal const int EM_SETHYPHENATEINFO		 =(WM_USER + 231); 
		internal const int EM_GETPAGEROTATE			 =(WM_USER + 235); 
		internal const int EM_SETPAGEROTATE			 =(WM_USER + 236); 
		internal const int EM_GETCTFMODEBIAS		 =(WM_USER + 237); 
		internal const int EM_SETCTFMODEBIAS		 =(WM_USER + 238); 
		internal const int EM_GETCTFOPENSTATUS		 =(WM_USER + 240); 
		internal const int EM_SETCTFOPENSTATUS		 =(WM_USER + 241); 
		internal const int EM_GETIMECOMPTEXT		 =(WM_USER + 242); 
		internal const int EM_ISIME					 =(WM_USER + 243); 
		internal const int EM_GETIMEPROPERTY		 =(WM_USER + 244); 

		// EM_SETPAGEROTATE wparam values
		internal const int EPR_0					 =0; 		// Text flows left to right and top to bottom
		internal const int EPR_270					 =1; 		// Text flows top to bottom and right to left
		internal const int EPR_180					 =2; 		// Text flows right to left and bottom to top
		internal const int EPR_90					 =3; 		// Text flows bottom to top and left to right

		// EM_SETCTFMODEBIAS wparam values
		internal const int CTFMODEBIAS_DEFAULT					 =0x0000; 
		internal const int CTFMODEBIAS_FILENAME					 =0x0001; 
		internal const int CTFMODEBIAS_NAME						 =0x0002; 
		internal const int CTFMODEBIAS_READING					 =0x0003; 
		internal const int CTFMODEBIAS_DATETIME					 =0x0004; 
		internal const int CTFMODEBIAS_CONVERSATION				 =0x0005; 
		internal const int CTFMODEBIAS_NUMERIC					 =0x0006; 
		internal const int CTFMODEBIAS_HIRAGANA					 =0x0007; 
		internal const int CTFMODEBIAS_KATAKANA					 =0x0008; 
		internal const int CTFMODEBIAS_HANGUL					 =0x0009; 
		internal const int CTFMODEBIAS_HALFWIDTHKATAKANA		 =0x000A; 
		internal const int CTFMODEBIAS_FULLWIDTHALPHANUMERIC	 =0x000B; 
		internal const int CTFMODEBIAS_HALFWIDTHALPHANUMERIC	 =0x000C; 

		// New notifications 
		internal const int EN_MSGFILTER				 =0x0700; 
		internal const int EN_REQUESTRESIZE			 =0x0701; 
		internal const int EN_SELCHANGE				 =0x0702; 
		internal const int EN_DROPFILES				 =0x0703; 
		internal const int EN_PROTECTED				 =0x0704; 
		internal const int EN_CORRECTTEXT			 =0x0705; 			// PenWin specific 
		internal const int EN_STOPNOUNDO			 =0x0706; 
		internal const int EN_IMECHANGE				 =0x0707; 			// East Asia specific 
		internal const int EN_SAVECLIPBOARD			 =0x0708; 
		internal const int EN_OLEOPFAILED			 =0x0709; 
		internal const int EN_OBJECTPOSITIONS		 =0x070a; 
		internal const int EN_LINK					 =0x070b; 
		internal const int EN_DRAGDROPDONE			 =0x070c; 
		internal const int EN_PARAGRAPHEXPANDED		 =0x070d; 
		internal const int EN_PAGECHANGE			 =0x070e; 
		internal const int EN_LOWFIRTF				 =0x070f; 
		internal const int EN_ALIGNLTR				 =0x0710; 			// BiDi specific notification
		internal const int EN_ALIGNRTL				 =0x0711; 			// BiDi specific notification

		// Event notification masks 
		internal const int ENM_NONE					 =0x00000000; 
		internal const int ENM_CHANGE				 =0x00000001; 
		internal const int ENM_UPDATE				 =0x00000002; 
		internal const int ENM_SCROLL				 =0x00000004; 
		internal const int ENM_SCROLLEVENTS			 =0x00000008; 
		internal const int ENM_DRAGDROPDONE			 =0x00000010; 
		internal const int ENM_PARAGRAPHEXPANDED	 =0x00000020; 
		internal const int ENM_PAGECHANGE			 =0x00000040; 
		internal const int ENM_KEYEVENTS			 =0x00010000; 
		internal const int ENM_MOUSEEVENTS			 =0x00020000; 
		internal const int ENM_REQUESTRESIZE		 =0x00040000; 
		internal const int ENM_SELCHANGE			 =0x00080000; 
		internal const int ENM_DROPFILES			 =0x00100000; 
		internal const int ENM_PROTECTED			 =0x00200000; 
		internal const int ENM_CORRECTTEXT			 =0x00400000; 		// PenWin specific 
		internal const int ENM_IMECHANGE			 =0x00800000; 		// Used by RE1.0 compatibility
		internal const int ENM_LANGCHANGE			 =0x01000000; 
		internal const int ENM_OBJECTPOSITIONS		 =0x02000000; 
		internal const int ENM_LINK					 =0x04000000; 
		internal const int ENM_LOWFIRTF				 =0x08000000; 


		// New edit control styles 
		internal const int ES_SAVESEL				 =0x00008000; 
		internal const int ES_SUNKEN				 =0x00004000; 
		internal const int ES_DISABLENOSCROLL		 =0x00002000; 
		// Same as WS_MAXIMIZE, but that doesn't make sense so we re-use the value 
		internal const int ES_SELECTIONBAR			 =0x01000000; 
		// Same as ES_UPPERCASE, but re-used to completely disable OLE drag'n'drop 
		internal const int ES_NOOLEDRAGDROP			 =0x00000008; 

		// Edit control options 
		internal const int ECO_AUTOWORDSELECTION	 =0x00000001; 
		internal const int ECO_AUTOVSCROLL			 =0x00000040; 
		internal const int ECO_AUTOHSCROLL			 =0x00000080; 
		internal const int ECO_NOHIDESEL			 =0x00000100; 
		internal const int ECO_READONLY				 =0x00000800; 
		internal const int ECO_WANTRETURN			 =0x00001000; 
		internal const int ECO_SAVESEL				 =0x00008000; 
		internal const int ECO_SELECTIONBAR			 =0x01000000; 
		internal const int ECO_VERTICAL				 =0x00400000; 		// FE specific 


		// ECO operations 
		internal const int ECOOP_SET				 =0x0001; 
		internal const int ECOOP_OR					 =0x0002; 
		internal const int ECOOP_AND				 =0x0003; 
		internal const int ECOOP_XOR				 =0x0004; 

		// Word break function actions 
		internal const int WB_LEFT				 =0; 
		internal const int WB_RIGHT				 =1; 
		internal const int WB_ISDELIMITER		 =2; 
		internal const int WB_CLASSIFY			 =3; 
		internal const int WB_MOVEWORDLEFT		 =4; 
		internal const int WB_MOVEWORDRIGHT		 =5; 
		internal const int WB_LEFTBREAK			 =6; 
		internal const int WB_RIGHTBREAK		 =7; 

		// Word break flags (used with WB_CLASSIFY) 
		internal const int WBF_CLASS			= 0x0F;
		internal const int WBF_ISWHITE			= 0x10;
		internal const int WBF_BREAKLINE		= 0x20;
		internal const int WBF_BREAKAFTER		= 0x40;

		// CFM_COLOR mirrors CFE_AUTOCOLOR, a little hack to easily deal with autocolor

		// CHARFORMAT masks 
		internal const int CFM_BOLD			 =0x00000001; 
		internal const int CFM_ITALIC		 =0x00000002; 
		internal const int CFM_UNDERLINE	 =0x00000004; 
		internal const int CFM_STRIKEOUT	 =0x00000008; 
		internal const int CFM_PROTECTED	 =0x00000010; 
		internal const int CFM_LINK			 =0x00000020; 			// Exchange hyperlink extension 
		
		internal const int CFM_SIZE			 =unchecked( (int) 0x80000000 ); 
		internal const int CFM_COLOR		 =0x40000000; 
		internal const int CFM_FACE			 =0x20000000; 
		internal const int CFM_OFFSET		 =0x10000000; 
		internal const int CFM_CHARSET		 =0x08000000; 

		// CHARFORMAT effects 
		internal const int CFE_BOLD			 =0x0001; 
		internal const int CFE_ITALIC		 =0x0002; 
		internal const int CFE_UNDERLINE	 =0x0004; 
		internal const int CFE_STRIKEOUT	 =0x0008; 
		internal const int CFE_PROTECTED	 =0x0010; 
		internal const int CFE_LINK			 =0x0020; 

		internal const int CFE_AUTOCOLOR	 =0x40000000; 			// NOTE: this corresponds to CFM_COLOR, which controls it 
		
		// Masks and effects defined for CHARFORMAT2 -- an (*) indicates
		// that the data is stored by RichEdit 2.0/3.0, but not displayed
		internal const int CFM_SMALLCAPS		 =0x0040; 			// (*)	
		internal const int CFM_ALLCAPS			 =0x0080; 			// Displayed by 3.0	
		internal const int CFM_HIDDEN			 =0x0100; 			// Hidden by 3.0 
		internal const int CFM_OUTLINE			 =0x0200; 			// (*)	
		internal const int CFM_SHADOW			 =0x0400; 			// (*)	
		internal const int CFM_EMBOSS			 =0x0800; 			// (*)	
		internal const int CFM_IMPRINT			 =0x1000; 			// (*)	
		internal const int CFM_DISABLED			 =0x2000; 
		internal const int CFM_REVISED			 =0x4000; 

		internal const int CFM_BACKCOLOR		 =0x04000000; 
		internal const int CFM_LCID				 =0x02000000; 
		internal const int CFM_UNDERLINETYPE	 =0x00800000; 		// Many displayed by 3.0 
		internal const int CFM_WEIGHT			 =0x00400000; 
		internal const int CFM_SPACING			 =0x00200000; 		// Displayed by 3.0	
		internal const int CFM_KERNING			 =0x00100000; 		// (*)	
		internal const int CFM_STYLE			 =0x00080000; 		// (*)	
		internal const int CFM_ANIMATION		 =0x00040000; 		// (*)	
		internal const int CFM_REVAUTHOR		 =0x00008000; 

		internal const int CFE_SUBSCRIPT		 =0x00010000; 		// Superscript and subscript are 
		internal const int CFE_SUPERSCRIPT		 =0x00020000; 		//  mutually exclusive			 

		internal const int CFM_SUBSCRIPT		 =CFE_SUBSCRIPT | CFE_SUPERSCRIPT;
		internal const int CFM_SUPERSCRIPT		 =CFM_SUBSCRIPT; 

		// CHARFORMAT "ALL" masks
		internal const int CFM_EFFECTS			 =(CFM_BOLD | CFM_ITALIC 
													| CFM_UNDERLINE | CFM_COLOR 
													| CFM_STRIKEOUT | CFE_PROTECTED 
													| CFM_LINK); 

		internal const int CFM_ALL				 =(CFM_EFFECTS | CFM_SIZE 
													| CFM_FACE | CFM_OFFSET 
													| CFM_CHARSET); 

		internal const int CFM_EFFECTS2			 =(CFM_EFFECTS | CFM_DISABLED 
													| CFM_SMALLCAPS | CFM_ALLCAPS 
													| CFM_HIDDEN  | CFM_OUTLINE 
													| CFM_SHADOW | CFM_EMBOSS 
													| CFM_IMPRINT | CFM_DISABLED 
													| CFM_REVISED | CFM_SUBSCRIPT 
													| CFM_SUPERSCRIPT | CFM_BACKCOLOR); 

		internal const int CFM_ALL2				 =(CFM_ALL | CFM_EFFECTS2 
													| CFM_BACKCOLOR | CFM_LCID 
													| CFM_UNDERLINETYPE | CFM_WEIGHT 
													| CFM_REVAUTHOR | CFM_SPACING 
													| CFM_KERNING | CFM_STYLE 
													| CFM_ANIMATION); 

		internal const int CFE_SMALLCAPS		 =CFM_SMALLCAPS; 
		internal const int CFE_ALLCAPS			 =CFM_ALLCAPS; 
		internal const int CFE_HIDDEN			 =CFM_HIDDEN; 
		internal const int CFE_OUTLINE			 =CFM_OUTLINE; 
		internal const int CFE_SHADOW			 =CFM_SHADOW; 
		internal const int CFE_EMBOSS			 =CFM_EMBOSS; 
		internal const int CFE_IMPRINT			 =CFM_IMPRINT; 
		internal const int CFE_DISABLED			 =CFM_DISABLED; 
		internal const int CFE_REVISED			 =CFM_REVISED; 

		// CFE_AUTOCOLOR and CFE_AUTOBACKCOLOR correspond to CFM_COLOR and
		// CFM_BACKCOLOR, respectively, which control them
		internal const int CFE_AUTOBACKCOLOR	 =CFM_BACKCOLOR; 

		// Underline types. RE 1.0 displays only CFU_UNDERLINE
		internal const int CFU_CF1UNDERLINE					 =0xFF; 	// Map charformat's bit underline to CF2
		internal const int CFU_INVERT						 =0xFE; 	// For IME composition fake a selection
		internal const int CFU_UNDERLINETHICKLONGDASH		 =18; 	// (*) display as dash
		internal const int CFU_UNDERLINETHICKDOTTED			 =17; 	// (*) display as dot
		internal const int CFU_UNDERLINETHICKDASHDOTDOT		 =16; 	// (*) display as dash dot dot
		internal const int CFU_UNDERLINETHICKDASHDOT		 =15; 	// (*) display as dash dot
		internal const int CFU_UNDERLINETHICKDASH			 =14; 	// (*) display as dash
		internal const int CFU_UNDERLINELONGDASH			 =13; 	// (*) display as dash
		internal const int CFU_UNDERLINEHEAVYWAVE			 =12; 	// (*) display as wave
		internal const int CFU_UNDERLINEDOUBLEWAVE			 =11; 	// (*) display as wave
		internal const int CFU_UNDERLINEHAIRLINE			 =10; 	// (*) display as single	
		internal const int CFU_UNDERLINETHICK				 =9; 
		internal const int CFU_UNDERLINEWAVE				 =8; 
		internal const int CFU_UNDERLINEDASHDOTDOT			 =7; 
		internal const int CFU_UNDERLINEDASHDOT				 =6; 
		internal const int CFU_UNDERLINEDASH				 =5; 
		internal const int CFU_UNDERLINEDOTTED				 =4; 
		internal const int CFU_UNDERLINEDOUBLE				 =3; 	// (*) display as single
		internal const int CFU_UNDERLINEWORD				 =2; 	// (*) display as single	
		internal const int CFU_UNDERLINE					 =1; 
		internal const int CFU_UNDERLINENONE				 =0; 


		// EM_SETCHARFORMAT wParam masks 
		internal const int SCF_SELECTION		 =0x0001; 
		internal const int SCF_WORD				 =0x0002; 
		internal const int SCF_DEFAULT			 =0x0000; 	// Set default charformat or paraformat
		internal const int SCF_ALL				 =0x0004; 	// Not valid with SCF_SELECTION or SCF_WORD


		// PARAFORMAT mask values 
		internal const int PFM_STARTINDENT			 =0x00000001; 
		internal const int PFM_RIGHTINDENT			 =0x00000002; 
		internal const int PFM_OFFSET				 =0x00000004; 
		internal const int PFM_ALIGNMENT			 =0x00000008; 
		internal const int PFM_TABSTOPS				 =0x00000010; 
		internal const int PFM_NUMBERING			 =0x00000020; 
		internal const int PFM_OFFSETINDENT			 =unchecked( (int) 0x80000000 ); 

		// PARAFORMAT 2.0 masks and effects 
		internal const int PFM_SPACEBEFORE			 =0x00000040; 
		internal const int PFM_SPACEAFTER			 =0x00000080; 
		internal const int PFM_LINESPACING			 =0x00000100; 
		internal const int PFM_STYLE				 =0x00000400; 
		internal const int PFM_BORDER				 =0x00000800; 	// (*)	
		internal const int PFM_SHADING				 =0x00001000; 	// (*)	
		internal const int PFM_NUMBERINGSTYLE		 =0x00002000; 	// RE 3.0	
		internal const int PFM_NUMBERINGTAB			 =0x00004000; 	// RE 3.0	
		internal const int PFM_NUMBERINGSTART		 =0x00008000; 	// RE 3.0	

		internal const int PFM_RTLPARA				 =0x00010000; 
		internal const int PFM_KEEP					 =0x00020000; 	// (*)	
		internal const int PFM_KEEPNEXT				 =0x00040000; 	// (*)	
		internal const int PFM_PAGEBREAKBEFORE		 =0x00080000; 	// (*)	
		internal const int PFM_NOLINENUMBER			 =0x00100000; 	// (*)	
		internal const int PFM_NOWIDOWCONTROL		 =0x00200000; 	// (*)	
		internal const int PFM_DONOTHYPHEN			 =0x00400000; 	// (*)	
		internal const int PFM_SIDEBYSIDE			 =0x00800000; 	// (*)	
		internal const int PFM_TABLE				 =0x40000000; 	// RE 3.0 
		internal const int PFM_TEXTWRAPPINGBREAK	 =0x20000000; 	// RE 3.0 
		internal const int PFM_TABLEROWDELIMITER	 =0x10000000; 	// RE 4.0 

		// The following three properties are read only
		internal const int PFM_COLLAPSED			 =0x01000000; 	// RE 3.0 
		internal const int PFM_OUTLINELEVEL			 =0x02000000; 	// RE 3.0 
		internal const int PFM_BOX					 =0x04000000; 	// RE 3.0 
		internal const int PFM_RESERVED2			 =0x08000000; 	// RE 4.0 


		// PARAFORMAT "ALL" masks
		internal const int PFM_ALL					 =(PFM_STARTINDENT | PFM_RIGHTINDENT 
														| PFM_OFFSET | PFM_ALIGNMENT   
														| PFM_TABSTOPS | PFM_NUMBERING 
														| PFM_OFFSETINDENT| PFM_RTLPARA); 

		// Note: PARAFORMAT has no effects (BiDi RichEdit 1.0 does have PFE_RTLPARA)
		internal const int PFM_EFFECTS				=(PFM_RTLPARA | PFM_KEEP 
														| PFM_KEEPNEXT | PFM_TABLE 
														| PFM_PAGEBREAKBEFORE | PFM_NOLINENUMBER 
														| PFM_NOWIDOWCONTROL | PFM_DONOTHYPHEN 
														| PFM_SIDEBYSIDE | PFM_TABLE 
														| PFM_TABLEROWDELIMITER); 

		internal const int PFM_ALL2					 =(PFM_ALL | PFM_EFFECTS 
														| PFM_SPACEBEFORE | PFM_SPACEAFTER 
														| PFM_LINESPACING | PFM_STYLE 
														| PFM_SHADING | PFM_BORDER 
														| PFM_NUMBERINGTAB | PFM_NUMBERINGSTART 
														| PFM_NUMBERINGSTYLE); 

		internal const int PFE_RTLPARA				 =(PFM_RTLPARA			 >> 16); 
		internal const int PFE_KEEP					 =(PFM_KEEP				 >> 16); 	// (*)	
		internal const int PFE_KEEPNEXT				 =(PFM_KEEPNEXT			 >> 16); 	// (*)	
		internal const int PFE_PAGEBREAKBEFORE		 =(PFM_PAGEBREAKBEFORE	 >> 16); 	// (*)	
		internal const int PFE_NOLINENUMBER			 =(PFM_NOLINENUMBER		 >> 16); 	// (*)	
		internal const int PFE_NOWIDOWCONTROL		 =(PFM_NOWIDOWCONTROL	 >> 16); 	// (*)	
		internal const int PFE_DONOTHYPHEN			 =(PFM_DONOTHYPHEN 		 >> 16); 	// (*)	
		internal const int PFE_SIDEBYSIDE			 =(PFM_SIDEBYSIDE		 >> 16); 	// (*)	
		internal const int PFE_TEXTWRAPPINGBREAK	 =(PFM_TEXTWRAPPINGBREAK >> 16);	// (*)	

		// The following four effects are read only
		internal const int PFE_COLLAPSED			 =(PFM_COLLAPSED		 >> 16); 	// (+)	
		internal const int PFE_BOX					 =(PFM_BOX				 >> 16); 	// (+)	
		internal const int PFE_TABLE				 =(PFM_TABLE			 >> 16); 	// Inside table row. RE 3.0 
		internal const int PFE_TABLEROWDELIMITER	 =(PFM_TABLEROWDELIMITER >> 16); 	// Table row start. RE 4.0 

		// PARAFORMAT numbering options 
		internal const int PFN_BULLET		 =1; 		// tomListBullet

		// PARAFORMAT2 wNumbering options 
		internal const int PFN_ARABIC		 =2; 		// tomListNumberAsArabic:   0, 1, 2,	...
		internal const int PFN_LCLETTER		 =3; 		// tomListNumberAsLCLetter: a, b, c,	...
		internal const int PFN_UCLETTER		 =4; 		// tomListNumberAsUCLetter: A, B, C,	...
		internal const int PFN_LCROMAN		 =5; 		// tomListNumberAsLCRoman:  i, ii, iii,	...
		internal const int PFN_UCROMAN		 =6; 		// tomListNumberAsUCRoman:  I, II, III,	...

		// PARAFORMAT2 wNumberingStyle options 
		internal const int PFNS_PAREN		 =0x000; 	// default, e.g.,				  1)	
		internal const int PFNS_PARENS		 =0x100; 	// tomListParentheses/256, e.g., (1)	
		internal const int PFNS_PERIOD		 =0x200; 	// tomListPeriod/256, e.g.,		  1.	
		internal const int PFNS_PLAIN		 =0x300; 	// tomListPlain/256, e.g.,		  1		
		internal const int PFNS_NONUMBER	 =0x400; 	// Used for continuation w/o number
		internal const int PFNS_NEWNUMBER	 =0x8000; 	// Start new number with wNumberingStart (can be combined with other PFNS_xxx)
		
		// PARAFORMAT alignment options 
		internal const int PFA_LEFT				 =1; 
		internal const int PFA_RIGHT			 =2; 
		internal const int PFA_CENTER			 =3; 

		// PARAFORMAT2 alignment options 
		internal const int PFA_JUSTIFY			 =4; 	// New paragraph-alignment option 2.0 (*) 
		internal const int PFA_FULL_INTERWORD	 =4; 	// These are supported in 3.0 with advanced
		internal const int PFA_FULL_INTERLETTER  =5; 	//  typography enabled
		internal const int PFA_FULL_SCALED		 =6; 
		internal const int PFA_FULL_GLYPHS		 =7; 
		internal const int PFA_SNAP_GRID		 =8; 


		[StructLayout(LayoutKind.Sequential, Pack=4, CharSet=CharSet.Auto)]
		public struct CHARFORMAT2
		{
			public int		cbSize;
			public int		dwMask;
			public int		dwEffects;
			public int		yHeight;
			public int		yOffset;			// > 0 for superscript, < 0 for subscript 
			public int		crTextColor;
			public byte		bCharSet;
			public byte		bPitchAndFamily;
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=32 )]
			public string	szFaceName;
			public short	wWeight;			// Font weight (LOGFONT value)		
			public short	sSpacing;			// Amount to space between letters	
			public int		crBackColor;		// Background color					
			public int		lcid;				// Locale ID						
			public int		dwReserved;			// Reserved. Must be 0				
			public short	sStyle;				// Style handle						
			public short	wKerning;			// Twip size above which to kern char pair
			public byte		bUnderlineType;		// Underline type					
			public byte		bAnimation;			// Animated text like marching ants	
			public byte		bRevAuthor;			// Revision author index		
		}	 

		[StructLayout(LayoutKind.Sequential)]
		public struct PARAFORMAT2
		{
			public int		cbSize;
			public int		dwMask;
			public short	wNumbering;
			public short	wReserved;
			public int		dxStartIndent;
			public int		dxRightIndent;
			public int		dxOffset;
			public short	wAlignment;
			public short	cTabCount;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
			public int[]	rgxTabs;
			public int		dySpaceBefore;			// Vertical spacing before para			
			public int		dySpaceAfter;			// Vertical spacing after para			
			public int		dyLineSpacing;			// Line spacing depending on Rule		
			public short	sStyle;					// Style handle							
			public byte		bLineSpacingRule;		// Rule for line spacing (see tom.doc)	
			public byte		bOutlineLevel;			// Outline Level						
			public short	wShadingWeight;			// Shading in hundredths of a per cent	
			public short	wShadingStyle;			// Byte 0: style, nib 2: cfpat, 3: cbpat
			public short	wNumberingStart;		// Starting value for numbering				
			public short	wNumberingStyle;		// Alignment, Roman/Arabic, (), ), ., etc.
			public short	wNumberingTab;			// Space bet 1st indent and 1st-line text
			public short	wBorderSpace;			// Border-text spaces (nbl/bdr in pts)	
			public short	wBorderWidth;			// Pen widths (nbl/bdr in half twips)	
			public short	wBorders;				// Border styles (nibble/border)		
		}	

		private NativeMethods()
		{
		}

		// Win API declaration
		[DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref CHARFORMAT2 cf2);
		[DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref PARAFORMAT2 pf2);
		[DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam); 
 

	}
}
