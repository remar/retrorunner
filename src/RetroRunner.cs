using System;
using System.IO;
using System.Reflection;
using SDL2;
using cats;

namespace retrorunner {
    public class RetroRunner {
	public static int ScreenWidth = 640;
	public static int ScreenHeight = 480;

        public static int Main() {
	    Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));


	    Cats cats = new Cats ();
            cats.Init (ScreenWidth, ScreenHeight);
            cats.LoadTileset ("data/gfx/blocks.json");
	    cats.LoadSprite("data/gfx/good.json");

	    Input input = new Input();

	    Game game = new Game();
	    Level level = new Level(game, cats, input);
	    level.LoadLevel("data/maps/map.tmx");
	    game.SetScreen(level);

            uint LastTime = SDL.SDL_GetTicks ();
            while(!game.Quit) {
                float delta = (SDL.SDL_GetTicks () - LastTime) / 1000f;
                LastTime = SDL.SDL_GetTicks ();

		input.GetInput();

		game.Update(delta);

                cats.Redraw (delta);
            }

            return 0;
        }
    }
}
