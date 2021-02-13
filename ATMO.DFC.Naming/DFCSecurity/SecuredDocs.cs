using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCSecurity
{
    /// <summary>
    /// mko, 12.4.2018
    /// Derived from DZAUtilities.GlobalDictionaries.DocTypeSAP
    /// 
    /// Following subsets were originally defined in frmDFC. Restricted types are not visible for everybody! 
    /// DocTypeSAP[] listRestrictedDocTypes = new DocTypeSAP[]
    ///    {
    ///        DocTypeSAP.AT3, DocTypeSAP.ATD, DocTypeSAP.ATZ, DocTypeSAP.ECA, DocTypeSAP.CTS, DocTypeSAP.SFC, DocTypeSAP.EDC,DocTypeSAP.ATB
    ///    };
    ///    
    /// Defines the subset of drawings
    /// DocTypeSAP[] lDrawingTypes = new DocTypeSAP[] { DocTypeSAP.ATD, DocTypeSAP.ATZ, DocTypeSAP.AT3, DocTypeSAP.ATDATZ };
    /// 
    /// mko, 04.05.2018
    /// Reduced.
    /// 
    /// mko 18.5.2018
    /// All securables removed that represents general user rights like docu check etc..
    /// This general rights now expressd as properties of IUserV02.
    /// All removed, because without meaning.
    /// Securables with pub suffix have got a new semantics:
    /// For a coworker a securable can be morphed to public counterpart (ATB -> ATB_pub), if it's IDocuClaim.IsZAT, .IsSTDBG or .IsDOKU is true.  
    /// For a customer a securable can be morphed to public counterpart (ATB -> ATB_pub), if it's IDocuClaim.IsEVWP is true.
    /// In general, public securables are readable by users. In special cases these types allow implementation for user groups with more stringent restrictions.
    /// </summary>
    public enum SecuredDocs
    {
        /// <summary>
        /// Type of Documents currently not documented here
        /// </summary>
        unkown,

        /// <summary>
        /// Part List
        /// </summary>
        ATB,

        /// <summary>
        /// public         
        /// </summary>
        ATB_pub,     

        /// <summary>
        /// Drawing, assigned to a Baugruppe (Zusammenbauzeichnung)
        /// </summary>
        ATDB,      
        ATDB_pub,  


        /// <summary>
        /// 2d Drawings
        /// </summary>
        ATD,        
        ATD_pub,
        
        /// <summary>
        /// mko, 30.7.2018
        /// Drawings of standarized parts, assys etc. 
        /// </summary>
        ATD_std,

        // mko, 13.4.2018
        // added
        /// <summary>
        /// EPlan
        /// </summary>
        EPlan,      //  EPLAN Document
        EPlan_pub,

        /// <summary>
        /// Offers
        /// </summary>
        ATO,      
        ATO_pub,  

        /// <summary>
        /// Catalog parts
        /// </summary>
        CAT,        

        /// <summary>
        /// Technical documentation, accessible for all DFC users (e.g. TDP 1, TDP 3, ...)
        /// </summary>
        TDP,            

        /// <summary>
        /// mko, 18.5.2018
        /// This kind of TDP is not accessible for custommers
        /// </summary>
        TDP_internal,      

        /// <summary>
        /// Manuals
        /// </summary>
        MAN,    

        /// <summary>
        /// Documentation of process cycletimes
        /// </summary>
        CTS,    

        /// <summary>
        /// Engineering changes
        /// write:  define a new EDC (only for engineers)
        /// read:   
        /// </summary>
        EDC,        

        /// <summary>
        /// Shop Floor changes
        /// </summary>
        SFC,        

        /// <summary>
        /// TEF Ressouces
        /// </summary>
        TEF,


        /// <summary>
        /// mko, 11.6.2018
        /// Prices of Bom' Items, price informations in offers etc. 
        /// </summary>
        PricingInformation,

        /// <summary>
        /// mko, 6.5.2019
        /// Project node of DFC Tree
        /// </summary>
        Project,

        /// <summary>
        /// mko, 6.5.2019
        /// Station Node of DFC Tree
        /// </summary>
        Station,

        /// <summary>
        /// mko, 6.5.2019
        /// Assembly group (Baugruppe) Node of DFC Tree
        /// </summary>
        Assy,

        /// <summary>
        /// mko, 6.5.2019
        /// Single Part Node of DFC Tree
        /// </summary>
        SingelPart,

        /// <summary>
        /// mko, 18.3.2020
        /// Personenbezogene Daten, die bezüglich der GDPR zu schützen sind 
        /// </summary>
        PersonalData,

        /// <summary>
        /// mko, 6.4.2020
        /// CharacteristicValues: Merkmale bzw. Parameter einer konfigurierbaren Baugruppe oder Einzelteil.
        /// Z.B. sind für ein Transportband (Baugruppe), das in einer Station verbaut wurde,
        /// die Länge und Breite festzulegen. Diese Daten werden in DFC als 
        /// CharacteristicValues dargestellt.
        /// </summary>
        CV,

        /// <summary>
        /// mko, 2.9.2020
        /// Prozessmodul
        /// </summary>
        FlexCon,

    }
}
