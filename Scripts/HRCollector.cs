using System;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;

public class HRCollector 
{

    SerialPort arduino;
    volatile bool stopCollecting = false;
    public volatile int HeartRate = -1;
    public volatile int IBI = -1;
    Thread collectionThread;
    public volatile int BPM = -1;
    public string MyPort()
    {
        return (arduino == null) ? "" : arduino.PortName;
    }

    public HRCollector()
    {
    }

    public int CurrentBPM()
    {
        return BPM;
    }

    public bool Connect()
    {
        //Debug.Log("made it here");
        bool connected = false;
        var allPorts = new List<String>(SerialPort.GetPortNames());
        foreach (var item in allPorts)
        {
            Debug.Log(item);
            if (IsArduino(item))
            {
                //success!
                arduino = PortNamed(item, 9600, Parity.None, StopBits.One, 8);
                connected = true;
                //Debug.Log("made it to the true also known as success");
                break;
            }
            else
            {
                //couldn't connect to arduino
            }
        }

        return connected;
    }

    public bool IsArduino(string portname)
    {
        //Debug.Log("isarduino check ");
        int validationTries = 10;
        SerialPort arduino = PortNamed(portname, 9600, Parity.None, StopBits.One, 8);
        //Debug.Log("just before try");
        try
        {
            //Debug.Log("We made it to the try");
            arduino.ReadTimeout = 5000;
            arduino.Open();
            //Debug.Log("port opened");
            //Debug.Log(arduino.ReadLine());
            if (arduino.BytesToRead <= 0)
            {
                Thread.Sleep(500);
            }

            for (int i = 0; i < validationTries; i++)
            {
                string msg = arduino.ReadLine();
                //Debug.Log(msg);
                if (msg.StartsWith("B"))
                {
                    arduino.Close();
                    return true;
                }
            }
        }
        catch
        {
            arduino.Close();
        }

        return false;
    }

    public SerialPort PortNamed(string portName, int baudrate, Parity parity, StopBits stopbits, int bytesize)
    {
        var port = new SerialPort(portName);
        port.BaudRate = baudrate;
        port.Parity = parity;
        port.StopBits = stopbits;
        port.DataBits = bytesize;
        return port;
    }

    public void StartCollecting()
    {
        //Debug.Log("made it to start Collecting");
        stopCollecting = false;
        collectionThread = new Thread(new ThreadStart(CollectorTask));
        collectionThread.Start();
    }

    public void StopCollecting()
    {
        stopCollecting = true;
    }

    void CollectorTask()
    {
        arduino.Open();
        while (!stopCollecting)
        {
            if (arduino.BytesToRead > 0)
            {
                ReadData();
            }
        }

        arduino.Close();
    }

    void ReadData()
    {
        //read line from arduino
        string msg = arduino.ReadLine();
        //Debug.Log(msg);
        var values = msg.Split(' ');
        //Debug.Log(values[1]);
        //parse heart rate
        int tmpHR = HeartRate;
        int.TryParse(values[0], out tmpHR);
        HeartRate = tmpHR;
        //parse inter-beat interval
        int tmpIBI = IBI;
        int.TryParse(values[1], out tmpIBI);
        IBI = tmpIBI;
        //Debug.Log(IBI);
        //Debug.Log(IBI.GetType());
        BPM = IBI;
    }

}
