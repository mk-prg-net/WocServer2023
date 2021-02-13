using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access.Datasources.ATMO.DFC.SessionTreeStore
{
    /// <summary>
    /// mko, 13.10.2020
    /// Klassifikation der Art und Weise der Ablage im DFC Filestore
    /// </summary>
    public class SessionTreeStore : NamingBase
    {
        public const long UID = 0x40E46AF9;

        public SessionTreeStore()
            : base(UID)
        {
        }

        public override string CNT => "sessionTreeStore";
        public override string CN => "DFC会话树商店";
        public override string DE => "DFC Sitzungsspeicher für Bäume";
        public override string EN => "DFC Session Tree Store";
        public override string ES => "Tienda de Árbol de Sesiones de DFC";
    }

    /// <summary>
    /// mko, 13.10.2020
    /// </summary>
    public class SessionId : NamingBase
    {
        public const long UID = 0xD35D7ABE;

        public SessionId()
            : base(UID)
        {
        }

        public override string CNT => "sessionID";
        public override string CN => "DFC的会话编号 树的会话内存";
        public override string DE => "Sitzungsnummer für DFC Sitzungsspeicher für Bäume";
        public override string EN => "SessionId for DFC Session Tree Store";
        public override string ES => "SessionId para DFC Session Tree Store";
    }



    /// <summary>
    /// mko, 13.10.2020
    /// </summary>
    public class TreeStoreMechanicalAreas : NamingBase
    {
        public const long UID = 0x7AF8B23C;

        public TreeStoreMechanicalAreas()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreMechanicalAreas";
        public override string CN => "DFC机械零件清单会话记忆";
        public override string DE => "DFC Sitzungsspeicher für mechanische Stücklisten";
        public override string EN => "DFC session memory for mechanical parts lists";
        public override string ES => "Memoria de sesión DFC para listas de piezas mecánicas";
    }

    /// <summary>
    /// mko, 13.10.2020
    /// </summary>
    public class TreeStoreElectricalAreas : NamingBase
    {
        public const long UID = 0xB1C20ED2;

        public TreeStoreElectricalAreas()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreElectricalAreas";
        public override string CN => "DFC电子零件清单会话存储器";
        public override string DE => "DFC Sitzungsspeicher für elektrische Stücklisten";
        public override string EN => "DFC session memory for electrical parts lists";
        public override string ES => "Memoria de sesión DFC para listas de piezas eléctricas";
    }

    /// <summary>
    /// mko, 13.10.2020
    /// </summary>
    public class TreeStoreProjects : NamingBase
    {
        public const long UID = 0xB887369;

        public TreeStoreProjects()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreProjects";
        public override string CN => "项目的DFC会话记忆";
        public override string DE => "DFC Sitzungsspeicher für Projekte";
        public override string EN => "DFC session memory for projects";
        public override string ES => "Memoria de sesión del DFC para proyectos";
    }

    /// <summary>
    /// mko, 13.10.2020
    /// </summary>
    public class TreeStoreStations : NamingBase
    {
        public const long UID = 0x20FB06B0;

        public TreeStoreStations()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreStations";
        public override string CN => "台站的DFC会话存储器";
        public override string DE => "DFC Sitzungsspeicher für Stationen";
        public override string EN => "DFC session memory for stations";
        public override string ES => "DFC Memoria de sesión para estaciones";
    }

    public class TreeStoreProcessModules : NamingBase
    {
        public const long UID = 0x22BCED4C;

        public TreeStoreProcessModules()
            : base(UID)
        {
        }

        public override string CNT => "treeStorePMs";
        public override string CN => "过程模块的DFC会话存储器";
        public override string DE => "DFC Sitzungsspeicher für Prozessmodule";
        public override string EN => "DFC session memory for process modules";
        public override string ES => "DFC Memoria de sesión para módulos de proceso";
    }


    public class TreeStoreAssemblies : NamingBase
    {
        public const long UID = 0x815B99ED;

        public TreeStoreAssemblies()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreAssemblies";
        public override string CN => "模块的DFC会话存储器";
        public override string DE => "DFC Sitzungsspeicher für Baugruppen";
        public override string EN => "DFC session memory for modules";
        public override string ES => "DFC Memoria de sesión para módulos";
    }

    public class TreeStoreSinglePart : NamingBase
    {
        public const long UID = 0xFA868386;

        public TreeStoreSinglePart()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreSinglePart";
        public override string CN => "单个部件的DFC会话存储器";
        public override string DE => "DFC Sitzungsspeicher für Einzelteile";
        public override string EN => "DFC session memory for single parts";
        public override string ES => "DFC Memoria de sesión para las partes individuales";
    }

    /// <summary>
    /// Speicherhandle 
    /// </summary>
    public class TreeStoreHandle : NamingBase
    {
        public const long UID = 0xC438ED85;

        public TreeStoreHandle()
            : base(UID)
        {
        }

        public override string CNT => "treeStoreHandle";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }



}
