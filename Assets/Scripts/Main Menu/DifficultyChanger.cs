using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public void Easy()
    {
        //decrease speed and damage of enemies
        EnemyClass.difficultyDamage = 0.8f;
        EnemyClass.difficultySpeed = 0.8f;
        //increase health
        PlayerTarget.difficultyHealth = 1.2f;
        //set value of artifact
        ArtifactData.difficultyValue = 0.8f;
    }
    public void Normal()
    {
        //leave all settings as default
        EnemyClass.difficultyDamage = 1f;
        EnemyClass.difficultySpeed = 1f;

        PlayerTarget.difficultyHealth = 1f;
        //set value of artifact
        ArtifactData.difficultyValue = 1f;
    }
    public void Hard()
    {
        //increase speed and damage of enemies
        EnemyClass.difficultyDamage = 1.2f;
        EnemyClass.difficultySpeed = 1.2f;
        //decrease player health
        PlayerTarget.difficultyHealth = 0.8f;
        //set value of artifact
        ArtifactData.difficultyValue = 1.2f;
    }

}
