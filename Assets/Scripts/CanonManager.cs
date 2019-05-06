using System;
using System.Collections;
using System.Collections.Generic;
using EDSDKLib;
using UnityEngine;

public class CanonManager {
    private EDSDK.EdsCameraAddedHandler registerDeviceMethod;
    private IntPtr context;
    private Dictionary<string, uint> errorDict;
    private uint error;
    private string propertyData;
    private EDSDK.EdsDeviceInfo info;
    private IntPtr camRef;

    public CanonManager()
    {
        Init();
    }
    private void Init()
    {
        errorDict = new Dictionary<string, uint>();
        error = EDSDK.EdsInitializeSDK();
        Debug.Log(error);
        if (error == 0)
        {
            registerDeviceMethod += CheckDeviceDetected;
            error=EDSDK.EdsSetCameraAddedHandler(registerDeviceMethod, context);
            Debug.Log(error);
            if (error == 0)
            {   
                error = EDSDK.EdsGetCameraList(out context);
                Debug.Log(error+","+ context);
                if (error == 0)
                {
                    
                    error = EDSDK.EdsGetChildAtIndex(context, 0,out camRef);
                    Debug.Log(error+":"+ camRef);
                    if (error == 0)
                    {
                        error = EDSDK.EdsOpenSession(camRef);
                        Debug.Log(error);
                        if (error == 0)
                        {
                            error = EDSDK.EdsGetDeviceInfo(camRef, out info);
                            Debug.Log(error);
                            if (error == 0)
                            {
                                Debug.Log("connect:" + info.szDeviceDescription);
                            }
                        }
                    }
                      
                }
            }
        }
    }

    public void Shot()
    {
       error=EDSDK.EdsSendCommand(camRef, EDSDK.CameraCommand_TakePicture, 0);
        Debug.Log(error);
    }

    public void DeInit()
    {
        Debug.Log(EDSDK.EdsCloseSession(camRef));
        Debug.Log(EDSDK.EdsRelease(IntPtr.Zero));
        Debug.Log(EDSDK.EdsTerminateSDK());
    }

    private uint CheckDeviceDetected(IntPtr inContext)
    {
        Debug.Log(inContext);
        return (uint)inContext;
    }
}
