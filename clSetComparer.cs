using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sprut
{
    class clSetComparer : IEqualityComparer<clSet>
    {

        public bool Equals(clSet x, clSet y)
        {
            bool result = true;

            ///if ((x.SetGuid.Equals(y.SetGuid)) && (x.StageGuid.Equals(y.StageGuid)))
            if (x.SetGuid.Equals(y.SetGuid))
            {
                //if((!x.PercentComplete.Equals(y.PercentComplete)) || (!x._responsible_user.Id.Equals(y._responsible_user.Id)))
                if(!x._responsible_user.Fio.Equals(y._responsible_user.Fio))
                {
                    result = false;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public int GetHashCode(clSet obj)
        {
            return (obj.Contract + obj.SetGuid + obj._responsible_user.Fio).GetHashCode();
        }
    }
}
