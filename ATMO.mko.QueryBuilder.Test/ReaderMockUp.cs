using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.QueryBuilder.Test
{
    public class ReaderMockUp : System.Data.Common.DbDataReader
    {

        (string Name, object Value)[] expectedValues;


        public ReaderMockUp(params (string Name, object Value)[] expectedValues)
        {
            this.expectedValues = expectedValues;
        }

        public override object this[int ordinal] => expectedValues[ordinal].Value;

        public override object this[string name] => expectedValues.FirstOrDefault(r => r.Name == name).Value;

        public override int Depth => throw new NotImplementedException();

        public override int FieldCount => expectedValues.Length;

        public override bool HasRows => expectedValues.Any();

        public override bool IsClosed => false;

        public override int RecordsAffected => throw new NotImplementedException();

        public override bool GetBoolean(int ordinal)
        {
            return (bool)expectedValues[ordinal].Value;
        }

        public override byte GetByte(int ordinal)
        {
            return (byte)expectedValues[ordinal].Value;
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return (long)expectedValues[ordinal].Value;
        }

        public override char GetChar(int ordinal)
        {
            return (char)expectedValues[ordinal].Value;
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return (long)expectedValues[ordinal].Value;
        }

        public override string GetDataTypeName(int ordinal)
        {
            return expectedValues[ordinal].Value.GetType().Name;
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return (DateTime)expectedValues[ordinal].Value;
        }

        public override decimal GetDecimal(int ordinal)
        {
            return (decimal)expectedValues[ordinal].Value;
        }

        public override double GetDouble(int ordinal)
        {
            return (double)expectedValues[ordinal].Value;
        }

        public override IEnumerator GetEnumerator()
        {
            return expectedValues.GetEnumerator();
        }

        public override Type GetFieldType(int ordinal)
        {
            return expectedValues[ordinal].Value.GetType();
        }

        public override float GetFloat(int ordinal)
        {
            return (float)expectedValues[ordinal].Value;
        }

        public override Guid GetGuid(int ordinal)
        {
            return (Guid)expectedValues[ordinal].Value;
        }

        public override short GetInt16(int ordinal)
        {
            return (short)expectedValues[ordinal].Value;
        }

        public override int GetInt32(int ordinal)
        {
            return (int)expectedValues[ordinal].Value;
        }

        public override long GetInt64(int ordinal)
        {
            return (long)expectedValues[ordinal].Value;
        }

        public override string GetName(int ordinal)
        {
            return expectedValues[ordinal].Name;
        }

        public override int GetOrdinal(string name)
        {
            int ordinal = -1;
            for(int i = 0; i < expectedValues.Length; i++)
            {
                if(expectedValues[i].Name == name)
                {
                    ordinal = i;
                    break;
                }
            }
            return ordinal;
        }

        public override string GetString(int ordinal)
        {
            return (string)expectedValues[ordinal].Value;
        }

        public override object GetValue(int ordinal)
        {
            return expectedValues[ordinal].Value;
        }

        public override int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public override bool IsDBNull(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override bool NextResult()
        {
            throw new NotImplementedException();
        }

        public override bool Read()
        {
            throw new NotImplementedException();
        }
    }
}
