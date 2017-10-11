using System.Xml;
using cats;

namespace retrorunner {
    public class Level {
	private Cats cats;
	private Field field;

	public Level(Cats cats) {
	    this.cats = cats;
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
    }
}
