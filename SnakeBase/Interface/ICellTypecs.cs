using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bing.Interface
{
    public interface ICell
    {
    }
    public interface IEmptyCell : ICell
    {
    }
    public interface IBodyCell : ICell
    {
    }
    public interface IFoodCell : ICell
    {
    }
}
