using System;

namespace TestExtender
{
	/// <summary>
	/// Summary description for TextState.
	/// </summary>
	public class TextState
	{
		public TextState()
		{
		}

		public int PreviousTextLength = 0;
		public int UncheckedWordIndex = -1;
		public int CurrentWordIndex = 0;
	}
}
