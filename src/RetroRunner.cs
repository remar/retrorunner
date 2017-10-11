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

	    Input input = new Input();

	    Level level = new Level(cats);
	    level.LoadLevel("data/maps/map.tmx");
            float offset = 0;

            bool Running = true;
            float dx = 0;

            uint LastTime = SDL.SDL_GetTicks ();
            while(Running) {
                float delta = (SDL.SDL_GetTicks () - LastTime) / 1000f;
                LastTime = SDL.SDL_GetTicks ();

		input.GetInput();

		if(input.Quit || input.ActionPressed(Input.Action.BACK)) {
		    Running = false;
		}

		dx = 0;
		if(input.ActionHeld(Input.Action.LEFT)) {
		    dx += 1;
		}
		if(input.ActionHeld(Input.Action.RIGHT)) {
		    dx -= 1;
		}

                offset += dx * delta * 500;
		cats.SetScroll ((int)offset, 0);

                cats.Redraw (delta);
            }

            return 0;
        }
    }
}
