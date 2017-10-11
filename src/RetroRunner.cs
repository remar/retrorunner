using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
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

	    Level level = new Level(cats);
	    level.LoadLevel("data/maps/map.tmx");
            float offset = 0;

            bool Running = true;
            bool LeftPressed = false;
            bool LeftChecked = false;
            bool RightPressed = false;
            bool RightChecked = false;
            float dx = 0;

            SDL.SDL_Event Event;
            uint LastTime = SDL.SDL_GetTicks ();
            while(Running) {
                float delta = (SDL.SDL_GetTicks () - LastTime) / 1000f;
                LastTime = SDL.SDL_GetTicks ();
                while(SDL.SDL_PollEvent(out Event) != 0) {
                    if(Event.type == SDL.SDL_EventType.SDL_QUIT) {
                        Running = false;
                    } else if(Event.type == SDL.SDL_EventType.SDL_KEYDOWN && Event.key.repeat == 0) {
                        if(Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE) {
                            Running = false;
                        } else if(Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_LEFT) {
                            LeftPressed = true;
                            LeftChecked = false;
                        } else if(Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_RIGHT) {
                            RightPressed = true;
                            RightChecked = false;
                        }
                    } else if(Event.type == SDL.SDL_EventType.SDL_KEYUP) {
                        if(Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_LEFT) {
                            LeftPressed = false;
                            LeftChecked = false;
                        } else if(Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_RIGHT) {
                            RightPressed = false;
                            RightChecked = false;
                        }
                    }
                }
                if(LeftPressed && !LeftChecked) {
                    dx += 1;
                    LeftChecked = true;
                }
                if(RightPressed && !RightChecked) {
                    dx -= 1;
                    RightChecked = true;
                }
                if(!LeftPressed && !LeftChecked) {
                    dx -= 1;
                    LeftChecked = true;
                }
                if(!RightPressed && !RightChecked) {
                    dx += 1;
                    RightChecked = true;
                }
                offset += dx * delta * 500;
		cats.SetScroll ((int)offset, 0);
                cats.Redraw (delta);
            }

            return 0;
        }
    }
}
