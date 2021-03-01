using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 25.1.2018
    /// Column- Property Mapper
    /// </summary>
    public class CMap<TBo>
    {

        internal CMap(IColXpr ColXpr, Action<TBo, object> PropertySetter)
        {
            this.ColXpr = ColXpr;
            this.PropertySetter = PropertySetter;
        }

        internal CMap(IColXpr ColXpr, Action<TBo, object> PropertySetter, Action<TBo> DefaultValuePropertySetter)
        {
            this.ColXpr = ColXpr;
            this.PropertySetter = PropertySetter;
            this.DefaultValuePropertySetter = DefaultValuePropertySetter;
        }



        /// <summary>
        /// Defines SQL column expression. Will be later translated to valid sql string (during evaluation).
        /// </summary>
        internal IColXpr ColXpr { get; }


        /// <summary>
        /// Setsa Property of TProxy to value
        /// </summary>
        internal Action<TBo, object> PropertySetter { get; }

        /// <summary>
        /// mko, 28.11.2018
        /// If mapping can't be done, this mapper sets bo property to desired default value
        /// </summary>
        internal Action<TBo> DefaultValuePropertySetter { get; }
    }

    public class RecordToBoMapper<TBo>
    {
        CMap<TBo>[] mappings;

        internal RecordToBoMapper(IEnumerable<CMap<TBo>> mappings)
        {
            this.mappings = mappings.ToArray();
        }

        /// <summary>
        /// Applies all defined propery setter on given proxy. A property setter sets a property of proxy with data from  
        /// corresponding recordset field.
        /// 
        /// mko, 28.11.2018
        /// Maps only valid column expression (invalid == Nop).
        /// From now on conditional mapping is possible.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="reader"></param>
        public void SetPropertiesOf(TBo proxy, System.Data.IDataReader reader)
        {
            //for(int i = 0; i < mappings.Length; i++)
            //{
            //    mappings[i].PropertySetter(proxy, reader[i]);
            //}
            
            int i = 0;
            foreach(var mapping in mappings)
            {
                if(!(mapping.ColXpr is Nop))
                {
                    mapping.PropertySetter(proxy, reader[i]);
                    i++;
                } else
                {                    
                    mapping.DefaultValuePropertySetter(proxy);
                }
            }

        }

    }

}
