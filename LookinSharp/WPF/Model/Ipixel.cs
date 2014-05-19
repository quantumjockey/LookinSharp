///////////////////////////////////////
#region Namespace Directives

using System.Windows;

#endregion
///////////////////////////////////////

namespace LookinSharp.WPF.Model
{
    public interface Ipixel
    {
        // Property signatures
        Point Coordinate { get; }
        string Tag { get; }
        double Temperature { get; }
        string ToolTip { get; }
    }
}
