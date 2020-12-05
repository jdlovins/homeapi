using homeapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeapi
{
    public class Utils
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static Device GetDevice()
        {
            var dev = new Device()
            {
                id = "1",
                DeviceType = "action.devices.types.AUDIO_VIDEO_RECEIVER",
                traits = new List<string>()
                {
                    "action.devices.traits.InputSelector",
                    "action.devices.traits.OnOff",
                    "action.devices.traits.Volume"
                },
                name = new DeviceName
                {
                    name = "Receiver"
                },
                willReportState = true
            };

            dev.attributes.volumeMaxLevel = 80;
            dev.attributes.volumeCanMuteAndUnmute = true;
            dev.attributes.availableInputs.Add(new AcReceiverInputs()
            {
                key = "CD",
                names = new List<Name>()
                {
                    new Name()
                    {
                        name_synonym = new List<string>()
                        {
                            "CD",
                            "Computer",
                            "PC"
                        },
                        lang = "en"
                    }
                },
            });

            dev.attributes.availableInputs.Add(new AcReceiverInputs()
            {
                key = "MPLAY",
                names = new List<Name>()
                {
                    new Name()
                    {
                        name_synonym = new List<string>()
                        {
                            "Media",
                            "Media Player",
                            "Shield"
                        },
                        lang = "en"
                    }
                },
            });

            dev.attributes.availableInputs.Add(new AcReceiverInputs()
            {
                key = "DVD",
                names = new List<Name>()
                {
                    new Name()
                    {
                        name_synonym = new List<string>()
                        {
                            "DVD",
                            "3090",
                            "nvidia"
                        },
                        lang = "en"
                    }
                },
            });
            dev.attributes.deviceInfo.manufacturer = "Denon";
            dev.attributes.deviceInfo.model = "x3400h";
            dev.attributes.deviceInfo.swVersion = "1.0.0";
            dev.attributes.deviceInfo.hwVersion = "1.0.0";

            return dev;
        }
    }
}
