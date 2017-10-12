using System.Xml;
using cats;

namespace retrorunner {
    public class Level : Screen {
	private Game game;
	private Cats cats;
	private Input input;
	private Field field;
	private float offset = 0;
	private int goodId;

	public Level(Game game, Cats cats, Input input) {
	    this.game = game;
	    this.cats = cats;
	    this.input = input;
	    goodId = cats.CreateSpriteInstance("good");
	    cats.SetSpritePosition(goodId, 32, 480-32-25);
	    cats.SetAnimation(goodId, "stand right");
	}

	public void LoadLevel(string file) {
	    XmlDocument doc = new XmlDocument();
	    doc.Load(file);
	    XmlNode MapNode = doc.SelectSingleNode("/map");
	    int width = int.Parse(MapNode.Attributes.GetNamedItem("width").Value);
	    int height = int.Parse(MapNode.Attributes.GetNamedItem("height").Value);
	    field = new Field(cats, width, height);
	    XmlNode DataNode = doc.SelectSingleNode("/map/layer/data");
	    XmlNodeList TileNodeList = DataNode.SelectNodes("tile");
	    int i = 0;
	    
	    foreach(XmlNode tile in TileNodeList) {
		var gid = int.Parse(tile.Attributes.GetNamedItem("gid").Value);
		var x = i % width;
		var y = i / width;
		if(gid == 1) {
		    field.SetBlock(x, y, Field.BlockType.BREAKABLE);
		} else if(gid == 2) {
		    field.SetBlock(x, y, Field.BlockType.SOLID);
		}
		i++;
	    }
	}

	public void Update(float delta) {
	    if(input.Quit || input.ActionPressed(Input.Action.BACK)) {
		game.Quit = true;
	    }

	    float dx = 0;
	    if(input.ActionPressed(Input.Action.LEFT)) {
		cats.SetAnimation(goodId, "walk left");
	    } else if(input.ActionPressed(Input.Action.RIGHT)) {
		cats.SetAnimation(goodId, "walk right");
	    }
	    if(input.ActionReleased(Input.Action.LEFT) ||
	       input.ActionReleased(Input.Action.RIGHT)) {
		cats.SetAnimation(goodId, "stand right");
	    }
	    if(input.ActionHeld(Input.Action.LEFT)) {
		dx += 1;
	    }
	    if(input.ActionHeld(Input.Action.RIGHT)) {
		dx -= 1;
	    }

	    offset += dx * delta * 200;
	    cats.SetScroll ((int)offset, 0);
	}
    }
}
