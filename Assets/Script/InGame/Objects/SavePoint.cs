using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointSnapshot
{
    public readonly Vector3 position;
    public readonly Enums.IsDark isDark;

    public SavePointSnapshot(Vector3 position, Enums.IsDark isDark)
    {
		this.position = position;
		this.isDark = isDark;
    }
}

public class SavePoint : MonoBehaviour
{

    public static SavePointSnapshot snapshot;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        snapshot = new SavePointSnapshot(player.transform.position, Global.ingame.isDark);
    }
}
