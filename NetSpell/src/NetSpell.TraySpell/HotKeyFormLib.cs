using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NetSpell.TraySpell.HotKey
{
	#region Win32 Methods
	/// <summary>
	/// A class that defines all the unmanaged methods used by the HotKeyForm
	/// assembly.
	/// </summary>
	internal class Win32
	{
		internal const int IDHOT_SNAPWINDOW = -1;          /* SHIFT-PRINTSCRN  */
		internal const int IDHOT_SNAPDESKTOP = -2;         /* PRINTSCRN        */
		internal const int WM_DESTROY = 0x2;
		internal const int WM_HOTKEY = 0x312;
		
		[DllImport("user32")]
		internal static extern bool RegisterHotKey(
			IntPtr hWnd , 
			int id, 
			int fsModifiers, 
			int vk);
		[DllImport("user32")]
		internal static extern bool UnregisterHotKey(
			IntPtr hWnd, 
			int id);
		[DllImport("kernel32")] // GloablAddAtomA
		internal static extern IntPtr GlobalAddAtom(
			string lpString);
		[DllImport("kernel32")]
		internal static extern IntPtr GlobalDeleteAtom(
			IntPtr nAtom);
		// To Report API errors:
		internal const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
		internal const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
		internal const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
		internal const int FORMAT_MESSAGE_FROM_STRING = 0x400;
		internal const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
		internal const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
		internal const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xFF;
		[DllImport("kernel32")]
		internal static extern int FormatMessage(
			int dwFlags, 
			IntPtr lpSource, 
			int dwMessageId, 
			int dwLanguageId, 
			string lpBuffer, 
			int nSize, 
			ref int Arguments);
		[DllImport("kernel32")]
		internal static extern int GetTickCount();
		[DllImport("user32")]
		internal static extern int SendMessage(
			IntPtr hwnd, 
			int wMsg, 
			IntPtr wParam, 
			IntPtr lParam);
		internal const int WM_SYSCOMMAND = 0x112;
		internal const int SC_RESTORE = 0xF120;
		[DllImport("user32")]
		internal static extern bool IsIconic (IntPtr hwnd);
		[DllImport("user32")]
		internal static extern bool IsWindowVisible (IntPtr hwnd);
		[DllImport("user32")]
		internal static extern int SetForegroundWindow(IntPtr hwnd);
		[DllImport("user32")]
		internal static extern int ShowWindow (IntPtr hwnd , int nCmdShow);
		internal const int SW_SHOW = 5;

	}
	#endregion

	#region HotKeyCollection
	/// <summary>
	/// Class to store the collection of hotkeys associated with this
	/// form (as an ArrayList).
	/// </summary>
	public class HotKeyCollection : CollectionBase
	{
		System.Windows.Forms.Form ownerForm;

		protected override void OnClear()
		{
			foreach (HotKey htk in this.InnerList)
			{
				RemoveHotKey(htk);
			}
			base.OnClear();
		}

		protected override void OnInsert(int index, object item)
		{
			// validate item is a hot key:
			HotKey htk = new HotKey();
			if (item.GetType().IsInstanceOfType(htk))
			{
				// check if the name, keycode and modifiers have been set up:
				htk = (HotKey)item;
				// throws ArgumentException if there is a problem:
				htk.Validate();

				// throws Unable to add HotKeyException:
				AddHotKey(htk);

				// ok
				base.OnInsert(index, item);
			}
			else
			{
				throw new InvalidCastException("Invalid object.");
			}
		}
		protected override void OnRemove(int index, object item)
		{
			// get the item to be removed:
			HotKey htk = (HotKey)item;
			RemoveHotKey(htk);
			base.OnRemove(index, item);
		}
		protected override void OnSet(int index, object oldItem, object newItem)
		{
			// remove old hot key:
			HotKey htk = (HotKey)oldItem;
			RemoveHotKey(htk);

			// add new hotkey:
			htk = (HotKey)newItem;
			AddHotKey(htk);

			base.OnSet(index, oldItem, newItem);
		}
		protected override void OnValidate(object item)
		{
			((HotKey)item).Validate();
		}

		/// <summary>
		/// Adds a new HotKey to the form
		/// </summary>
		/// <param name="hotKey">The HotKey to add</param>
		/// <exception cref="ArgumentException">Thrown if the HotKey being added 
		/// has not been configured correctly.</exception>
		/// <exception cref="HotKeyAddException">Thrown if the HotKey cannot be added.</exception>
		public void Add(HotKey hotKey)
		{
			// throws argument exception:
			hotKey.Validate();
			// throws unable to add hot key exception:
			AddHotKey(hotKey);

			// assuming all is well:
			this.InnerList.Add(hotKey);
		}

		/// <summary>
		/// Gets the HotKey at the specified index.
		/// </summary>
		public HotKey this[int index]
		{
			get
			{
				return (HotKey)this.InnerList[index];
			}
		}

		private void RemoveHotKey(HotKey hotKey)
		{
			// remove the hot key:
			Win32.UnregisterHotKey(ownerForm.Handle, hotKey.AtomId.ToInt32());
			// unregister the atom:
			Win32.GlobalDeleteAtom(hotKey.AtomId);
		}

		private string WinErrorMsg(int lastDllError)
		{
			string buff = new String('\0', 256);
			int a = 0;
			int count = Win32.FormatMessage(
				Win32.FORMAT_MESSAGE_FROM_SYSTEM | Win32.FORMAT_MESSAGE_IGNORE_INSERTS,
				IntPtr.Zero,
				lastDllError,
				0,
				buff,
				255,
				ref a);
			if (count > 0)
			{
				return buff.Substring(0, count);
			}
			else
			{
				return "";
			}
		}

		private void AddHotKey(HotKey hotKey)
		{
			// generate the id:
			string atomName = hotKey.Name + "_" + Win32.GetTickCount().ToString();
			if (atomName.Length > 255)
			{
				atomName = atomName.Substring(0, 255);
			}
			// Create a new atom:
			IntPtr id = Win32.GlobalAddAtom(atomName);
			if (id.Equals(IntPtr.Zero))
			{
				// failed
				throw new HotKeyAddException("Failed to add GlobalAtom for HotKey");				
			}
			else
			{
				// succeeded:
				bool ret = Win32.RegisterHotKey(
					ownerForm.Handle, 
					id.ToInt32(), 
					(int)hotKey.Modifiers, 
					(int)hotKey.KeyCode);
				if (!ret)
				{
					// Remove the atom:
					Win32.GlobalDeleteAtom(id);
					// failed
					throw new HotKeyAddException("Failed to register HotKey");
				}
				else
				{
					hotKey.AtomName = atomName;
					hotKey.AtomId = id;
				}				
			}
		}

		/// <summary>
		/// Creates a new HotKey collection.  This class is for use with 
		/// HotKeyForm and is not intended to be created externally.
		/// </summary>
		/// <param name="ownerForm">The </param>
		public HotKeyCollection(System.Windows.Forms.Form ownerForm)
		{
			this.ownerForm = ownerForm;
		}
	}
	#endregion

	#region HotKeyAddException
	public class HotKeyAddException : System.Exception 
	{
		public HotKeyAddException() : base()
		{
		}
		public HotKeyAddException(string message) : base(message)
		{
		}
		public HotKeyAddException(string message, System.Exception innerException) : base (message, innerException)
		{
		}
	}
	#endregion
	
	#region HotKey
	public class HotKey
	{
		/// <summary>
		/// Enumeration of HotKeyModifiers 
		/// </summary>
		[Flags]
		public enum HotKeyModifiers : int
		{
			MOD_ALT = 0x1,
			MOD_CONTROL = 0x2,
			MOD_SHIFT = 0x4,
			MOD_WIN = 0x8
		}
		private string name;
		private string atomName;
		private IntPtr atomId;
		private Keys keyCode;
		private HotKeyModifiers modifiers;

		/// <summary>
		/// Gets/sets the id of the HotKey.
		/// </summary>
		internal IntPtr AtomId
		{
			get
			{
				return atomId;
			}
			set
			{
				atomId = value;
			}
		}

		/// <summary>
		/// Gets/sets the name of the atom used to generate the HotKey.
		/// </summary>
		internal string AtomName
		{
			get
			{
				return atomName;
			}
			set
			{
				atomName = value;
			}
		}

		/// <summary>
		/// Gets/sets the name of this HotKey.
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}
		
		/// <summary>
		/// Gets/sets the key for this HotKey.
		/// </summary>
		public Keys KeyCode
		{
			get
			{
				return keyCode;
			}
			set
			{
				keyCode = value;
			}
		}

		/// <summary>
		/// Gets/sets the modifiers for this HotKey.
		/// </summary>
		public HotKeyModifiers Modifiers
		{
			get
			{
				return modifiers;
			}
			set
			{
				modifiers = value;
			}
		}

		/// <summary>
		/// Validates the HotKey class to confirm all required fields have
		/// been set.	
		/// </summary>
		/// <exception cref="ArgumentException">If the HotKey class has not been
		/// set up correctly.</exception>
		public void Validate()
		{
			string msg = "";
			// name validation:
			if (name == null)
			{
				msg = "Name parameter cannot be null";
			}
			else if (name.Trim().Length == 0)
			{
				msg = "Name parameter cannot be zero-length";
			}
			/*
			else if (keyCode == null)
			{
				msg = "KeyCode parameter must be set";
			}
			*/
			else if ((keyCode == Keys.Alt) ||
				(keyCode == Keys.Control) ||
				(keyCode == Keys.Shift) ||
				(keyCode == Keys.ShiftKey) ||
				(keyCode == Keys.ControlKey))
			{
				msg = "KeyCode cannot be set to a modifier key";
			}
			/*
			else if (modifiers == null)
			{
				msg = "Modifiers cannot be null";
			}
			*/
			if (msg.Length > 0)
			{
				throw new ArgumentException(msg);
			}
		}

		/// <summary>
		/// Creates a new instance of a HotKey.
		/// </summary>
		public HotKey()
		{
		}
		
		/// <summary>
		/// Creates a new instance of a HotKey with the specified name,
		/// key code and modifiers.
		/// </summary>
		/// <param name="keyCode">The Key for the HotKey</param>
		/// <param name="modifiers">The Modifiers for the HotKey</param>
		/// <param name="name">The Name for this HotKey</param>
		public HotKey(
			string name,
			Keys keyCode,
			HotKeyModifiers modifiers
			)
		{
			this.name = name;
			this.keyCode = keyCode;
			this.modifiers = modifiers;
		}
	}
	#endregion

	#region HotKeyForm
	/// <summary>
	/// A Windows Form which implements System-Wide HotKeys.
	/// </summary>
	public class HotKeyForm : System.Windows.Forms.Form
	{
		#region Internal Properties
		private HotKeyCollection hotKeys = null;
		#endregion

		#region Event declarations
		public event HotKeyPressedEventHandler HotKeyPressed;
		public event PrintWindowPressedEventHandler PrintWindowPressed;
		public event PrintDesktopPressedEventHandler PrintDesktopPressed;
		#endregion

		/// <summary>
		/// Restores, activates and brings the form to the foreground.  
		/// Use if you want to display your form in response to a 
		/// HotKey event.
		/// </summary>
		public void RestoreAndActivate()
		{
			if (!Win32.IsWindowVisible(this.Handle))
			{
				Win32.ShowWindow(this.Handle, Win32.SW_SHOW);
			}
			if (Win32.IsIconic(this.Handle))
			{
				Win32.SendMessage(this.Handle, Win32.WM_SYSCOMMAND, 
					(IntPtr)Win32.SC_RESTORE, IntPtr.Zero);
			}
			Win32.SetForegroundWindow(this.Handle);
		}

		/// <summary>
		/// Gets the collection of HotKeys associated with the form.
		/// </summary>
		public HotKeyCollection HotKeys
		{
			get
			{
				return this.hotKeys;
			}
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == Win32.WM_HOTKEY)
			{
				int hotKeyId = m.WParam.ToInt32();
				switch (hotKeyId)
				{
					case Win32.IDHOT_SNAPDESKTOP:
						if (PrintDesktopPressed != null)
						{
							System.EventArgs e = new System.EventArgs();
							PrintDesktopPressed(this, e);
						}
						break;
					case Win32.IDHOT_SNAPWINDOW:
						if (PrintWindowPressed != null)
						{
							System.EventArgs e = new System.EventArgs();
							PrintWindowPressed(this, e);
						}
						break;
					default:
						foreach (HotKey htk in hotKeys)
						{
							if (htk.AtomId.Equals(m.WParam))
							{
								if (HotKeyPressed != null)
								{
									HotKeyPressedEventArgs e = new HotKeyPressedEventArgs(htk);
									HotKeyPressed(this, e);
								}
							}
						}
						break;	
				}
			}
		}

		protected override void OnClosed ( System.EventArgs e )
		{
			hotKeys.Clear();
			base.OnClosed(e);
		}

		public HotKeyForm() : base()
		{
			hotKeys = new HotKeyCollection(this);
		}
	}
	#endregion

	#region Delegate and Event Argument Declarations

	public delegate void HotKeyPressedEventHandler(object sender, HotKeyPressedEventArgs e);
	public delegate void PrintWindowPressedEventHandler(object sender, EventArgs e);
	public delegate void PrintDesktopPressedEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Provides data for a HotKey pressed event
	/// </summary>
	public class HotKeyPressedEventArgs : EventArgs
	{
		private HotKey hotKey;

		public HotKey HotKey
		{
			get
			{
				return hotKey;
			}
		}

		internal HotKeyPressedEventArgs(
			HotKey hotKey
			)
		{
			this.hotKey = hotKey;			
		}

	}
	#endregion

}
