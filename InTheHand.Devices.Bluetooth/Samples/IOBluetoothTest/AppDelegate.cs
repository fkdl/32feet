﻿using System;
using System.Runtime.InteropServices;
using AppKit;
using Foundation;
using ObjCRuntime;


namespace IOBluetoothTest
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
            ObjCRuntime.Dlfcn.dlopen("/System.Library/Frameworks/IOBluetooth.framework/IOBluetooth", 0);
            var controller = IOBluetooth.IOBluetoothHostController.DefaultController;
            var cod = controller.ClassOfDevice.ToString("x8");
            var addr = controller.AddressAsString;

            foreach(IOBluetooth.IOBluetoothDevice dev in IOBluetooth.IOBluetoothDevice.PairedDevices)
            {
                IntPtr paddr = dev.GetAddress();
                IntPtr pbytes = Marshal.ReadIntPtr(paddr);
                byte[] daddr = new byte[6];
                Marshal.Copy(pbytes, daddr, 0, 6);
                //System.Diagnostics.Debug.WriteLine(dev.Address.ToString("x6"));
                System.Diagnostics.Debug.WriteLine(dev.AddressString);
                System.Diagnostics.Debug.WriteLine(dev.NameOrAddress);
            }
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
