﻿using UnityEngine;
using System.Collections;

public static class SpritePivotAlignment {

	public static SpriteAlignment GetSpriteAlignment(GameObject SpriteObject, BoxCollider2D spriteBoxCollider){



		float colX = spriteBoxCollider.offset.x;
		float colY = spriteBoxCollider.offset.y;

		if (colX > 0f && colY < 0f)
			return (SpriteAlignment.TopLeft);
		else if (colX < 0 && colY < 0)
			return (SpriteAlignment.TopRight);
		else if (colX == 0 && colY < 0)
			return (SpriteAlignment.TopCenter);
		else if (colX > 0 && colY == 0)
			return (SpriteAlignment.LeftCenter);
		else if (colX < 0 && colY == 0)
			return (SpriteAlignment.RightCenter);
		else if (colX > 0 && colY > 0)
			return (SpriteAlignment.BottomLeft);
		else if (colX < 0 && colY > 0)
			return (SpriteAlignment.BottomRight);
		else if (colX == 0 && colY > 0)
			return (SpriteAlignment.BottomCenter);
		else if (colX == 0 && colY == 0)
			return (SpriteAlignment.Center);
		else
			return (SpriteAlignment.Custom);
	}
}