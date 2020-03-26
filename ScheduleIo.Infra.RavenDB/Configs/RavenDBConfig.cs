using ScheduleIo.Infra.Configurations;
using ScheduleIo.Infra.Configurations.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ScheduleIo.Infra.RavenDB.Configs
{
    public class RavenDBConfig : IDataBaseConfig
    {
        public string[] Urls;
        public string DataBase { get; set; }
        public string CertificateFilePath { get; set; }
        public string CertificatePassword { get; set; }
        public RavenDBConfig(string[] urls, string dataBase, string certificateFilePath, string certificatePassword = null)
        {
            Urls = urls;
            DataBase = dataBase;
            CertificateFilePath = certificateFilePath;
            CertificatePassword = certificatePassword;
        }

        public EDataBaseType GetDataBaseType()
        {
            return EDataBaseType.RAVENDB;
        }
    }
}

