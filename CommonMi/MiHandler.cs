using System;
using System.Collections.Generic;
using System.Linq;
using CommonMi.Structures;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;

namespace CommonMi
{
    /// <summary>
    /// Main Class that handles all queries.
    /// </summary>
    public class MiHandler : IDisposable
    {
               private readonly CimSession _session;
        private const string CIMv2Namespace = @"root\cimv2";
        
        //Need to specify DComSessionOptions to get MI working without provider.
        private readonly DComSessionOptions sessionOptions = new DComSessionOptions
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
        /// <summary>
        /// Default constructor, creates CIM session;
        /// </summary>
        public MiHandler()
        {
            this._session = CimSession.Create("localhost", sessionOptions);
        }
        /// <summary>
        /// Class Destructor. Closes and removes CIM Session
        /// </summary>
        ~MiHandler()
        {
            this.Dispose();
        }

        /// <summary>Removes underlying CIM session object.</summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this._session.Close();
            this._session.Dispose();
        }
        /// <summary>
        /// Query <c>Win32_Bios</c> class for SerialNumber.
        /// </summary>
        /// <returns>Serial Number value</returns>
        public string GetSerialNumber()
        {
            IEnumerable<CimInstance> instances = this._session.QueryInstances(CIMv2Namespace, "WQL", "SELECT SerialNumber FROM Win32_Bios");
            foreach (CimInstance instance in instances)
            {
                return instance.CimInstanceProperties["SerialNumber"].Value.ToString();
            }
            //In case that no instance was enumerated return null;
            return null;
        }
        /// <summary>
        /// Query <c>Win32_VideoController</c> class for video adapters available in system.
        /// </summary>
        /// <returns>List of VideoController Structs</returns>
        public List<VideoController> GetVideoCards()
        {
            IEnumerable<CimInstance> instances = this._session.QueryInstances(CIMv2Namespace, 
                "WQL", 
                "SELECT Caption, DriverVersion, Status, InfFilename, PNPDeviceID FROM Win32_VideoController");
            //Create new empty array
            List<VideoController> controllers = new List<VideoController>();
            //Loop through instances, and add new controller to list
            foreach (CimInstance instance in instances)
            {
                controllers.Add(new VideoController(
                    instance.CimInstanceProperties["Caption"].Value.ToString(),
                    instance.CimInstanceProperties["DriverVersion"].Value.ToString(),
                    instance.CimInstanceProperties["Status"].Value.ToString(),
                    instance.CimInstanceProperties["InfFilename"].Value.ToString(),
                    instance.CimInstanceProperties["PNPDeviceID"].Value.ToString()
                    ));
            }
            // return completed list;
            return controllers;
        }

        
        /// <summary>
        /// Query <c>Win32_OperatingSystem</c> class for OS information
        /// </summary>
        /// <returns>OsInfo record</returns>
        public OsInfo GetOperatingSystemInfo()
        {
            IEnumerable<CimInstance> instances = this._session.QueryInstances(CIMv2Namespace, 
                "WQL", 
                "SELECT Caption, OSArchitecture, MUILanguages, Version, SystemDrive FROM Win32_OperatingSystem");
            //Cannot be more than one instance, immediately return object
            foreach (CimInstance instance in instances)
            {
                return new OsInfo(
                    instance.CimInstanceProperties["Caption"].Value.ToString(),
                    instance.CimInstanceProperties["OSArchitecture"].Value.ToString(),
                    (string[])instance.CimInstanceProperties["MUILanguages"].Value,
                    instance.CimInstanceProperties["Version"].Value.ToString(),
                    instance.CimInstanceProperties["SystemDrive"].Value.ToString()
                );
            }
            // return null by default
            return null;
        }
        
        /// <summary>
        /// Query <c>Win32_ComputerSystem</c> class for Computer Model
        /// </summary>
        /// <returns>Model string</returns>
        public String GetModel()
        {
            IEnumerable<CimInstance> instances = this._session.QueryInstances(CIMv2Namespace, 
                "WQL", 
                "SELECT Model FROM Win32_ComputerSystem");
            //Cannot be more than one instance, immediately return object
            foreach (CimInstance instance in instances)
            {
                return
                    instance.CimInstanceProperties["Model"].Value.ToString();
            }
            // return null by default
            return null;
        }

        /// <summary>
        /// Query <c>Win32_ComputerSystem</c> class for computer manufacturer.
        /// </summary>
        /// <returns>Manufacturer string</returns>
        public string GetManufacturer()
        {
            IEnumerable<CimInstance> instances = this._session.QueryInstances(CIMv2Namespace, 
                "WQL", 
                "SELECT Manufacturer FROM Win32_ComputerSystem");
            //Cannot be more than one instance, immediately return object
            foreach (CimInstance instance in instances)
            {
                return
                    instance.CimInstanceProperties["Manufacturer"].Value.ToString();
            }
            // return null by default
            return null;
        }
        /// <summary>
        /// Query <c>Win32_Processor</c> class for all processors
        /// </summary>
        /// <returns>Array of processor names</returns>
        public string[] GetProcessors()
        {
            IEnumerable<CimInstance> instances = this._session.QueryInstances(CIMv2Namespace, 
                "WQL", 
                "SELECT Name FROM Win32_Processor");
            // return array of names.
            return instances.Select(instance => instance.CimInstanceProperties["Name"].Value.ToString()).ToArray();
        }

        public bool IsComputerOnACPower()
        {
            throw new NotImplementedException();
        }

        public bool IsWindowsActivated()
        {
            throw new NotImplementedException();
        }

        public string GetIP()
        {
            throw new NotImplementedException();
        }

        public List<DiskDrive> GetDiskDrives(bool internalDrivesOnly = false)
        {
            throw new NotImplementedException();
        }
    }
}