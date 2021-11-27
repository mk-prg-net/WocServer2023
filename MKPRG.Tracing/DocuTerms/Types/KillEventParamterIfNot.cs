﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms
{
    public class KillEventParamIfNot
        : DocuEntity,
        IKillEventParamIfNot
    {
        public KillEventParamIfNot(bool Condition, Func<IEventParameter> createEventParameter)
            : base(DocuEntityTypes.KillIfNot)
        {
            this.Condition = Condition;

            if (createEventParameter != null)
                _createEventParameter = createEventParameter;
        }

        public static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));

        Func<IEventParameter> _createEventParameter { get; } = new Func<IEventParameter>(() => _defaultValue);

        public IEventParameter EventParameter => _createEventParameter();

        public bool Condition { get; }

    }
}
