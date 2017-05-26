using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOrderChange : MonoBehaviour
{

	public MeshRenderer _renderer;
	// Use this for initialization
	void Update()
	{

		_renderer.sortingLayerName = "LayerName";
		_renderer.sortingOrder = 100;
	}

}
