using cats;

namespace retrorunner {
    public class Field {
	public int Width {get; private set;}
	public int Height {get; private set;}
	private BlockType[,] field;
	private Cats cats;

	public Field(Cats cats, int Width, int Height) {
	    this.Width = Width;
	    this.Height = Height;
	    this.cats = cats;
	    field = new BlockType[Width, Height];
            cats.SetupTileLayer (Width, Height, 32, 32);
	}

	public void SetBlock(int x, int y, BlockType type) {
	    field[x,y] = type;
	    cats.SetTile(x, y, "blocks", 0, type == BlockType.BREAKABLE ? 0 : 2);
	}

	public enum BlockType {
	    EMPTY = 0,
	    SOLID = 1,
	    BREAKABLE = 2,
	    DAMAGED = 3,
	    BROKEN = 4
	}
    }
}
