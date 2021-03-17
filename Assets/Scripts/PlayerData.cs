using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public string checkpoint;
    public float[] position;
    public bool in_level;
    public int nr_deaths;
    public float timeInGame;

    public PlayerData(Player player)
    {
        level = player.level;
        checkpoint = player.checkpoint;
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        in_level = GameMaster.wasingame;
        timeInGame = GameMaster.timeInGame;
        nr_deaths = GameMaster.nr_death;
    }
}
