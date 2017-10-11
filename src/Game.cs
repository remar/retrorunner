using cats;

namespace retrorunner {
    public class Game {
	private Screen currentScreen;

	public bool Quit {get; set;}

	public Game() {}

	public void SetScreen(Screen screen) {
	    currentScreen = screen;
	}

	public void Update(float delta) {
	    currentScreen.Update(delta);
	}
    }
}
