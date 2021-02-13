using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Event
        : DocuEntity,
        IEvent
    {
        public Event(IFormater fmt, NID nid)
            : base(fmt, DocuEntityTypes.Event, nid)
                {
                }

        public Event(IFormater fmt, NID nid, IEventParameter eventParam)
            : base(fmt, DocuEntityTypes.Event, nid, eventParam)
        {
        }

        public Event(IFormater fmt, String name)
            : base(fmt, DocuEntityTypes.Event, name)
        {
        }

        public Event(IFormater fmt, String name, IEventParameter eventParam)
            : base(fmt, DocuEntityTypes.Event, name, eventParam)
        {
        }


        public DocuEntityHlp.EventTypes EventType
        {
            get
            {
                if (Childs.First() is NID nid)
                {
                    switch (nid.NamingId)
                    {
                        case ANC.DocuTerms.Event.End.UID:
                            return DocuEntityHlp.EventTypes.end;
                        case ANC.DocuTerms.Event.Fails.UID:
                            return DocuEntityHlp.EventTypes.fails;
                        case ANC.DocuTerms.Event.Info.UID:
                            return DocuEntityHlp.EventTypes.info;
                        case ANC.DocuTerms.Event.NotCompleted.UID:
                            return DocuEntityHlp.EventTypes.notCompleted;
                        case ANC.DocuTerms.Event.Start.UID:
                            return DocuEntityHlp.EventTypes.start;
                        case ANC.DocuTerms.Event.Succeeded.UID:
                            return DocuEntityHlp.EventTypes.succeded;
                        case ANC.DocuTerms.Event.Warn.UID:
                            return DocuEntityHlp.EventTypes.warn;
                        default:
                            return DocuEntityHlp.EventTypes.none;
                    }
                }
                else
                {
                    return this.GetEventType();
                }
            }
        }

        public IEventParameter EventParameter => (IEventParameter)Childs.Skip(1).FirstOrDefault();
    }
}
