using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 8.7.2020
/// Netzwerke
/// </summary>
namespace MKPRG.Naming.TechTerms.Net
{
    public class Network
        : NamingBase
    {
        public const long UID = 0xF61BE997;

        public Network()
            : base(UID)
        {
        }

        public override string CNT => "net";
        public override string CN => EN;
        public override string DE => "Netzwerk";
        public override string EN => "Network";
        public override string ES => EN;
    }

    public class Node
        : NamingBase
    {
        public const long UID = 0xCA25FA92;

        public Node()
            : base(UID)
        {
        }

        public override string CNT => "netNode";
        public override string CN => EN;
        public override string DE => "Netzwerkknoten";
        public override string EN => "Network node";
        public override string ES => EN;
    }

    public class NodeName
        : NamingBase
    {
        public const long UID = 0xDFC22929;

        public NodeName()
            : base(UID)
        {
        }

        public override string CNT => "netNodeName";
        public override string CN => EN;
        public override string DE => "Netzwerkknotenname";
        public override string EN => "Network node name";
        public override string ES => EN;
    }


    public class NetworkAdapter
    : NamingBase
    {
        public const long UID = 0xCFA6CF17;

        public NetworkAdapter()
            : base(UID)
        {
        }

        public override string CNT => "netAdapter";
        public override string CN => EN;
        public override string DE => "Netzwerkkarte";
        public override string EN => "Network adapter";
        public override string ES => EN;
    }


    public class NetworkInterface
        : NamingBase
    {
        public const long UID = 0xB2735B8A;

        public NetworkInterface()
            : base(UID)
        {
        }

        public override string CNT => "netInterface";
        public override string CN => EN;
        public override string DE => "Netzwerkschnittstelle";
        public override string EN => "Network interface";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class AccessPoint
    : NamingBase
    {
        public const long UID = 0x678219ED;

        public AccessPoint()
            : base(UID)
        {
        }

        public override string CNT => "accessPoint";
        public override string CN => EN;
        public override string DE => "Zugangspunkt";
        public override string EN => "Access Point";
        public override string ES => "Punto de acceso";
    }


    /// <summary>
    /// 13.7.2020
    /// </summary>
    public class ConnectionType
    : NamingBase
    {
        public const long UID = 0x7F7E2392;

        public ConnectionType()
            : base(UID)
        {
        }

        public override string CNT => "connectionType";
        public override string CN => EN;
        public override string DE => "Verbindungtyp";
        public override string EN => "connection type";
        public override string ES => "Tipo de conexión";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class ConnectionParameters
    : NamingBase
    {
        public const long UID = 0xCC77C35D;

        public ConnectionParameters()
            : base(UID)
        {
        }

        public override string CNT => "connectionParams";
        public override string CN => EN;
        public override string DE => "Konfigurationsdaten der Netzwerkverbindung";
        public override string EN => "Configuration data of the network connection";
        public override string ES => "Datos de configuración de la conexión de la red";
    }


    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class CheckConnectivity
    : NamingBase
    {
        public const long UID = 0x67E80809;

        public CheckConnectivity()
            : base(UID)
        {
        }

        public override string CNT => "checkConnectivity";
        public override string CN => EN;
        public override string DE => "Verbindungsqualität mit dem Netzwerk prüfen";
        public override string EN => "Check the connection quality with the network";
        public override string ES => "Comprobando la calidad de la conexión con la red";
    }



}
