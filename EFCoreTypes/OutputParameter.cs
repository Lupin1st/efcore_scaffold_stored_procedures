using System;

namespace EFCore.ScaffoldProcedures.EFCoreTypes
{
    public class OutputParameter<TValue>
    {
        private bool _hasOperationFinished = false;

        public TValue _value;
        public TValue Value
        {
            get
            {
                if (!_hasOperationFinished)
                    throw new InvalidOperationException("Operation has not finished.");

                return _value;
            }
        }

        public void SetValueInternal(object value)
        {
            _hasOperationFinished = true;
            _value = (TValue)value;
        }

        public System.Data.DbType GetDataTypeInternal()
        {
            if (typeof(TValue) == typeof(int))
                return System.Data.DbType.Int32;

            throw new NotImplementedException("Only int is supported :(");
        }
    }
}
