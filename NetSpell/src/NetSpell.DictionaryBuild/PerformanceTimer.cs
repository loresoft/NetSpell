// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Runtime.InteropServices;

/// <summary>
///		Use this class to profile your code and any other operation
///		typically with a precision greater than 1 millionth of a second
/// </summary>
public class PerformanceTimer
{
	private long _Frequency = 0;
	private float _TotalTime = 0;
	private long _StartTime = 0;
	private long _EndTime = 0;

	[DllImport("KERNEL32")]
	private static extern bool QueryPerformanceCounter( ref long lpPerformanceCount);

	[DllImport("KERNEL32")]
	private static extern bool QueryPerformanceFrequency( ref long lpFrequency);

	/// <summary>
	///     The default constructor automaticly starts the timer
	/// </summary>
	public PerformanceTimer()
	{
		QueryPerformanceFrequency(ref _Frequency);
		this.StartTimer();
	}

	/// <summary>
	///     Starts the timer
	/// </summary>
	public void StartTimer()
	{
		//get the current value of the counter
		QueryPerformanceCounter(ref _StartTime);
	}

	/// <summary>
	///     Stops the timer
	/// </summary>
	/// <returns>
	///     the time elapsed since StartTimer
	/// </returns>
	public float StopTimer()
	{
		// get the elapsed time
		float result = this.ElapsedTime;
		// update the total time counter
		_TotalTime += (_EndTime - _StartTime);
		// reset starting time
		_StartTime = 0;

		return result;
	}

	/// <summary>
	///     returns the elapsed time in seconds since StartTimer,
	///     without stopping the timer
	/// </summary>
	public float ElapsedTime
	{
		get 
		{
			if(_StartTime == 0) return 0;
			
			// get the current value of the counter
			QueryPerformanceCounter(ref _EndTime);
			// return the elapsed time in seconds
			return ((float)_EndTime - (float)_StartTime) / (float)_Frequency;
		}
	}

	/// <summary>
	///     returns the total time in seconds that the 
	///     timer has been running
	/// </summary>
	public float TotalTime
	{
		get 
		{
			if(_StartTime == 0)
			    _TotalTime = _TotalTime / (float)_Frequency;
			else
				_TotalTime = (_TotalTime + ((float)_EndTime - (float)_StartTime)) / (float)_Frequency;

			return _TotalTime;
		}
		set {_TotalTime = value;}
	}

	/// <summary>
	///     returns the timer precision in seconds
	/// </summary>
	public float Precision
	{
		get {return 1 / ((float)_Frequency * 10000);}
	}

}

