using SDL2;
using System.Collections.Generic;

namespace retrorunner {
    public class Input {
	private SDL.SDL_Event Event;
	public bool Quit {get; private set;}
	private Dictionary<SDL.SDL_Keycode,Action> actions =
	    new Dictionary<SDL.SDL_Keycode,Action>();
	private Dictionary<Action,ActionState> states =
	    new Dictionary<Action,ActionState>();

	public enum Action {
	    LEFT,
	    RIGHT,
	    BACK
	}

	public Input() {
	    MapKey(SDL.SDL_Keycode.SDLK_LEFT, Action.LEFT);
	    MapKey(SDL.SDL_Keycode.SDLK_RIGHT, Action.RIGHT);
	    MapKey(SDL.SDL_Keycode.SDLK_ESCAPE, Action.BACK);
	}

	public void GetInput() {
	    while(SDL.SDL_PollEvent(out Event) != 0) {
		if(Event.type == SDL.SDL_EventType.SDL_QUIT) {
		    Quit = true;
		} else if(Event.type == SDL.SDL_EventType.SDL_KEYDOWN && Event.key.repeat == 0) {
		    var sym = Event.key.keysym.sym;
		    if(actions.ContainsKey(sym)) {
			states[actions[sym]].SetState(true);
		    }
		} else if(Event.type == SDL.SDL_EventType.SDL_KEYUP) {
		    var sym = Event.key.keysym.sym;
		    if(actions.ContainsKey(sym)) {
			states[actions[sym]].SetState(false);
		    }
		}
	    }
	}

	public bool ActionPressed(Action action) {
	    if(states[action].Pressed && !states[action].Checked) {
		CheckState(action);
		return true;
	    }
	    return false;
	}

	public bool ActionReleased(Action action) {
	    if(!states[action].Pressed && !states[action].Checked) {
		CheckState(action);
		return true;
	    }
	    return false;
	}

	public bool ActionHeld(Action action) {
	    return states[action].Pressed;
	}
	
	private void CheckState(Action action) {
	    states[action].Checked = true;
	}

	private void MapKey(SDL.SDL_Keycode key, Action action) {
	    actions[key] = action;
	    states[action] = new ActionState();
	}

	public class ActionState {
	    public bool Pressed {get;set;}
	    public bool Checked {get;set;}

	    public void SetState(bool pressed) {
		Pressed = pressed;
		Checked = false;
	    }
	}
    }
}
