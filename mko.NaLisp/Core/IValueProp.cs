﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Core
{
    public interface IValueProp<T>
    {
        T Value { get; set; }
    }
}
