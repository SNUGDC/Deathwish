using UnityEngine;
using System;
using System.Collections;

public class Chapter5Cheat : MonoBehaviour
{
    // in chapter 5, player count is two.
    // So should ensure that cheat is used only once.
    public static bool IsCheatAvailable(Player player)
    {
        var players = FindObjectsOfType<Player>();
        if (players.Length == 1)
        {
            Debug.LogWarning("In chapter 5, player's number should be two, but now one.");
            return true;
        }
        else if (players.Length == 2)
        {
            var firstInstanceId = players[0].gameObject.GetInstanceID();
            var secondInstanceId = players[0].gameObject.GetInstanceID();
            var biggerObjectId = Math.Max(firstInstanceId, secondInstanceId);

            return biggerObjectId == player.gameObject.GetInstanceID();
        }
        else
        {
            Debug.LogError("Invalid player length. : " + players.Length);
            Debug.LogError("So does not use cheat.");
            return false;
        }
    }
}
