using MKPRG.Grid2D;
using MKPRG.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TRC = MKPRG.Tracing;
using DT = MKPRG.Tracing.DocuTerms;

using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

namespace MKPRG.WormTron.V1
{
    public class Segment
        : ISegment
    {
        DT.IComposer pnL;

        public Segment(DT.IComposer pnL)
        {
            this.pnL = pnL;
        }

        public bool ToBeDrilled { get; set; }

        public Gridpoint SegmentCenterPoint { get; set; }

        public int WormNo { get; private set; }

        public IEnumerable<SegmentBorders> Borders
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ab = false;
        bool bc = false;
        bool cd = false;
        bool da = false;

        public RC AddBorder(SegmentBorders newBorder)
        {
            var ret = RC.Ok(pnL);

            if(newBorder == SegmentBorders.ab)
            {
                ab = true;
            }
            else if(newBorder == SegmentBorders.bc)
            {
                bc = true;
            }
            else if(newBorder == SegmentBorders.cd)
            {
                cd = true;
            }
            else if(newBorder == SegmentBorders.da)
            {
                da = true;
            }
            else
            {
                ret = RC.Failed(pnL.ReturnValidatePreconditionFailedArgumentOutOfRange(pnL.p("newBorder", newBorder.ToString())));
            }

            return ret;
            
        }

        public RC InsertIntoWorm(int WormNo)
        {
            this.WormNo = WormNo;
            return RC.Ok(pnL);
        }

        public RC RemoveBorder(SegmentBorders lostBorder)
        {
            throw new NotImplementedException();
        }
    }
}
