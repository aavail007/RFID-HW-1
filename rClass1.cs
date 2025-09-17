using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct MW_EasyPOD
{
    public uint VID;			        // Need to match user device's "Vendor ID".
    public uint PID;			        // Need to match user device's "Product ID".
    public uint ReadTimeOut;		    // Specifies the read data time-out interval, in milliseconds.
    public uint WriteTimeOut;		    // Specifies the write data time-out interval, in milliseconds.    
    public uint Handle;                 // Do not modify this value, reserved for DLL
    public uint FeatureReportSize;      // Do not modify this value, reserved for DLL
    public uint InputReportSize;        // Do not modify this value, reserved for DLL
    public uint OutputReportSize;       // Do not modify this value, reserved for DLL   
}


class PODfuncs
{
    [DllImport("EasyPOD.dll", CallingConvention = CallingConvention.StdCall)]
    unsafe public static extern uint ConnectPOD(MW_EasyPOD* pEasyPOD, uint Index);

    [DllImport("EasyPOD.dll", CallingConvention = CallingConvention.StdCall)]
    unsafe public static extern uint WriteData(MW_EasyPOD* pEasyPOD, byte[] lpBuffer, uint nNumberOfBytesToWrite, uint* lpNumberOfBytesWritten);

    [DllImport("EasyPOD.dll", CallingConvention = CallingConvention.StdCall)]
    unsafe public static extern uint ReadData(MW_EasyPOD* pEasyPOD, byte[] lpBuffer, uint nNumberOfBytesToRead, uint* lpNumberOfBytesRead);

    [DllImport("EasyPOD.dll", CallingConvention = CallingConvention.StdCall)]
    unsafe public static extern uint DisconnectPOD(MW_EasyPOD* pEasyPOD);

    [DllImport("EasyPOD.dll", CallingConvention = CallingConvention.StdCall)]
    unsafe public static extern uint ClearPODBuffer(MW_EasyPOD* pEasyPOD);
}


