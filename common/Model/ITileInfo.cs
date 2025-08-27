namespace Syncfusion.DemosCommon.WinUI
{
    /// <summary>
    /// An interface to represent a tile information.
    /// </summary>
    public interface ITileInfo
    {
        string Name { get; }
        string Category { get; set; }
        string Description { get; set; }
        string Badge { get; }
    }
}
