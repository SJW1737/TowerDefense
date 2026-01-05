using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator
{
    public static WaveData Generate(int wave)
    {
        WaveData data = new WaveData();
        data.monsters = new List<WaveMonster>();

        data.spawnInterval = 1f;

        int patternWave = (wave - 1) % 10 + 1;

        if (patternWave == 10)
        {
            data.monsters.Add(new WaveMonster{type = MonsterType.Boss, count = 1});
        }

        //Wave 1 ~ 3
        if (patternWave >= 1 && patternWave <= 3)
        {
            int[] normalCounts = { 5, 5, 6 };
            data.monsters.Add(new WaveMonster{type = MonsterType.Normal, count = normalCounts[patternWave - 1]});
        }
        //Wave 4 ~ 6
        else if (patternWave >= 4 && patternWave <= 6)
        {
            int[] normalCounts = { 5, 5, 6 };
            int[] fastCounts = { 2, 2, 3 };

            int idx = patternWave - 4;

            data.monsters.Add(new WaveMonster{type = MonsterType.Normal, count = normalCounts[idx]});

            data.monsters.Add(new WaveMonster{type = MonsterType.Fast, count = fastCounts[idx]});
        }
        //Wave 7 ~ 9
        else if (patternWave >= 7 && patternWave <= 9)
        {
            int[] normalCounts = { 4, 4, 4 };
            int[] fastCounts = { 3, 3, 3 };
            int[] tankCounts = { 1, 1, 2 };

            int idx = patternWave - 7;

            data.monsters.Add(new WaveMonster { type = MonsterType.Tank, count = tankCounts[idx] });

            data.monsters.Add(new WaveMonster{type = MonsterType.Normal, count = normalCounts[idx]});

            data.monsters.Add(new WaveMonster{type = MonsterType.Fast, count = fastCounts[idx]});
        }

        return data;
    }
}
