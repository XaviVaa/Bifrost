﻿using Bifrost.Devices.Gpio.Core;
using Bifrost.Devices.Gpio.Abstractions;
using System;
using System.IO;

namespace Bifrost.Devices.Gpio
{
    public class GpioPin : IGpioPin, IDisposable
    {
        public int PinNumber { get; private set; }

        public string GpioPath { get; private set; }

        public GpioPin(int pinNumber, string gpioPath)
        {
            this.PinNumber = pinNumber;
            this.GpioPath = gpioPath;
        }

        public void Dispose()
        {
            // equivalent to unexport
            // write a file with the pin name to the unexport folder
            // delete files and folders from the device path
            throw new NotImplementedException();
        }

        public void SetDriveMode(GpioPinDriveMode driveMode)
        {
            //var path = GpioController.DevicePath;
            // write in or out to the "direction" file for this pin.
            File.WriteAllText(Path.Combine(this.GpioPath, "direction"), "out");
            Directory.SetLastWriteTime(Path.Combine(this.GpioPath), DateTime.UtcNow);
        }

        public void Write(GpioPinValue pinValue)
        {
            //var path = GpioController.DevicePath;
            // write a 1 or a 0 to the "value" file for this pin
            Console.WriteLine($"Write value {pinValue.ToString()} to pin at {Path.Combine(this.GpioPath, "value")}");
            File.WriteAllText(Path.Combine(this.GpioPath, "value"), ((int)pinValue).ToString());
            Directory.SetLastWriteTime(Path.Combine(this.GpioPath), DateTime.UtcNow);
        }

        public GpioPinValue Read()
        {
            Console.WriteLine($"Testing if file exists at {Path.Combine(this.GpioPath, "value")}");
            if (File.Exists(Path.Combine(this.GpioPath, "value")))
            {
                Console.WriteLine("File exists - about to get text");
                var pinValue = File.ReadAllText(Path.Combine(this.GpioPath, "value"));

                return (GpioPinValue)Enum.Parse(typeof(GpioPinValue), pinValue);
            }
            else
            {
                return GpioPinValue.Low;
            }
        }
    }
}
