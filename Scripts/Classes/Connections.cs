public class Connections
{
    public enum Flow
    {
        Horizontal = 0,
        Vertical = 1
    }
    public bool IsFirstLetter = false;
    public bool IsLastLetter = false;
}

public class AdjacentTiles
{
    public class Side
    {
        public PlayableTile Tile;
        public bool Exists;

        public Side(bool exists)
        {
            Exists = exists;
        }

        public Side(PlayableTile tile, bool exists)
        {
            Exists = exists;
            Tile = tile;
        }
    }
    public Side Top = new Side(false);
    public Side Right = new Side(false);
    public Side Left = new Side(false);
    public Side Bottom = new Side(false);
}
