using UnityEngine;
using System.Collections;

public class InputListener : MonoSingleton<InputListener> {

	public delegate void listener( string type, int id, float x, float y, float dx, float dy );
	public event listener begin0, begin1, begin2, begin3, begin4;
	public event listener move0, move1, move2, move3, move4;
	public event listener end0, end1, end2, end3, end4;

	public bool isKeyboard = true;

	Vector2[] delta = new Vector2[5];

	void Start()
	{
		DontDestroyOnLoad(this);
	}

	void Update()
	{
		if (isKeyboard) {
			KeyboardInputUpdate ();
		} 
		else {
			MouseInputUpdate ();
		}
	}

	void KeyboardInputUpdate()
	{
		if (Input.GetKeyDown (KeyCode.A) == true) {
			if( end0 != null ) end0( "end", 0, 0, 0, -100, 0 );
		}
		else if (Input.GetKeyDown (KeyCode.D) == true){
			if( end0 != null ) end0( "end", 0, 0, 0, 100, 0 );
		}
		else if (Input.GetKeyDown (KeyCode.Space) == true){
			if( end0 != null ) end0( "end", 0, 0, 0, 0, 0 );
		}
		else if (Input.GetKeyDown (KeyCode.LeftShift) == true){
			if( end0 != null ) end0( "dash", 0, 0, 0, 0, 0 );
		}
	}

	void MouseInputUpdate()
	{
#if UNITY_EDITOR
		int count = (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))?1:0;
#else
		int count = Input.touchCount;
#endif
		if( count == 0 ) return;

		// mobile <-> editor variant
		int fingerId = 0;
		Vector2 position = Vector2.zero;
		TouchPhase phase = TouchPhase.Began;

		for( int i = 0 ; i < count ; i++ )
		{
#if UNITY_EDITOR
			fingerId = 0;
			position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			bool mbegan = false, mdown = false, mup = false;
			mbegan = Input.GetMouseButtonDown(0);
			mdown = Input.GetMouseButton(0);
			mup = Input.GetMouseButtonUp(0);
			//Debug.Log5(string.Format("GetButton: {0} Down: {1} Up: {2}",mbegan,mdown,mup));
			phase = mbegan?TouchPhase.Began:mdown?TouchPhase.Moved:TouchPhase.Ended;
#else
			Touch touch = Input.GetTouch(i);
			fingerId = touch.fingerId;
			position = touch.position;
			phase = touch.phase;
#endif
			int id = fingerId;
			Vector2 pos = position;
			if( phase == TouchPhase.Began ) delta[id] = position;
			float x, y, dx, dy;
			x = pos.x;
			y = pos.y;
			if( phase == TouchPhase.Began ){
				dx = dy = 0;
			}else{
				dx = pos.x - delta[id].x;
				dy = pos.y - delta[id].y;
			}

			//상태에 따라 이벤트를 호출하자
			if( phase == TouchPhase.Began ){
				switch( id ){
				case 0: if( begin0 != null ) begin0( "begin", id, x, y, dx, dy ); break;
				case 1: if( begin1 != null ) begin1( "begin", id, x, y, dx, dy ); break;
				case 2: if( begin2 != null ) begin2( "begin", id, x, y, dx, dy ); break;
				case 3: if( begin3 != null ) begin3( "begin", id, x, y, dx, dy ); break;
				case 4: if( begin4 != null ) begin4( "begin", id, x, y, dx, dy ); break;
				}
			}else if( phase == TouchPhase.Moved ){
				switch( id ){
				case 0: if( move0 != null ) move0( "move", id, x, y, dx, dy ); break;
				case 1: if( move1 != null ) move1( "move", id, x, y, dx, dy ); break;
				case 2: if( move2 != null ) move2( "move", id, x, y, dx, dy ); break;
				case 3: if( move3 != null ) move3( "move", id, x, y, dx, dy ); break;
				case 4: if( move4 != null ) move4( "move", id, x, y, dx, dy ); break;
				}
			}else if( phase == TouchPhase.Ended ){
				switch( id ){
				case 0: if( end0 != null ) end0( "end", id, x, y, dx, dy ); break;
				case 1: if( end1 != null ) end1( "end", id, x, y, dx, dy ); break;
				case 2: if( end2 != null ) end2( "end", id, x, y, dx, dy ); break;
				case 3: if( end3 != null ) end3( "end", id, x, y, dx, dy ); break;
				case 4: if( end4 != null ) end4( "end", id, x, y, dx, dy ); break;
				}
			}
		}
	}

	/*
	void Start()
	{
		begin0 += onTouch;
		end0 += onTouch;
		move0 += onTouch;
	}

	void onTouch( string type, int id, float x, float y, float dx, float dy)
	{
		switch( type ){
		case"begin": Debug.Log( "down:" + x + "," + y ); break;
		case"end": Debug.Log( "end:" + x + "," + y +", d:" + dx +","+dy ); break;
		case"move": Debug.Log( "move:" + x + "," + y +", d:" + dx +","+dy ); break;
		}
	}
	*/
}